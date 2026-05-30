using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using FasTPS;

namespace FasTPS
{
    public class EditorWindowCharacterCreator : EditorWindow
    {
        string ThirdPersonControllerName = "AdvancedThirdPersonController";
        GameObject ThirdPersonCharacterNested;
        GameObject HumanoidRigNewCharacter;
        public bool AutoRotation = true;
        public bool Analog = false;
        public bool StrafeRun = true;
        public bool Covering = true;
        public bool Sliding = true;
        public bool Vaulting = true;
        public bool WallRunning = true;
        public bool AutoStep = true;
        public bool LandingRoll = true;

        [MenuItem("Tools/FasTPS/Character Creator")]
        public static void ShowWindow()
        {
            EditorWindow instance = (EditorWindow)CreateInstance(typeof(EditorWindow));
            instance = GetWindow<EditorWindowCharacterCreator>("CharacterCreator");
            instance.maxSize = new Vector2(450f, 310f);
            instance.minSize = instance.maxSize;
        }

        private void OnEnable()
        {
            ThirdPersonCharacterNested = Resources.Load("Nested-Characters-Don'tDelete/Advanced-Standard-Character") as GameObject;
        }
        private void OnGUI()
        {
            GUILayout.Space(5);
            GUILayout.Label("Third-Person-Controller Creator", EditorStyles.boldLabel);

            ThirdPersonControllerName = EditorGUILayout.TextField("Controller Name", ThirdPersonControllerName);
            //ThirdPersonCharacterNested = EditorGUILayout.ObjectField("Nested Controller", ThirdPersonCharacterNested, typeof(GameObject), false) as GameObject;
            HumanoidRigNewCharacter = EditorGUILayout.ObjectField("New Model", HumanoidRigNewCharacter, typeof(GameObject), false) as GameObject;
            GUILayout.Space(10);
            GUILayout.Label("Customize", EditorStyles.boldLabel);
            AutoRotation = EditorGUILayout.Toggle("AutoRotation", AutoRotation);
            Analog = EditorGUILayout.Toggle("Analog", Analog);
            StrafeRun = EditorGUILayout.Toggle("StrafeRun", StrafeRun);
            Covering = EditorGUILayout.Toggle("Covering", Covering);
            Sliding = EditorGUILayout.Toggle("Sliding", Sliding);
            Vaulting = EditorGUILayout.Toggle("Vaulting", Vaulting);
            WallRunning = EditorGUILayout.Toggle("WallRunning", WallRunning);
            AutoStep = EditorGUILayout.Toggle("AutoStep", AutoStep);
            LandingRoll = EditorGUILayout.Toggle("LandingRoll", LandingRoll);


            if (GUILayout.Button("Create Controller"))
            {
                SpawnObject();
            }
        }

        private void SpawnObject()
        {
            if (HumanoidRigNewCharacter == null) { Debug.LogError("Please Assign A Humanoid Model"); return; }
            if (ThirdPersonControllerName == "")
            {
                ThirdPersonControllerName = "AdvancedThirdPersonController";
            }
            GameObject character = Instantiate(ThirdPersonCharacterNested.gameObject, Vector3.zero, Quaternion.identity);
            character.gameObject.name = ThirdPersonControllerName;
            DestroyImmediate(character.gameObject.GetComponentInChildren<CharacterMovement>().transform.GetChild(0).gameObject);
            GameObject newmodel = Instantiate(HumanoidRigNewCharacter, Vector3.zero, Quaternion.identity);
            character.GetComponentInChildren<CinemachineFreeLook>().Follow = newmodel.transform;
            newmodel.transform.SetParent(character.GetComponentInChildren<CharacterMovement>().transform);
            newmodel.transform.SetSiblingIndex(0);

            CharacterMovement controller = character.GetComponentInChildren<CharacterMovement>();
            newmodel.transform.localPosition = Vector3.zero;
            controller.GetComponent<Animator>().avatar = newmodel.GetComponent<Animator>().avatar;
            DestroyImmediate(newmodel.GetComponent<Animator>());
            newmodel.transform.localRotation = Quaternion.identity;

            controller.AutoRotation = AutoRotation;
            controller.Analog = Analog;
            controller.StrafeRun = StrafeRun;
            controller.Covering = Covering;
            controller.Sliding = Sliding;
            controller.Vaulting = Vaulting;
            controller.WallRunning = WallRunning;
            controller.AutoStep = AutoStep;
            controller.LandingRoll = LandingRoll;
        }
    }
}
