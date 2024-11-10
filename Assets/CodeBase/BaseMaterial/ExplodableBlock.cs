using System;
using UnityEngine;

namespace CodeBase.BaseMaterial
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExplodableBlock : MonoBehaviour
    {
        // [SerializeField] private GameObject _explosionEffect; // Префаб взрыва
        [SerializeField] private float _explosionRadius = 5f;
        [SerializeField] private float _explosionForce = 500f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.magnitude > 1f)
            {
                Explode();  
            }
        }

        public void Explode()
        {
            // Instantiate(_explosionEffect, transform.position, Quaternion.identity);
            ApplyExplosionForce();
            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Explode();
            }
        }

        private void ApplyExplosionForce()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _explosionRadius);
            foreach (var collider in colliders)
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    Vector2 direction = (rb.transform.position - transform.position).normalized;
                    rb.AddForce(direction * _explosionForce);
                }
            }
        }
    }
}