using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyScript : MonoBehaviour {

	public int playerLives = 1;

	public GameObject bullet;
	bool isGrabbed;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Shoot", .001f, .25f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {
		if(isGrabbed){
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			worldPosition.z = 0;
//			Debug.Log ("World Position: " + worldPosition.ToString());
			this.transform.position = worldPosition;
		}
	}

	void OnMouseDown(){
		Debug.Log ("FairyScript.OnMouseDown()");
		isGrabbed = true;
	}

	void OnMouseUp(){
		Debug.Log ("FairyScript.OnMouseUp()");
		if(isGrabbed){
			isGrabbed = false;

//			Vector3 diff = launchPoint.transform.position - this.transform.position;
//			this.GetComponent<Rigidbody2D>().velocity = diff * launchFactor;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
//		Debug.Log ("FairyScript.OnTriggerEnter2D()");

		if (collider.gameObject.layer == LayerMask.NameToLayer("Reward")) {
			Debug.Log ("Reward");

			RewardScript reward = collider.gameObject.GetComponent<RewardScript> ();
			int rewardValue = reward.rewardValue;
			ScoreManager.score += rewardValue;

//			Destroy (collider.gameObject);
			collider.gameObject.SetActive(false);
		}
		if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Debug.Log ("Enemy");
			playerLives--;

			if (playerLives <= 0) {
				Debug.Log ("Player is dead");
				// stop the game, don't destroy the Player
				// play dead animation
				Destroy (gameObject, 0.1f);
			} else {
				// play harmed animation
				StartCoroutine(Flasher());
			}
		}
	}

	IEnumerator Flasher() 
	{
		for (int i = 0; i < 5; i++)
		{
			Color newColor = Color.red;
			newColor.a -= 0.5f;
			this.GetComponent<SpriteRenderer> ().material.color = newColor;
			yield return new WaitForSeconds(.1f);
			this.GetComponent<SpriteRenderer> ().material.color = Color.white; 
			yield return new WaitForSeconds(.1f);
		}
	}

	void Shoot() {
		Vector3 bulletOffset = new Vector3(0, 1f, 0.1f);
//		Instantiate(bullet, this.transform.position + bulletOffset, Quaternion.identity);

//		GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject(gameObject.tag); 
		GameObject b = ObjectPooler.SharedInstance.GetPooledObject(bullet.tag); 
		if (b != null) {
			b.transform.position = this.transform.position + bulletOffset;
			b.SetActive(true);
		}
	}

}
