using UnityEngine;

namespace SebInput.Internal
{
	public class MouseInteractionListener : MonoBehaviour
	{
		// Terminology: composite collider => the set of 2D colliders attached to this object and its child objects

		// Called when mouse enters composite collider
		public event System.Action OnMouseEntered;
		// Called when mouse exits composite collider
		public event System.Action OnMouseExitted;

		// Called when left mouse is pressed down over composite collider
		public event System.Action OnLeftMouseDown;
		// Called when right mouse is pressed down over composite collider
		public event System.Action OnRightMouseDown;
		// Called when left mouse has been both pressed and then released over composite collider
		public event System.Action OnLeftClickCompleted;
		// Called when right mouse has been both pressed and then released over composite collider
		public event System.Action OnRightClickCompleted;

		// Called when left mouse is released over composite collider
		public event System.Action OnLeftMouseReleased;
		// Called when right mouse is released over composite collider
		public event System.Action OnRightMouseReleased;

		void Awake()
		{
			MouseEventSystem.AddInteractionListener(this);
		}

		public void OnMouseEnter()
		{
			OnMouseEntered?.Invoke();
		}

		public void OnMouseExit()
		{
			OnMouseExitted?.Invoke();
		}

		public void OnMousePressDown(MouseEventSystem.MouseButton mouseButton)
		{
			switch (mouseButton)
			{
				case MouseEventSystem.MouseButton.Left:
					OnLeftMouseDown?.Invoke();
					break;
				case MouseEventSystem.MouseButton.Right:
					OnRightMouseDown?.Invoke();
					break;
			}
		}
		

		public void OnClickCompleted(MouseEventSystem.MouseButton mouseButton)
		{
			switch (mouseButton)
			{
				case MouseEventSystem.MouseButton.Left:
					OnLeftClickCompleted?.Invoke();
					break;
				case MouseEventSystem.MouseButton.Right:
					OnRightClickCompleted?.Invoke();
					break;
			}
		}

		public void OnMouseRelease(MouseEventSystem.MouseButton mouseButton)
		{
			switch (mouseButton)
			{
				case MouseEventSystem.MouseButton.Left:
					OnLeftMouseReleased?.Invoke();
					break;
				case MouseEventSystem.MouseButton.Right:
					OnRightMouseReleased?.Invoke();
					break;
			}
		}

		void OnDestroy()
		{
			MouseEventSystem.RemoveInteractionListener(this);
		}
	}

}