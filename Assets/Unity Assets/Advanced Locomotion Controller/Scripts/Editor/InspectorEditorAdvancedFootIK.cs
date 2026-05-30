using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(AdvancedFootIK))]
    public class InspectorEditorAdvancedFootIK : Editor
    {
        public bool IkValueSettings, CustomizableSettings, AdvancedCurveSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            AdvancedFootIK IK = (AdvancedFootIK)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Advanced Foot IK", Title());
            EditorGUILayout.EndHorizontal();

            IkValueSettings = GUILayout.Toggle(IkValueSettings, "⊕ IK Values", BoxStyle());
            IKValueSettingsVars(IK);

            AdvancedCurveSettings = GUILayout.Toggle(AdvancedCurveSettings, "∿  Advanced Curve", BoxStyle());
            AdvancedCurveSettingsVars(IK);

            CustomizableSettings = GUILayout.Toggle(CustomizableSettings, "☼ Customizable", BoxStyle());
            CustomizableSettingsVars(IK);

            serializedObject.ApplyModifiedProperties();
        }
        void IKValueSettingsVars(AdvancedFootIK IK)
        {
            if (IkValueSettings)
            {
                EditorGUILayout.LabelField("IK Values", Text());
                serializedObject.FindProperty("HeightRaycast").floatValue = EditorGUILayout.Slider("Height Raycast", IK.HeightRaycast, 0, 2);
                serializedObject.FindProperty("RaycastDownDistance").floatValue = EditorGUILayout.Slider("Raycast Down Distance", IK.RaycastDownDistance, 0, 5);
                serializedObject.FindProperty("pelvisOffset").floatValue = EditorGUILayout.Slider("Pelvis Offset", IK.pelvisOffset, 0, 5);
                serializedObject.FindProperty("pelvisUpAndDownSpeed").floatValue = EditorGUILayout.Slider("Pelvis Move Speed", IK.pelvisUpAndDownSpeed, 0, 2);
                serializedObject.FindProperty("FootToIKPositionSpeed").floatValue = EditorGUILayout.Slider("Foot IK MoveSpeed", IK.FootToIKPositionSpeed, 0, 2);
                EditorGUILayout.Space();
            }
        }
        void CustomizableSettingsVars(AdvancedFootIK IK)
        {
            if (CustomizableSettings)
            {
                EditorGUILayout.LabelField("Foot IK Features", Text());
                serializedObject.FindProperty("EnableFootIK").boolValue = EditorGUILayout.Toggle("Enable Foot IK", IK.EnableFootIK);
                serializedObject.FindProperty("EnableProIKFeature").boolValue = EditorGUILayout.Toggle("Enable Pro IK Feature", IK.EnableProIKFeature);
                serializedObject.FindProperty("showSolverDebug").boolValue = EditorGUILayout.Toggle("Show Solver Debug", IK.showSolverDebug);
                EditorGUILayout.Space();
            }
        }
        void AdvancedCurveSettingsVars(AdvancedFootIK IK)
        {
            if (AdvancedCurveSettings)
            {
                EditorGUILayout.LabelField("Advanced Foot Curves", Text());
                serializedObject.FindProperty("leftFootAnimVariableName").stringValue = EditorGUILayout.TextField("Left Foot Curve", IK.leftFootAnimVariableName);
                serializedObject.FindProperty("rightFootAnimVariableName").stringValue = EditorGUILayout.TextField("Right Foot Curve", IK.rightFootAnimVariableName);
                EditorGUILayout.Space();
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
