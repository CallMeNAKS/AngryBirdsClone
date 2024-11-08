using System;
using UnityEngine;

namespace CodeBase.Bird
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpringJoint2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Bird : MonoBehaviour
    {
        protected Rigidbody2D Rigidbody { get; private set; }
        private SpringJoint2D _springJoint2D;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            _springJoint2D = GetComponent<SpringJoint2D>();
        }

        public void SetConnectedBody(Slingshot.Slingshot slingshot)
        {
            _springJoint2D.connectedBody = slingshot.GetComponent<Rigidbody2D>();
        }

        public abstract void AddForce(Vector2 force);
    }
}