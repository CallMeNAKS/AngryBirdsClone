using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Pig
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Pig : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        public event Action Death;
        
        [SerializeField] private float _deathForce = 1f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.rigidbody != null)
            {
                float impactForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;

                if (impactForce > _deathForce)
                {
                    PigDeath();
                }
            }
        }

        private void PigDeath()
        {
            Death?.Invoke();
            Camera.main.DOShakePosition(2f, .2f); //TODO 
            gameObject.SetActive(false);
        }
    }
}