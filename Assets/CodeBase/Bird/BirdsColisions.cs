using System;
using CodeBase.BaseMaterial;
using UnityEngine;

namespace CodeBase.Bird
{
    public class BirdCollisionNotifier : MonoBehaviour
    {
        public event Action<BasePigMaterial, float> OnBirdCollision;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent<BasePigMaterial>(out var basePigMaterial))
            {
                OnBirdCollision?.Invoke(basePigMaterial, other.relativeVelocity.magnitude);
            }
        }
    }
}