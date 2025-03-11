using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Control
{
    public class PlayerLook : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerCameraCollision playerCameraCollision;
        [SerializeField] private Transform playerRoot;
        [SerializeField] private Transform orientationBody;
        [SerializeField] private Transform aimPoint;

        [Header("Parameters")]
        [SerializeField] private float sensitivityX;
        [SerializeField] private float sensitivityY;
        [SerializeField] private float offsetSmoothSpeed;

        private Vector2 input;
        private float rotX;
        private float rotY;

        void Update()
        {
            UpdateInput();

            RotateCamera();
            MoveCamera();

            RotateRootPlayer();
        }

        public Vector3 GetLookDir() => orientationBody.forward;

        private void RotateCamera()
        {            
            rotX -= input.y;
            rotY += input.x;

            rotX = Mathf.Clamp(rotX, -30.0f, 80.0f);

            Quaternion rot = Quaternion.AngleAxis(rotY, playerRoot.up) * Quaternion.AngleAxis(rotX, playerRoot.right);

            transform.rotation = rot;
        }

        private void MoveCamera()
        {
            float offset = playerCameraCollision.GetOffset(aimPoint.position);
            Vector3 targetPos = aimPoint.position + (-transform.forward * offset);
            // transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * offsetSmoothSpeed);
            transform.position = targetPos;
        }

        private void RotateRootPlayer()
        {
            orientationBody.rotation = Quaternion.AngleAxis(rotY, playerRoot.up);
        }

        private void UpdateInput()
        {
            float x = Input.GetAxisRaw("Mouse X") * sensitivityX;
            float y = Input.GetAxisRaw("Mouse Y") * sensitivityY;

            input = new Vector2(x, y);
        }
    }
}
