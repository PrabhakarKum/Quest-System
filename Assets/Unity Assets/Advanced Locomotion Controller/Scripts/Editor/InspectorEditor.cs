using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(CharacterMovement))]
    public class InspectorEditor : Editor
    {
        public bool MovementSettings, VaultingSettings, CoverSettings, CustomizableSettings, ComponentsSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CharacterMovement controller = (CharacterMovement)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Character Movement", Title());
            EditorGUILayout.EndHorizontal();

            MovementSettings = GUILayout.Toggle(MovementSettings, "✥ Movement", BoxStyle());
            MovementSettingsVars(controller);

            VaultingSettings = GUILayout.Toggle(VaultingSettings, "░  Vaulting", BoxStyle());
            VaultingSettingsVars(controller);

            CoverSettings = GUILayout.Toggle(CoverSettings, "▩ Cover", BoxStyle());
            CoverSettingsVars(controller);

            CustomizableSettings = GUILayout.Toggle(CustomizableSettings, "☼ Customizable", BoxStyle());
            CustomizableSettingsVars(controller);

            ComponentsSettings = GUILayout.Toggle(ComponentsSettings, "⊕ Components", BoxStyle());
            ComponenetsSettingsVars(controller);

            serializedObject.ApplyModifiedProperties();
        }
        void MovementSettingsVars(CharacterMovement controller)
        {
            if (MovementSettings)
            {
                EditorGUILayout.LabelField("Move Speeds", Text());
                serializedObject.FindProperty("WalkSpeed").floatValue = EditorGUILayout.Slider("Walk Speed", controller.WalkSpeed, 0, 10);
                serializedObject.FindProperty("CrouchSpeed").floatValue = EditorGUILayout.Slider("Crouch Speed", controller.CrouchSpeed, 0, 10);
                serializedObject.FindProperty("SprintSpeed").floatValue = EditorGUILayout.Slider("Sprint Speed", controller.SprintSpeed, 0, 10);
                serializedObject.FindProperty("SlideSpeed").floatValue = EditorGUILayout.Slider("Slide Speed", controller.SlideSpeed, 0, 10);
                serializedObject.FindProperty("WallSpeed").floatValue = EditorGUILayout.Slider("Wall Speed", controller.WallSpeed, 0, 10);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Move Options", Text());
                serializedObject.FindProperty("jumpHeight").floatValue = EditorGUILayout.Slider("Jump Height", controller.jumpHeight, 0, 10);
                serializedObject.FindProperty("BackwardsSpeedDropOff").floatValue = EditorGUILayout.Slider("Backward Speed Drop Off", controller.BackwardsSpeedDropOff, 0, 3);
                serializedObject.FindProperty("Gravity").floatValue = EditorGUILayout.Slider("Gravity", controller.Gravity, 0, -30);
                serializedObject.FindProperty("LedgeHeightFallRoll").floatValue = EditorGUILayout.Slider("Ledge Height Fall Roll", controller.LedgeHeightFallRoll, 0, 20);
                serializedObject.FindProperty("groundDistance").floatValue = EditorGUILayout.Slider("Ground Distance", controller.groundDistance, 0, 5);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Rotation Speeds", Text());
                serializedObject.FindProperty("turnSpeed").floatValue = EditorGUILayout.Slider("Turn Speed", controller.turnSpeed, 0, 30);
                serializedObject.FindProperty("turnSmoothTime").floatValue = EditorGUILayout.Slider("Turn Smooth Time", controller.turnSmoothTime, 0, 3);
                EditorGUILayout.Space();
            }
        }
        void VaultingSettingsVars(CharacterMovement controller)
        {
            if (VaultingSettings)
            {
                EditorGUILayout.LabelField("Vault Logic", Text());
                serializedObject.FindProperty("distanceToCheckForward").floatValue = EditorGUILayout.Slider("Distance To Check Forward", controller.distanceToCheckForward, 0, 3);
                serializedObject.FindProperty("vaultOverHeight").floatValue = EditorGUILayout.Slider("Vault Over Height", controller.vaultOverHeight, 0, 5);
                serializedObject.FindProperty("vaultFloorHeighDifference").floatValue = EditorGUILayout.Slider("Vault Floor Height Difference", controller.vaultFloorHeighDifference, 0, 3);
                serializedObject.FindProperty("vaultCheckDistance").floatValue = EditorGUILayout.Slider("Vault Check Distance", controller.vaultCheckDistance, 0, 10);
                serializedObject.FindProperty("VaultCurve").animationCurveValue = EditorGUILayout.CurveField("Vault Curve", controller.VaultCurve);
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Climb Logic", Text());
                serializedObject.FindProperty("climbMaxHeight").floatValue = EditorGUILayout.Slider("Climb Max Height", controller.climbMaxHeight, 0, 10);
                serializedObject.FindProperty("walkUpHeight").floatValue = EditorGUILayout.Slider("Walk Up Height", controller.walkUpHeight, 0, 10);
                serializedObject.FindProperty("WalkUpThreshold").floatValue = EditorGUILayout.Slider("Walk Up Threshold", controller.WalkUpThreshold, 0, 10);
                serializedObject.FindProperty("WalkUpCurve").animationCurveValue = EditorGUILayout.CurveField("Walk Up Curve", controller.WalkUpCurve);
                LayerMask tempMask = EditorGUILayout.MaskField("Ground Layer", UnityEditorInternal.InternalEditorUtility.LayerMaskToConcatenatedLayersMask(controller.GroundMask), UnityEditorInternal.InternalEditorUtility.layers);
                serializedObject.FindProperty("GroundMask").intValue = UnityEditorInternal.InternalEditorUtility.ConcatenatedLayersMaskToLayerMask(tempMask);
                EditorGUILayout.Space();
            }
        }
        void CoverSettingsVars(CharacterMovement controller)
        {
            if (CoverSettings)
            {
                EditorGUILayout.LabelField("Cover Logic", Text());
                serializedObject.FindProperty("wallDistance").floatValue = EditorGUILayout.Slider("Wall Distance", controller.wallDistance, 0, 3);

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Wall-Running Logic", Text());
                serializedObject.FindProperty("minimumJumpHeight").floatValue = EditorGUILayout.Slider("Minimum Jump Height", controller.minimumJumpHeight, 0, 5);
                serializedObject.FindProperty("wallRunGravity").floatValue = EditorGUILayout.Slider("Wall Run Gravity", controller.wallRunGravity, 0, 10);
                serializedObject.FindProperty("wallRunJumpForce").floatValue = EditorGUILayout.Slider("Wall Run Jump Force", controller.wallRunJumpForce, 0, 5);
                EditorGUILayout.Space();
            }
        }
        void CustomizableSettingsVars(CharacterMovement controller)
        {
            if (CustomizableSettings)
            {
                EditorGUILayout.LabelField("Customizable Settings", Text());
                serializedObject.FindProperty("AutoRotation").boolValue = EditorGUILayout.Toggle("Auto Rotation", controller.AutoRotation);
                serializedObject.FindProperty("Analog").boolValue = EditorGUILayout.Toggle("Analog", controller.Analog);
                serializedObject.FindProperty("StrafeRun").boolValue = EditorGUILayout.Toggle("Strafe Run", controller.StrafeRun);
                serializedObject.FindProperty("Covering").boolValue = EditorGUILayout.Toggle("Covering", controller.Covering);
                serializedObject.FindProperty("Sliding").boolValue = EditorGUILayout.Toggle("Sliding", controller.Sliding);
                serializedObject.FindProperty("Vaulting").boolValue = EditorGUILayout.Toggle("Vaulting", controller.Vaulting);
                serializedObject.FindProperty("WallRunning").boolValue = EditorGUILayout.Toggle("Wall Running", controller.WallRunning);
                serializedObject.FindProperty("AutoStep").boolValue = EditorGUILayout.Toggle("Auto Step", controller.AutoStep);
                serializedObject.FindProperty("LandingRoll").boolValue = EditorGUILayout.Toggle("Landing Roll", controller.LandingRoll);
                serializedObject.FindProperty("Footsteps").boolValue = EditorGUILayout.Toggle("Footsteps", controller.Footsteps);
                EditorGUILayout.Space();
            }
        }
        void ComponenetsSettingsVars(CharacterMovement controller)
        {
            if (ComponentsSettings)
            {
                EditorGUILayout.LabelField("Components", Text());
                serializedObject.FindProperty("Cam").objectReferenceValue = EditorGUILayout.ObjectField("Camera", controller.Cam, typeof(Transform), true) as Transform;
                serializedObject.FindProperty("GroundCheck").objectReferenceValue = EditorGUILayout.ObjectField("Ground Check", controller.GroundCheck, typeof(Transform), true) as Transform;
                serializedObject.FindProperty("Orientation").objectReferenceValue = EditorGUILayout.ObjectField("Orientation", controller.Orientation, typeof(Transform), true) as Transform;
                serializedObject.FindProperty("MenuPanel").objectReferenceValue = EditorGUILayout.ObjectField("Menu Panel", controller.MenuPanel, typeof(GameObject), true) as GameObject;
            }
        }
        public static GUIStyle BoxStyle()
        {
            GUIStyle style = new GUIStyle(EditorStyles.objectField);
            style.font = Resources.Load("TitilliumWeb-Bold") as Font;
            style.fontSize = 16;
            style.normal.textColor = Color.white;
            style.fontStyle = FontStyle.Normal;
            return style;
        }
        public static GUIStyle Title()
        {
            GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
            titleStyle.font = Resources.Load("TitilliumWeb-Bold") as Font;
            titleStyle.fontSize = 18;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleLeft;
            return titleStyle;
        }
        public static GUIStyle Text()
        {
            GUIStyle titleStyle = new GUIStyle(EditorStyles.label);
            titleStyle.font = Resources.Load("TitilliumWeb-Bold") as Font;
            titleStyle.fontSize = 14;
            titleStyle.normal.textColor = Color.white;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleLeft;
            return titleStyle;
        }
    }
}
