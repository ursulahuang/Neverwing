using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public FairyScript fairy;

	public GameObject[] enemies;
	public GameObject[] rewards;

	public float spawnTime = 3f;
	public int numOfEnemies = 5;

	float enemyWidth = 1.5f;
	float cameraUpperBound = 7f; // TODO: find a better way of restricting the spawn points


	// Use this for initialization
	void Start () {
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Spawn () {
		if (fairy.playerLives <= 0) {
			return;
		}

		Vector3 pos = new Vector3 (-enemyWidth * (numOfEnemies + 1) / 2, cameraUpperBound, 0f);

		for (int i = 0; i < numOfEnemies; i++) {
			int randomIndex = Random.Range (0, enemies.Length);
			GameObject randomEnemy = enemies [randomIndex];
			pos += new Vector3 (1.5f, 0, 0);
//			Debug.Log ("pos: " + pos);

//			GameObject enemy = Instantiate (randomEnemy, pos, Quaternion.identity);
			GameObject enemy = ObjectPooler.SharedInstance.GetPooledObject(randomEnemy.tag);

			if (enemy != null) {
				// attach reward
				int r = Random.Range (0, rewards.Length);
				GameObject randomReward = rewards [r];

				GameObject reward = ObjectPooler.SharedInstance.GetPooledObject(randomReward.tag);
				if (reward != null) {
					enemy.GetComponent<EnemyScript> ().reward = reward;
				} else {
					Debug.Log ("No more rewards");
				}

				enemy.transform.position = pos;
				enemy.GetComponent<EnemyScript> ().isVelocityEnabled = true;
				enemy.GetComponent<EnemyScript> ().velocity = new Vector3 (0, -2f, 0);
				enemy.SetActive(true);
			}
		}
	}
}
