using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	private SteamVR_TrackedObject trackedObject;
	private SteamVR_Controller.Device controller { 
		get { 
			return SteamVR_Controller.Input( (int) trackedObject.index);
		}
	}

	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private bool gripButtonUp = false;
	private bool gripButtonDown = false;
	private bool gripButtonPress = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool triggerButtonUp = false;
	private bool triggerButtonDown = false;
	private bool triggerButtonPress = false;

	void Start () {
		trackedObject = GetComponent <SteamVR_TrackedObject> ();
	}
	
	void Update () {
		if(controller == null) {
			Debug.Log ("Controller not initialized.");
			return;
		}

		gripButtonDown = controller.GetPressDown (gripButton);
		gripButtonUp = controller.GetPressUp (gripButton);
		gripButtonPress = controller.GetPress (gripButton);

		triggerButtonDown = controller.GetPressDown (triggerButton);
		triggerButtonUp = controller.GetPressUp (triggerButton);
		triggerButtonPress = controller.GetPress (triggerButton);

		if (gripButtonDown) {
			Debug.Log ("Grip Button was just pressed");
		}
		if (gripButtonUp) {
			Debug.Log ("Grip Button was just unpressed");
		}
		if(triggerButtonDown) {
			Debug.Log ("Trigger button was just pressed");
		}
		if (triggerButtonUp) {
			Debug.Log ("Trigger button was just unpressed");
		}
	}
}
