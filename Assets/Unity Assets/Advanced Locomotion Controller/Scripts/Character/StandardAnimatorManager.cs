using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    public class StandardAnimatorManager : MonoBehaviour
    {
        [Header("Character Blend")]
        public float Acceleration = 2.0f;
        public float Deceleration = 2.0f;
        float maximumWalkVelocity = 0.5f;
        float maximumRunVelocity = 1.0f;
        float velocityZ = 0.0f;
        float velocityX = 0.0f;

        private Animator PlayerLocomotionController;
        private PlayerInput PlayerInput;
        private CharacterMovement controller;

        //States
        bool forwardPressed;
        bool backwardPressed;
        bool leftPressed;
        bool rightPressed;
        bool sprintPressed;
        bool Sliding;
        bool Analog;
        bool StrafeRun;
        float currentMaxVelocity;
        bool hasSlid;

        private void Start()
        {
            PlayerLocomotionController = GetComponent<Animator>();
            PlayerInput = GetComponentInParent<PlayerInput>();
            controller = GetComponent<CharacterMovement>();
        }

        private void Update()
        {
            if (controller.MenuOpen) { return; }
            UpdateStates();
            if (PlayerInput.Crouch && !Sliding && !controller.IsCovering)
            {
                HandleCrouchAnimations();
            }
            else if (Sliding)
            {
                if (hasSlid) { return; }
                if (PlayerLocomotionController.GetBool("IsSliding"))
                {
                    PlayerLocomotionController.SetBool("IsSliding", false);
                    hasSlid = true;
                }
                else
                {
                    PlayerLocomotionController.SetBool("IsSliding", true);
                }
            }
            else
            {
                HandleNormalAnimations();
            }
        }
        private void UpdateStates()
        {
            forwardPressed = PlayerInput.MoveInput.y > 0;
            backwardPressed = PlayerInput.MoveInput.y < 0;
            leftPressed = PlayerInput.MoveInput.x < 0;
            rightPressed = PlayerInput.MoveInput.x > 0;
            sprintPressed = PlayerInput.Sprint;
            Analog = controller.Analog;
            StrafeRun = controller.StrafeRun;
            Sliding = controller.Sliding && ((PlayerInput.Crouch && sprintPressed && forwardPressed) || (controller.Analog && (forwardPressed || backwardPressed || leftPressed || rightPressed) && sprintPressed && PlayerInput.Crouch));
            if (!StrafeRun && !Analog)
            {
                currentMaxVelocity = sprintPressed && !PlayerInput.Crouch && ((!leftPressed && !rightPressed) || (forwardPressed || backwardPressed)) ? maximumRunVelocity : maximumWalkVelocity;
            }
            else
            {
                currentMaxVelocity = sprintPressed && !PlayerInput.Crouch ? maximumRunVelocity : maximumWalkVelocity;
            }
        }
        private void HandleNormalAnimations()
        {
            ChangeVelocity();
            ResetVelocity();

            if (Analog && !controller.IsCovering)
            {
                if ((forwardPressed || leftPressed || rightPressed || backwardPressed) && sprintPressed && velocityZ > currentMaxVelocity)
                {
                    velocityZ = currentMaxVelocity;
                }
                else if ((forwardPressed || leftPressed || rightPressed || backwardPressed) && velocityZ > currentMaxVelocity)
                {
                    velocityZ -= Time.deltaTime * Deceleration;
                    if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.1f))
                    {
                        velocityZ = currentMaxVelocity;
                    }
                }
                else if ((forwardPressed || leftPressed || rightPressed || backwardPressed) && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.1f))
                {
                    velocityZ = currentMaxVelocity;
                }
            }
            else
            {
                //Capping Forward Velocity
                if (forwardPressed && sprintPressed && velocityZ > currentMaxVelocity)
                {
                    velocityZ = currentMaxVelocity;
                }
                else if (forwardPressed && velocityZ > currentMaxVelocity)
                {
                    velocityZ -= Time.deltaTime * Deceleration;
                    if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
                    {
                        velocityZ = currentMaxVelocity;
                    }
                }
                else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
                {
                    velocityZ = currentMaxVelocity;

                }

                //Capping Backward Velocity
                if (backwardPressed && sprintPressed && velocityZ < -currentMaxVelocity)
                {
                    velocityZ = -currentMaxVelocity;
                }
                else if (backwardPressed && velocityZ < -currentMaxVelocity)
                {
                    velocityZ += Time.deltaTime * Deceleration;
                    if (velocityZ < -currentMaxVelocity && velocityZ > (-currentMaxVelocity - 0.05f))
                    {
                        velocityZ = -currentMaxVelocity;
                    }
                }
                else if (backwardPressed && velocityZ > -currentMaxVelocity && velocityZ < (-currentMaxVelocity + 0.05f))
                {
                    velocityZ = -currentMaxVelocity;
                }
                //Capping Right Velocity
                if (rightPressed && sprintPressed && velocityX > currentMaxVelocity)
                {
                    velocityX = currentMaxVelocity;
                }
                else if (rightPressed && velocityX > currentMaxVelocity)
                {
                    velocityX -= Time.deltaTime * Deceleration;
                    if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
                    {
                        velocityX = currentMaxVelocity;
                    }
                }
                else if (rightPressed && velocityX < currentMaxVelocity && velocityX > (currentMaxVelocity - 0.05f))
                {
                    velocityX = currentMaxVelocity;
                }

                //Capping Left Velocity
                if (leftPressed && sprintPressed && velocityX < -currentMaxVelocity)
                {
                    velocityX = -currentMaxVelocity;
                }
                else if (leftPressed && velocityX < -currentMaxVelocity)
                {
                    velocityX += Time.deltaTime * Deceleration;
                    if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
                    {
                        velocityX = -currentMaxVelocity;
                    }
                }
                else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
                {
                    velocityX = -currentMaxVelocity;
                }
            }

            PlayerLocomotionController.SetBool("IsCrouching", false);
            hasSlid = false;
            PlayerLocomotionController.SetBool("IsSliding", false);
            PlayerLocomotionController.SetFloat("StandingVelocityX", velocityX);
            PlayerLocomotionController.SetFloat("StandingVelocityZ", velocityZ);
        }
        private void HandleCrouchAnimations()
        {
            if (Analog)
            {
                if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && velocityZ < currentMaxVelocity)
                {
                    velocityZ += Time.deltaTime * Acceleration;
                }
            }
            else
            {
                //Increase VelocityX and VelocityZ
                if (forwardPressed && velocityZ < 0.5f)
                {
                    velocityZ += Time.deltaTime * Acceleration / 2;
                }
                if (backwardPressed && velocityZ > -0.5f)
                {
                    velocityZ -= Time.deltaTime * Acceleration / 2;
                }
                if (leftPressed && velocityX > -0.5f)
                {
                    velocityX -= Time.deltaTime * Acceleration / 2;
                }
                if (rightPressed && velocityX < 0.5f)
                {
                    velocityX += Time.deltaTime * Acceleration / 2;
                }
            }

            ResetVelocity();
            PlayerLocomotionController.SetBool("IsCrouching", true);
            hasSlid = false;
            PlayerLocomotionController.SetBool("IsSliding", false);
            PlayerLocomotionController.SetFloat("CrouchingVelocityX", velocityX);
            PlayerLocomotionController.SetFloat("CrouchingVelocityZ", velocityZ);
        }
        private void ChangeVelocity()
        {
            if (Analog && !controller.IsCovering)
            {
                if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && velocityZ < currentMaxVelocity)
                {
                    velocityZ += Time.deltaTime * Acceleration;
                }
                velocityX = 0;
            }
            else
            {
                //Increase VelocityX and VelocityZ
                if (forwardPressed && velocityZ < currentMaxVelocity)
                {
                    velocityZ += Time.deltaTime * Acceleration;
                }
                if (backwardPressed && velocityZ > -currentMaxVelocity)
                {
                    velocityZ -= Time.deltaTime * Acceleration;
                }
                if (leftPressed && velocityX > -currentMaxVelocity)
                {
                    velocityX -= Time.deltaTime * Acceleration;
                }
                if (rightPressed && velocityX < currentMaxVelocity)
                {
                    velocityX += Time.deltaTime * Acceleration;
                }
            }
        }
        private void ResetVelocity()
        {
            if (Analog && !controller.IsCovering)
            {
                if (!forwardPressed && !backwardPressed && !leftPressed && !rightPressed && velocityZ > 0.0f)
                {
                    velocityZ -= Time.deltaTime * Deceleration;
                }
                if (!forwardPressed && !backwardPressed && !leftPressed && !rightPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
                {
                    velocityZ = 0.0f;
                }
                velocityX = 0;
            }
            else
            {
                //Decrease VelocityZ
                if (!forwardPressed && velocityZ > 0.0f)
                {
                    velocityZ -= Time.deltaTime * Deceleration;
                }
                if (!backwardPressed && velocityZ < 0.0f)
                {
                    velocityZ += Time.deltaTime * Deceleration;
                }
                if (!forwardPressed && !backwardPressed && velocityZ != 0.0f && (velocityZ > -0.05f && velocityZ < 0.05f))
                {
                    velocityZ = 0.0f;
                }

                //Decrease VelocityX
                if (!rightPressed && velocityX > 0.0f)
                {
                    velocityX -= Time.deltaTime * Deceleration;
                }
                if (!leftPressed && velocityX < 0.0f)
                {
                    velocityX += Time.deltaTime * Deceleration;
                }
                if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
                {
                    velocityX = 0.0f;
                }
            }
        }
    }
}
