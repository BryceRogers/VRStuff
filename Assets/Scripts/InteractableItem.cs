using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {
	
	private Rigidbody rigidBody;

	private bool currentlyInteracting;

	private Transform interactionPoint;

	private Controller attachedController;

	private Vector3 posDelta;
	private Quaternion rotationDelta;
	private float angle;
	private Vector3 axis;
	private float rotationFactor = 400f;
	private float velocityFactor = 20000f;

	void Start () {
		rigidBody = GetComponent <Rigidbody> ();
		interactionPoint = new GameObject ().transform;
		velocityFactor /= rigidBody.mass;
	}
	
	void FixedUpdate () {
		if(attachedController && currentlyInteracting) {
			posDelta = attachedController.transform.position - interactionPoint.position;
			this.rigidBody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

			rotationDelta = attachedController.transform.rotation * Quaternion.Inverse (interactionPoint.rotation);
			rotationDelta.ToAngleAxis (out angle, out axis);

			if(angle > 180) {
				angle -= 360;
			}

			if (angle < 5f) {
				this.rigidBody.angularVelocity = Vector3.zero;
			} else {
				this.rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationFactor;
			}
		}
	}

	public void BeginInteraction(Controller controller) {
		attachedController = controller;
		interactionPoint.position = controller.transform.position;
		interactionPoint.rotation = controller.transform.rotation;
		interactionPoint.SetParent (transform, true);

		currentlyInteracting = true;
	}

	public void EndInteraction(Controller controller) {
		if(controller == attachedController) {
			attachedController = null;
			currentlyInteracting = false;
		}
	}

	public bool isInteracting() {
		return currentlyInteracting;
	}

}
