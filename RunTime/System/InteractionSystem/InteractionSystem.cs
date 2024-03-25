using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VitoBarra.System.Interaction
{
    public class InteractionSystem : MonoBehaviour
    {
        public static InteractionSystem Instance;

        private Interactable InteractableWhitFocus;

        //CommandExecutes ExecutionCommand;

        private void Awake()
        {
            Instance = this;
            //ExecutionCommand = new CommandExecutes();
        }

        private void Update()
        { 
            //TODO: generalize
            if (InteractableWhitFocus == null) return;
            if (InputHelper.AnyOfTheseKeysDown(KeyCode.Backspace, KeyCode.Delete) || Input.GetMouseButton(2))
                InteractableWhitFocus.DeleteCommand();
            //END TODO
            
            //ExecutionCommand.Execute();
            
        }

        public bool HasFocus(Interactable interactable) => InteractableWhitFocus == interactable;

        public void ReleaseFocus(Interactable interactable)
        {
            if (!HasFocus(interactable)) return;
            InteractableWhitFocus = null;
        }

        public bool RequestFocus(Interactable interactable)
        {
            if (HasFocus(interactable)) return false;
            if (InteractableWhitFocus == null)
            {
                SetIntelligibleWhitFocus(interactable);
                return true;
            }

            if (!InteractableWhitFocus.CanReleaseFocus()) return false;


            InteractableWhitFocus.ReleaseFocus();
            SetIntelligibleWhitFocus(interactable);
            return true;
        }

        private void SetIntelligibleWhitFocus(Interactable interactable)
        {
            InteractableWhitFocus = interactable;
        }

        private void OnDestroy()
        {
           // ExecutionCommand.UnregisterEvent();
        }
    }
}