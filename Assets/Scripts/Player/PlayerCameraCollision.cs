using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Control
{
    public class PlayerCameraCollision : MonoBehaviour
    {
        [SerializeField] private LayerMask collisionMask;
        [SerializeField] private float cameraDistance = 5f;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 7f;

        private float defaultOffset;
        private float currentOffset;

        void Start()
        {
            defaultOffset = cameraDistance;
            currentOffset = defaultOffset;
        }

        public float GetOffset(Vector3 playerCenter)
        {
            if (Physics.SphereCast(playerCenter, 0.3f, -transform.forward, out RaycastHit hit, cameraDistance, collisionMask))
            {
                float hitDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
                currentOffset = hitDistance;
            }
            else
            {
                currentOffset = defaultOffset;
            }

            return currentOffset;
        }
    }
}