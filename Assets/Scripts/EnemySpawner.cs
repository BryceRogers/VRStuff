using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	private GameObject dudePrefab;

	private float difficultyMultiplier = 0.98f;
	private float spawnMinTimer = 4f;
	private float spawnMaxTimer = 8f;

	void Start () {
		StartCoroutine ("SpawnEnemies");
		dudePrefab = (GameObject)Resources.Load ("Dude");
	}

	private IEnumerator SpawnEnemies() {
		while (true) {
			yield return new WaitForSeconds (Random.Range (spawnMinTimer, spawnMaxTimer));
			Instantiate (dudePrefab, selectRandomPoint (), Quaternion.identity);

			spawnMinTimer *= difficultyMultiplier;
			spawnMaxTimer *= difficultyMultiplier;
		}
	}

	private Vector3 selectRandomPoint() {
		Vector2 randomPoint = Random.insideUnitCircle * 1.5f;

		float x = randomPoint.x + transform.position.x;
		float y = 0.1f;
		float z = randomPoint.y + transform.position.z;

		return new Vector3 (x, y, z);
	}
}
