﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VitoBarra.System.Interaction
{
    public abstract class Interactable : MonoBehaviour
    {
        public event Action OnFocusObtained;
        public event Action OnFocusLost;


        public abstract void DeleteCommand();


        public virtual bool CanReleaseFocus() => true;

        public bool HasFocus => InteractionSystem.Instance.HasFocus(this);

        public bool RequestFocus()
        {
            var FocusObtained = InteractionSystem.Instance.RequestFocus(this);
            if (FocusObtained)
                OnFocusObtained?.Invoke();
            return FocusObtained;
        }
        public void RequestFocusNoReturn()
        {
            RequestFocus();
        }

        private void ReleaseFocusNotHandled()
        {
            InteractionSystem.Instance.ReleaseFocus(this);
        }

        public void ReleaseFocus()
        {
            ReleaseFocusNotHandled();
            OnFocusLost?.Invoke();
        }
    }
}