using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device controller { 
		get { 
			return SteamVR_Controller.Input( (int) trackedObject.index);
		}
	}

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private HashSet<InteractableItem> objectHoveringOver = new HashSet<InteractableItem>();

	private InteractableItem closestItem;
	private InteractableItem interactingItem;

	void Start () {
		trackedObject = GetComponent <SteamVR_TrackedObject> ();
	}
	
	void Update () {
		if(controller == null) {
			Debug.Log ("Controller not initialized.");
			return;
		}

		if (controller.GetPressDown (gripButton)) {
		}
		if (controller.GetPressUp (gripButton)) {
		}

		if(controller.GetPressDown (triggerButton)) {
			float minDistance = float.MaxValue;
			float distance;
			closestItem = null;
			foreach(InteractableItem item in objectHoveringOver) {
				distance = (item.transform.position - transform.position).sqrMagnitude;
				if(distance < minDistance) {
					minDistance = distance;
					closestItem = item;
				}
			}

			interactingItem = closestItem;

			if (interactingItem) {
				if (interactingItem.isInteracting ()) {
					interactingItem.EndInteraction (this);
				}

				interactingItem.BeginInteraction (this);
			}
		}

		if (controller.GetPressUp (triggerButton) && interactingItem != null) {
			interactingItem.EndInteraction (this);
			interactingItem = null;
		}

	}

	private void OnTriggerEnter(Collider collider) {
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if(collidedItem) {
			objectHoveringOver.Add (collidedItem);
		}
	}

	private void OnTriggerExit(Collider collider) {
		InteractableItem collidedItem = collider.GetComponent<InteractableItem> ();
		if(collidedItem) {
			objectHoveringOver.Remove (collidedItem);
		}
	}

}
