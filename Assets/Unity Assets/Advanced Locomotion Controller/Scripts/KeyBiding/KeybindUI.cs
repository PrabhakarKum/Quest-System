using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    public class KeybindUI : MonoBehaviour
    {
        public int CurrentPlatform;
        public GameObject HighlightKeyboard;
        public GameObject HighlightGamepad;

        public void ChangePanel(int panel)
        {
            CurrentPlatform = panel;
        }
        private void Update()
        {
            if (CurrentPlatform == 0)
            {
                HighlightKeyboard.SetActive(true);
                HighlightGamepad.SetActive(false);
            }
            else
            {
                HighlightKeyboard.SetActive(false);
                HighlightGamepad.SetActive(true);
            }
        }
    }
}
