using UnityEngine;

namespace CodeBase.Bird
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BirdCollisionNotifier))]
    public abstract class Bird : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody { get; private set; }
        public BirdCollisionNotifier CollisionNotifier { get; private set; }
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Rigidbody.isKinematic = true;
            CollisionNotifier = GetComponent<BirdCollisionNotifier>();
        }

        public void Launch(Vector3 direction)
        {
            Rigidbody.isKinematic = false;
            Rigidbody.AddForce(direction, ForceMode2D.Impulse);
        }
    }
}