using System;
using CodeBase.Slingshot;
using UnityEngine;

public class DynamicLineFromDirection : MonoBehaviour
{
    [SerializeField] private GameObject _pointPrefab;
    [SerializeField] private int _pointCount = 5;
    [SerializeField] private ShootPoint _shootPoint;
    [SerializeField] private float trajectorySpacing = 2f;
    private GameObject[] _points;

    private void OnEnable()
    {
        _shootPoint.Trajectory += DrawLine;
        _shootPoint.Release += ShootPointOnRelease;
    }

    private void ShootPointOnRelease(Vector3 obj)
    {
        foreach (var point in _points)
        {
            point.SetActive(false);
        }
    }

    private void Start()
    {
        _points = new GameObject[_pointCount];

        for (int i = 0; i < _pointCount; i++)
        {
            _points[i] = Instantiate(_pointPrefab, transform.position, Quaternion.identity);
            _points[i].SetActive(false);
        }
    }

    private void DrawLine(Vector3 direction, float tensionStrength)
    {
        Vector3 normalizedDirection = direction.normalized;
        float initialSpeed = 30f * tensionStrength;
        float gravity = Mathf.Abs(Physics.gravity.y);

        // Увеличиваем расстояние между точками по коэффициенту `trajectorySpacing`
        float dynamicDistance = Mathf.Max(0.5f, 0.5f * tensionStrength) * trajectorySpacing;

        for (int i = 0; i < _pointCount; i++)
        {
            float time = i * dynamicDistance / initialSpeed;

            float x = _shootPoint.transform.position.x + normalizedDirection.x * initialSpeed * time;
            float y = _shootPoint.transform.position.y + normalizedDirection.y * initialSpeed * time - 0.5f * gravity * time * time;
            float z = _shootPoint.transform.position.z + normalizedDirection.z * initialSpeed * time;

            _points[i].transform.position = new Vector3(x, y, z);
            _points[i].SetActive(true);
        }
    }

    
    private void OnDisable()
    {
        _shootPoint.Trajectory += DrawLine;
        _shootPoint.Release -= ShootPointOnRelease;
    }
}