using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

	public bool isVelocityEnabled = false;
	public Vector3 velocity;

	public const float maxHealth = 100f;
	float currentHealth = maxHealth;
	public const float impactDamage = 10f;

	public GameObject explosion;
	public GameObject reward;
	float speed = 5f;

	public Slider healthSlider;
	private bool healthBarVisible = false;

	void OnEnable () {
		currentHealth = maxHealth;
		UpdateHealthUI ();
		healthSlider.gameObject.SetActive(false);
		healthBarVisible = false;
	}

	void FixedUpdate () {
		if (isVelocityEnabled) {
			transform.position += velocity * Time.deltaTime;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D collider) {
//		Debug.Log ("EnemyScript.OnTriggerEnter2D()" + collider.gameObject);
		if (collider.gameObject.layer == LayerMask.NameToLayer ("Bullet")) {
			if (!healthBarVisible) {
				healthBarVisible = true;
				healthSlider.gameObject.SetActive(true);

			}
			float powerFactor = collider.gameObject.GetComponent<BulletScript> ().powerFactor;
			TakeDamage (powerFactor);
		}
	}


	public void TakeDamage (float powerFactor) {
//		Debug.Log ("EnemyScript.TakeDamage(" + powerFactor + ")");
//		Debug.Log ("currentHealth: " + currentHealth);
		currentHealth -= impactDamage * powerFactor;
//		Debug.Log ("after damage: " + currentHealth);
		UpdateHealthUI();

		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Debug.Log("Dead!");

			OnDeath ();
		}
	}

	private void UpdateHealthUI () {
		// set slider value to current health
		healthSlider.value = currentHealth;

		// change the color of the slider 
	}

	private void OnDeath () {
		GameObject explo = Instantiate (explosion, 
			gameObject.transform.position, 
			Quaternion.identity);
		
//		GameObject.Destroy (gameObject);
		gameObject.SetActive(false);

		GameObject.Destroy (explo, 
			explo.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo(0).length);

		SpawnReward ();
	}

	private void SpawnReward () {
		GameObject rewardObj = ObjectPooler.SharedInstance.GetPooledObject(reward.tag); 
		if (rewardObj != null) {
			rewardObj.transform.position = this.transform.position;
			rewardObj.SetActive(true);
		}
			
		Vector3 velocity1 = new Vector3 (Random.Range(-1f, 1f), Random.Range(1, 2), 0);
		velocity1.Normalize ();
		rewardObj.GetComponent<Rigidbody2D> ().velocity = velocity1 * speed;
	}
}
