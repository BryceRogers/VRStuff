using UnityEngine;
using System.Collections;

public class CastleBehaviour : MonoBehaviour {

	private int hp = 100;

	void Start () {
	}
	
	void Update () {
		if (hp < 0) {
			Destroy (gameObject);
		}
	}

	public void ProcessDamage(int damage) {
		hp -= damage;
	}

	public int getHp() {
		return hp;
	}

	private void OnDestroy() {
		GameObject[] enemyRelatedObjects = GameObject.FindGameObjectsWithTag ("EnemyRelated");

		foreach (GameObject obj in enemyRelatedObjects) {
			Destroy (obj);
		}
	}
}
