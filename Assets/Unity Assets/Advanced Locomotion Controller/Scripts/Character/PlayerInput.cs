using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    public class PlayerInput : MonoBehaviour
    {
        [HideInInspector]
        public Vector2 MoveInput;
        [HideInInspector]
        public bool Sprint;
        [HideInInspector]
        public bool Crouch;

        PlayerControls Controls;
        private void Start()
        {
            CharacterMovement controller = GetComponentInChildren<CharacterMovement>();
            Controls = InputManager.inputActions;
            Controls.Enable();

            Controls.Keyboard.MovementVector.performed += ctx =>
            {
                MoveInput = ctx.ReadValue<Vector2>();
            };
            Controls.Keyboard.MovementVector.canceled += ctx =>
            {
                MoveInput = ctx.ReadValue<Vector2>();
            };
            Controls.Keyboard.Escape.performed += ctx =>
            {
                controller.OpenMenu();
            };
            Controls.Keyboard.Sprint.performed += ctx =>
            {
                Sprint = true;
            };
            Controls.Keyboard.Sprint.canceled += ctx =>
            {
                Sprint = false;
            };
            Controls.Keyboard.Crouch.performed += ctx =>
            {
                Crouch = !Crouch;
            };
            Controls.Keyboard.Jump.performed += ctx =>
            {
                if (!Crouch && !controller.MenuOpen)
                    controller.Jump();
            };
            Controls.Keyboard.Cover.performed += ctx =>
            {
                if (controller.Covering && !controller.MenuOpen)
                    controller.Cover();
            };
            Controls.Keyboard.Vault.performed += ctx =>
            {
            };
        }
    }
}
