using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Control
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private PlayerLook playerLook;
        [SerializeField] private Rigidbody playerRigidBody;

        [Header("Parameters")]

        [Header("Input")]
        [SerializeField] private KeyCode moveForward = KeyCode.W;
        [SerializeField] private KeyCode moveBackward = KeyCode.S;
        [SerializeField] private KeyCode strafeLeft = KeyCode.A;
        [SerializeField] private KeyCode strafeRight = KeyCode.D;

        [Header("Movement")]
        [SerializeField] private float movementSpeed;
        [SerializeField] private float acceleration;

        private Vector3 currentVelocity;

        private float horizontalInput;
        private float verticalInput;

        void Update()
        {
            UpdateInput();

            Move(GetMoveDir());
        }

        private void Move(Vector3 dir)
        {
            Vector3 inputDir = dir * verticalInput + playerTransform.right * horizontalInput;
            currentVelocity = Vector3.Lerp(currentVelocity, inputDir * movementSpeed, acceleration * Time.deltaTime);

            playerRigidBody.velocity = currentVelocity;
        }

        private void UpdateInput()
        {
            if (Input.GetKey(strafeLeft))
            {
                horizontalInput = -1.0f;
            }
            else if (Input.GetKey(strafeRight))
            {
                horizontalInput = 1.0f;
            }
            else
            {
                horizontalInput = 0.0f;
            }

            if (Input.GetKey(moveForward))
            {
                verticalInput = 1.0f;
            }
            else if (Input.GetKey(moveBackward))
            {
                verticalInput = -1.0f;
            }
            else
            {
                verticalInput = 0.0f;
            }
        }

        private Vector3 GetMoveDir()
        {
            return playerLook.GetLookDir();
        }
    }
}
