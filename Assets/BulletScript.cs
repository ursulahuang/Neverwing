using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	public Vector3 velocity;
	public float powerFactor;
	public GameObject hit;
	public Vector3 hitOffset = new Vector3(0, 1f, -1f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		transform.position += velocity * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collider) {
//		Debug.Log ("BulletScript.OnTriggerEnter2D()");

//		collider.gameObject.SendMessage ("TakeDamage", powerFactor);

		if (collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			GameObject hitobj = Instantiate (hit, this.transform.position + hitOffset, Quaternion.identity);
			//		Destroy(gameObject);
			gameObject.SetActive(false); 

			GameObject.Destroy (hitobj, 0.15f);
		}
	}
}
