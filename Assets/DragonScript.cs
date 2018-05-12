using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonScript : MonoBehaviour {

	public GameObject bullet;
	public GameObject body;
	public GameObject leftWing;
	public GameObject rightWing;
	private bool rotateUp = true;
	private Vector3 leftWingPosition;

	// Use this for initialization
	void Start () {
		leftWingPosition = leftWing.transform.localPosition;
		InvokeRepeating ("Shoot", 0.01f, 0.2f);
//		InvokeRepeating ("FlapWings", 0.01f, 0.2f);
//		StartCoroutine(FlapWings());
		InvokeRepeating ("FlapWings2", 0.01f, 0.05f);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void Shoot () {
//		Instantiate (bullet, this.transform.position, Quaternion.identity);
//		bullet.gameObject.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 2f, 0);
		GameObject b = ObjectPooler.SharedInstance.GetPooledObject(bullet.tag); 
		if (b != null) {
			b.transform.position = this.transform.position;
			b.SetActive(true);
		}
	}

	void activateFlapWings3 () {
		StartCoroutine (FlapWings3());
	}

	IEnumerator FlapWings3 () {
		for (float i = 0.0f; i < -45f; i-=5f) {
			Debug.Log ("rotating " + i);
			transform.Rotate (Vector3.forward * -i);
			yield return new WaitForSeconds (0.5f);
		}
	}

	void FlapWings2 () {
		float eulerAngleY = leftWing.transform.localEulerAngles.y;
		Debug.Log ("eulerAngleY: " + eulerAngleY);
		Debug.Log ("rotateUp: " + rotateUp);
		Debug.Log ("leftWing.transform.localPosition: " + leftWing.transform.localPosition);
		Debug.Log ("leftWingPosition: " + leftWingPosition);
		if (rotateUp) {
			if ((eulerAngleY >= 305f) && (eulerAngleY <= 310f)) { // maximum vertical rotation
				Debug.Log("1 up");
				rotateUp = false;
//				leftWing.transform.Rotate (Vector3.down * 5f);
//				rightWing.transform.Rotate (Vector3.up * 5f);
				leftWing.transform.RotateAround(body.transform.position, Vector3.up, 5f);
				rightWing.transform.RotateAround(body.transform.position, Vector3.down, 5f);
			} else {
				Debug.Log("2 down");
//				leftWing.transform.Rotate (Vector3.up * 5f);
//				rightWing.transform.Rotate (Vector3.down * 5f);
				leftWing.transform.RotateAround(body.transform.position, Vector3.down, 5f);
				rightWing.transform.RotateAround(body.transform.position, Vector3.up, 5f);
			}
		} else {
			if ((eulerAngleY >= 0f) && (eulerAngleY <= 10f)) { // minimum vertical rotation
				Debug.Log("3 down");
				rotateUp = true;
//				leftWing.transform.Rotate (Vector3.up * 5f);
//				rightWing.transform.Rotate (Vector3.down * 5f);
				leftWing.transform.RotateAround(body.transform.position, Vector3.down, 5f);
				rightWing.transform.RotateAround(body.transform.position, Vector3.up, 5f);
			} else {
				Debug.Log("4 up");
//				leftWing.transform.Rotate (Vector3.down * 5f);
//				rightWing.transform.Rotate (Vector3.up * 5f);
				leftWing.transform.RotateAround(body.transform.position, Vector3.up, 5f);
				rightWing.transform.RotateAround(body.transform.position, Vector3.down, 5f);
			}
		}
	}

	IEnumerator FlapWings () {
		Debug.Log ("FlapWings");
		yield return new WaitForSeconds (0.5f);
		/*
		while (true) {
			Debug.Log ("rotate up");
//			leftWing.transform.Rotate (new Vector3 (0, 1, 0), Space.Self);
			leftWing.transform.RotateAround (leftWing.transform.position, Vector3.up, 45f);
			leftWing.transform.Rotate (new Vector3 (0, 1, 0), Space.World);
			yield return new WaitForSeconds (0.5f);
			Debug.Log ("rotate normal");
//			leftWing.transform.Rotate (new Vector3 (0, -1, 0), Space.Self);
			leftWing.transform.Rotate (new Vector3 (0, -1, 0), Space.World);
			yield return new WaitForSeconds (0.5f);
			Debug.Log ("rotate down");
//			leftWing.transform.Rotate (new Vector3 (0, -1, 0), Space.Self);
			leftWing.transform.Rotate (new Vector3 (0, -1, 0), Space.World);
			yield return new WaitForSeconds (0.5f);
			Debug.Log ("rotate normal");
//			leftWing.transform.Rotate (new Vector3 (0, 1, 0), Space.Self);
			leftWing.transform.Rotate (new Vector3 (0, 1, 0), Space.World);
			yield return new WaitForSeconds (0.5f);
		}
		*/
	}
}
