using UnityEngine;
using System.Collections;

public class InteractableItem : MonoBehaviour {
	
	protected Rigidbody rigidBody;

	protected bool currentlyInteracting;

	private Transform interactionPoint;

	private Controller attachedController;

	private Vector3 posDelta;
	private Quaternion rotationDelta;
	private float angle;
	private Vector3 axis;
	private float rotationBaseSpeed = 20f;
	private float velocityFactor = 40000f;

	protected void Start () {
		rigidBody = GetComponent <Rigidbody> ();
		interactionPoint = new GameObject ().transform;
		velocityFactor /= rigidBody.mass;
		rigidBody.maxAngularVelocity = 50f;
	}
	
	protected void FixedUpdate () {
		if(attachedController && currentlyInteracting) {
			posDelta = attachedController.transform.position - interactionPoint.position;
			this.rigidBody.velocity = posDelta * velocityFactor * Time.fixedDeltaTime;

			rotationDelta = attachedController.transform.rotation * Quaternion.Inverse (interactionPoint.rotation);
			rotationDelta.ToAngleAxis (out angle, out axis);

			if(angle > 180) {
				angle -= 360;
			}
			else if(angle < -180){
				angle += 360;
			}

			if (angle < 1f) {
				this.rigidBody.angularVelocity = Vector3.zero;
			} else {
				// TODO: GET A REAL LOG FUNCTION LMAO
				float rotationSpeed = rotationBaseSpeed * (0.54f * Mathf.Log (0.01924f*angle)+2.2f)/3f; // 0.02 - 1.08 logarithmically
				this.rigidBody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationSpeed;
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

	private void OnDestroy() {
		if(interactionPoint)
			Destroy (interactionPoint.gameObject);
	}
}
