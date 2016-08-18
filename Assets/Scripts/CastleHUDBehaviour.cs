using UnityEngine;
using System.Collections;

public class CastleHUDBehaviour : MonoBehaviour {

	private TextMesh textMesh;
	private CastleBehaviour castle;

	void Start () {
		textMesh = GetComponent<TextMesh> ();
		castle = GameObject.FindWithTag ("Castle").GetComponent<CastleBehaviour> ();
	}
	
	void Update () {
		textMesh.transform.LookAt (Camera.main.transform.position);

		if (castle) {
			textMesh.text = "HP: " + castle.getHp ();
		} else {
			textMesh.text = "Thanks";
		}
	}


}
