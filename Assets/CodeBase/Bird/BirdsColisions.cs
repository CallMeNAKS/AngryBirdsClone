using System;
using System.Collections;
using CodeBase.BaseMaterial;
using UnityEngine;

namespace CodeBase.Bird
{
    public class BirdCollisionNotifier : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        private bool _isCheckingStopped;
        public event Action<BasePigMaterial, float> OnBirdCollision;
        public event Action BirdStopped;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BasePigMaterial>(out var basePigMaterial))
            {
                OnBirdCollision?.Invoke(basePigMaterial, other.relativeVelocity.magnitude);
            }
            
            if (!_isCheckingStopped)
            {
                StartCoroutine(CheckIfStopped());
            }
        }

        private IEnumerator CheckIfStopped()
        {
            _isCheckingStopped = true;

            while (_rigidbody.velocity.magnitude > 0)
            {
                yield return new WaitForSeconds(0.1f);
            }
            
            BirdStopped?.Invoke();
            _isCheckingStopped = false;
        }
    }
}