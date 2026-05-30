using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    public class AdvancedFootIK : MonoBehaviour
    {
        private Vector3 rightFootPosition, leftFootPosition, leftFootIKPosition, rightFootIKPosition;
        private Quaternion leftFootIKRotation, rightFootIKRotation;
        private float lastPelvisPositionY, lastRightFootPositionY, lastLeftFootPositionY;
        private LayerMask enviromentLayer;
        private Animator animator;
        private CharacterMovement controller;
        private CharacterController ControllerMain;

        [Header("Foot Grounder")]
        public bool EnableFootIK = true;
        public bool EnableProIKFeature = true;
        public bool showSolverDebug = true;

        [Header("Foot IK Values")]
        [Range(0, 2)] public float HeightRaycast = 1.14f;
        [Range(0, 2)] public float RaycastDownDistance = 1.5f;
        public float pelvisOffset = 0f;
        [Range(0, 1)] public float pelvisUpAndDownSpeed = 0.05f;
        [Range(0, 1)] public float FootToIKPositionSpeed = 0.05f;
        public string leftFootAnimVariableName = "LeftFootCurve";
        public string rightFootAnimVariableName = "RightFootCurve";

        void Start()
        {
            controller = GetComponent<CharacterMovement>();
            ControllerMain = GetComponent<CharacterController>();
            enviromentLayer = controller.GroundMask;
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            if (controller.IsClimbUp || controller.IsJumping || controller.IsVaulting || controller.IsSliding || controller.SteepSlope) { EnableFootIK = false; } else { if (!EnableFootIK) { Delay(); } }
            if (animator == null) { return; }

            AdjustFeetTarget(ref rightFootPosition, HumanBodyBones.RightFoot);
            AdjustFeetTarget(ref leftFootPosition, HumanBodyBones.LeftFoot);

            FeetPositionSolver(rightFootPosition, ref rightFootIKPosition, ref rightFootIKRotation);
            FeetPositionSolver(leftFootPosition, ref leftFootIKPosition, ref leftFootIKRotation);

        }
        void Delay()
        {
            lastPelvisPositionY = 0;
            lastLeftFootPositionY = 0;
            lastRightFootPositionY = 0;
            EnableFootIK = true;
        }
        private void OnAnimatorIK(int layerIndex)
        {
            if (EnableFootIK == false) { return; }
            if (animator == null) { return; }

            MovePelvisHeight();
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);

            if (EnableProIKFeature)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, animator.GetFloat(rightFootAnimVariableName));
            }

            MoveFeetToIKPoint(AvatarIKGoal.RightFoot, rightFootIKPosition, rightFootIKRotation, ref lastRightFootPositionY);

            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);

            if (EnableProIKFeature)
            {
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, animator.GetFloat(leftFootAnimVariableName));
            }

            MoveFeetToIKPoint(AvatarIKGoal.LeftFoot, leftFootIKPosition, leftFootIKRotation, ref lastLeftFootPositionY);
        }

        void MoveFeetToIKPoint(AvatarIKGoal foot, Vector3 positionIKHolder, Quaternion rotationIKHolder, ref float lastFootPositionY)
        {
            Vector3 targetIKPosition = animator.GetIKPosition(foot);

            if (positionIKHolder != Vector3.zero)
            {
                targetIKPosition = transform.InverseTransformPoint(targetIKPosition);
                positionIKHolder = transform.InverseTransformPoint(positionIKHolder);

                float yVariable = Mathf.Lerp(lastFootPositionY, positionIKHolder.y, FootToIKPositionSpeed);
                targetIKPosition.y += yVariable;

                lastFootPositionY = yVariable;

                targetIKPosition = transform.TransformPoint(targetIKPosition);

                animator.SetIKRotation(foot, rotationIKHolder);
            }
            animator.SetIKPosition(foot, targetIKPosition);
        }

        private void MovePelvisHeight()
        {
            if (rightFootIKPosition == Vector3.zero || leftFootIKPosition == Vector3.zero || lastPelvisPositionY == 0)
            {
                lastPelvisPositionY = animator.bodyPosition.y;
                return;
            }

            float lOffsetPosition = leftFootIKPosition.y - transform.position.y;
            float rOffsetPosition = rightFootIKPosition.y - transform.position.y;

            float totalOffset = (lOffsetPosition < rOffsetPosition) ? lOffsetPosition : rOffsetPosition;
            Vector3 newPelvisPosition = animator.bodyPosition + Vector3.up * totalOffset;
            newPelvisPosition.y = Mathf.Lerp(lastPelvisPositionY, newPelvisPosition.y, pelvisUpAndDownSpeed);
            animator.bodyPosition = newPelvisPosition;
            lastPelvisPositionY = animator.bodyPosition.y;
        }
        private void FeetPositionSolver(Vector3 fromSkyPosition, ref Vector3 feetIKPosition, ref Quaternion feetIKRotation)
        {
            RaycastHit feetOutHit;

            if (showSolverDebug)
            {
                Debug.DrawLine(fromSkyPosition, fromSkyPosition + Vector3.down * (RaycastDownDistance + HeightRaycast), Color.red);
            }
            if (Physics.Raycast(fromSkyPosition, Vector3.down, out feetOutHit, RaycastDownDistance + HeightRaycast, enviromentLayer))
            {
                feetIKPosition = fromSkyPosition;
                feetIKPosition.y = feetOutHit.point.y + pelvisOffset;
                feetIKRotation = Quaternion.FromToRotation(Vector3.up, feetOutHit.normal) * transform.rotation;
                return;
            }

            feetIKPosition = Vector3.zero;
        }
        private void AdjustFeetTarget(ref Vector3 feetPosition, HumanBodyBones foot)
        {
            feetPosition = animator.GetBoneTransform(foot).position;
            feetPosition.y = transform.position.y + HeightRaycast;
        }
    }
}