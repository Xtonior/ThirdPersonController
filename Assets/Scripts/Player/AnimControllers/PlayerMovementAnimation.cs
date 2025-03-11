using System;
using System.Collections;
using System.Collections.Generic;
using Player.Control;
using UnityEngine;

namespace Player.Animations
{
    public class PlayerMovementAnimation : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Animator animator;

        [Header("Animator Parameters")]
        [SerializeField] private string animatorVerticalMovementId;
        [SerializeField] private string animatorHorizontalMovementId;
        [SerializeField] private string animatorisIdleId;

        [Header("Animation Parameters")]
        [SerializeField] private float minVelocity;

        private Vector2 input;
        private Vector2 velocity;

        void Update()
        {
            UpdateInputs();

            bool isMove = Mathf.Abs(velocity.x) > minVelocity || Mathf.Abs(velocity.y) > minVelocity;

            if (isMove)
            {
                animator.SetBool(animatorisIdleId, false);

                animator.SetFloat(animatorVerticalMovementId, velocity.y);
                animator.SetFloat(animatorHorizontalMovementId, velocity.x);
            }
            else
            {
                animator.SetBool(animatorisIdleId, true);
            }
        }

        private void UpdateInputs()
        {
            velocity = Vector2.ClampMagnitude(new Vector2(playerMovement.GetVelocity().x, playerMovement.GetVelocity().z), 1.0f);

            Vector2 forward = new Vector2(playerMovement.GetForward().x, playerMovement.GetForward().z);
            Vector2 right = new Vector2(playerMovement.GetRight().x, playerMovement.GetRight().z);

            Vector2 dir;
            dir.x = Vector2.Dot(velocity, forward);
            dir.y = Vector2.Dot(velocity, right);

            velocity = dir;
        }
    }
}