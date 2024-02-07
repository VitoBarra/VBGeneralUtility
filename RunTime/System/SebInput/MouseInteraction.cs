using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SebInput.Internal;

namespace SebInput
{
	public class MouseInteraction<T>
	{
		public T Context { get; private set; }
		// Terminology: composite collider => the set of 2D colliders attached to this object and its child objects

		// Called when mouse enters composite collider
		public event System.Action<T> MouseEntered;
		// Called when mouse exits composite collider
		public event System.Action<T> MouseExitted;

		// Called when left mouse is pressed down over composite collider
		public event System.Action<T> LeftMouseDown;
		// Called when right mouse is pressed down over composite collider
		public event System.Action<T> RightMouseDown;
		// Called when left mouse is released over composite collider
		public event System.Action<T> LeftMouseReleased;
		// Called when right mouse is released over composite collider
		public event System.Action<T> RightMouseReleased;

		// Called when left mouse has been both pressed and then released over composite collider
		public event System.Action<T> LeftClickCompleted;
		// Called when right mouse has been both pressed and then released over composite collider
		public event System.Action<T> RightClickCompleted;

		// Is the mouse currently over the composite collider?
		public bool MouseIsOver { get; private set; }

		// Adds a listener component to the given gameObject. The given eventContext will be passed in to all events.
		public MouseInteraction(GameObject listenerTarget, T eventContext)
		{
			var listener = listenerTarget.AddComponent<MouseInteractionListener>();
			Context = eventContext;
			
			listener.OnMouseEntered += () => { MouseIsOver = true; MouseEntered?.Invoke(eventContext); };
			listener.OnMouseExitted += () => { MouseIsOver = false; MouseExitted?.Invoke(eventContext); };
			listener.OnLeftMouseDown += () => LeftMouseDown?.Invoke(eventContext);
			listener.OnRightMouseDown += () => RightMouseDown?.Invoke(eventContext);
			listener.OnLeftMouseReleased += () => LeftMouseReleased?.Invoke(eventContext);
			listener.OnRightMouseReleased += () => RightMouseReleased?.Invoke(eventContext);
			listener.OnLeftClickCompleted += () => LeftClickCompleted?.Invoke(eventContext);
			listener.OnRightClickCompleted += () => RightClickCompleted?.Invoke(eventContext);
		}

	}

}