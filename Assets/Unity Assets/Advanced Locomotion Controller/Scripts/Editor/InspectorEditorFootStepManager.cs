using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    [CustomEditor(typeof(StandardFootstepManager))]
    public class InspectorEditorFootStepManager : Editor
    {
        public bool FootstepSettings;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            StandardFootstepManager controller = (StandardFootstepManager)target;

            EditorGUILayout.BeginHorizontal();
            Texture2D LogoTex = (Texture2D)Resources.Load("FasTPS_Logo_Gradient");
            GUILayout.Label(LogoTex, GUILayout.MaxWidth(135f), GUILayout.MaxHeight(25f));

            GUILayout.Label("Footstep Manager", Title());
            EditorGUILayout.EndHorizontal();

            FootstepSettings = GUILayout.Toggle(FootstepSettings, "♫ Footsteps", BoxStyle());
            FootstepSettingsVars();

            serializedObject.ApplyModifiedProperties();
        }
        void FootstepSettingsVars()
        {
            if (FootstepSettings)
            {
                EditorGUILayout.LabelField("Footstep Sounds", Text());
                SerializedProperty r_foot;

                r_foot = serializedObject.FindProperty("ConcreteFootsteps");
                for (int x = 0; x < r_foot.arraySize; x++)
                {
                    SerializedProperty property = r_foot.GetArrayElementAtIndex(x);
                    property.floatValue = Mathf.Max(0, property.floatValue);
                }
                EditorGUILayout.PropertyField(r_foot, true);
                serializedObject.ApplyModifiedProperties();

                r_foot = serializedObject.FindProperty("GrassFootsteps");
                for (int x = 0; x < r_foot.arraySize; x++)
                {
                    SerializedProperty property = r_foot.GetArrayElementAtIndex(x);
                    property.floatValue = Mathf.Max(0, property.floatValue);
                }
                EditorGUILayout.PropertyField(r_foot, true);
                serializedObject.ApplyModifiedProperties();

                r_foot = serializedObject.FindProperty("WoodFootsteps");
                for (int x = 0; x < r_foot.arraySize; x++)
                {
                    SerializedProperty property = r_foot.GetArrayElementAtIndex(x);
                    property.floatValue = Mathf.Max(0, property.floatValue);
                }
                EditorGUILayout.PropertyField(r_foot, true);
                serializedObject.ApplyModifiedProperties();

                r_foot = serializedObject.FindProperty("DirtFootsteps");
                for (int x = 0; x < r_foot.arraySize; x++)
                {
                    SerializedProperty property = r_foot.GetArrayElementAtIndex(x);
                    property.floatValue = Mathf.Max(0, property.floatValue);
                }
                EditorGUILayout.PropertyField(r_foot, true);
                serializedObject.ApplyModifiedProperties();

                r_foot = serializedObject.FindProperty("GravelFootsteps");
                for (int x = 0; x < r_foot.arraySize; x++)
                {
                    SerializedProperty property = r_foot.GetArrayElementAtIndex(x);
                    property.floatValue = Mathf.Max(0, property.floatValue);
                }
                EditorGUILayout.PropertyField(r_foot, true);
                serializedObject.ApplyModifiedProperties();
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
