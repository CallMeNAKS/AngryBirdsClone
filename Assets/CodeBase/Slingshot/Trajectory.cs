using System;
using CodeBase.Slingshot;
using UnityEngine;

public class DynamicLineFromDirection : MonoBehaviour
{
    [SerializeField] private GameObject _pointPrefab;   // Префаб точки (например, Sphere)
    [SerializeField] private Transform _startPoint;     // Точка начала
    [SerializeField] private Vector3 _direction = Vector3.forward; // Направление линии
    [SerializeField] private float _distance = 0.2f;      // Расстояние между точками
    [SerializeField] private int _pointCount = 5;      // Количество точек

    [SerializeField] private ShootPoint _shootPoint;
    
    private float _gravity = -9.81f; 

    private GameObject[] _points; // Массив для хранения точек

    private void OnEnable()
    {
        _shootPoint.Trajectory += DrawLine;
    }

    private void Start()
    {
        _points = new GameObject[_pointCount];

        for (int i = 0; i < _pointCount; i++)
        {
            Vector3 position = transform.position + _direction.normalized * i * _distance;
            _points[i] = Instantiate(_pointPrefab, position, Quaternion.identity);
        }
    }

    private void DrawLine(Vector3 direction)
    {
        // Нормализуем направление выстрела
        Vector3 normalizedDirection = -direction.normalized;

        // Начальная скорость (можно изменять для нужного эффекта)
        float speed = 30f;

        // Рассчитываем расстояние между точкой старта и точкой выстрела
        float distanceToShootPoint = Vector3.Distance(transform.position, _shootPoint.transform.position);

        // Меняем расстояние между точками в зависимости от дистанции до точки выстрела
        float dynamicDistance = Mathf.Max(0.1f, distanceToShootPoint / _pointCount) * _distance;

        for (int i = 0; i < _pointCount; i++)
        {
            // Время для точки i, рассчитываем по её индексу с учётом динамического расстояния
            float time = i * dynamicDistance / speed;

            // Параболическое движение
            float x = transform.position.x + normalizedDirection.x * speed * time;
            float y = transform.position.y + normalizedDirection.y * speed * time + 0.5f * _gravity * time * time;
            float z = transform.position.z + normalizedDirection.z * speed * time;

            _points[i].transform.position = new Vector3(x, y, z);
        }
    }

    
    private void OnDisable()
    {
        _shootPoint.Trajectory += DrawLine;
    }
}