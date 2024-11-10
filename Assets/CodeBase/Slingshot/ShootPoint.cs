using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CodeBase.Slingshot
{
    public class ShootPoint : MonoBehaviour
    {
        [SerializeField] private float maxDistance = 2f;
        [Header("Debug")] [SerializeField] private Vector3 start;
        public event Action<Vector3> Release;
        public event Action<Vector3, float> Trajectory;
        
        private new Camera camera;

        private void Start()
        {
            camera = Camera.main;
            start = transform.position;
        }

        private void OnMouseDrag()
        {
            if (!enabled)
                return;

            var target = camera.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            if (Vector3.Distance(start, target) < maxDistance)
            {
                transform.position = target;
            }
            else
            {
                var direction = (target - start).normalized * maxDistance;
                transform.position = start + direction;
            }

            var directionToStart = start - transform.position; //TODO
            float tensionStrength = Mathf.Clamp01(directionToStart.magnitude / maxDistance);

            Trajectory?.Invoke(directionToStart, tensionStrength);
        }
        
        private void OnMouseUp()
        {
            var releasePosition = transform.position;
            transform.position = start;
            var delta = releasePosition - start;
            Release?.Invoke(delta.normalized * (delta.magnitude / maxDistance));
        }
    }
}