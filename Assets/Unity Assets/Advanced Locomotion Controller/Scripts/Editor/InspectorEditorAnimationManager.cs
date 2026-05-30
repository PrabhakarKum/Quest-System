using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(StandardAnimatorManager))]
    public class InspectorEditorAnimationManager : Editor
    {
        public bool AnimationSettings;
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            StandardAnimatorManager manager = (StandardAnimatorManager)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Animation Manager", Title());
            EditorGUILayout.EndHorizontal();

            AnimationSettings = GUILayout.Toggle(AnimationSettings, "➠ Animation Speeds", BoxStyle());
            AnimationSettingsVars(manager);

            serializedObject.ApplyModifiedProperties();
        }
        void AnimationSettingsVars(StandardAnimatorManager manager)
        {
            if (AnimationSettings)
            {
                EditorGUILayout.LabelField("Blend Speeds", Text());
                serializedObject.FindProperty("Acceleration").floatValue = EditorGUILayout.Slider("Acceleration Blend", manager.Acceleration, 0, 10);
                serializedObject.FindProperty("Deceleration").floatValue = EditorGUILayout.Slider("Deceleration Blend", manager.Deceleration, 0, 10);
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

