using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem {
	public int amountToPool;
	public GameObject objectToPool;
	public List<GameObject> pooledObjects;
}

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler SharedInstance;

	public List<ObjectPoolItem> itemsToPool;

	void Awake() {
		SharedInstance = this;
	}

	void Start () {
		foreach (ObjectPoolItem item in itemsToPool) {
			item.pooledObjects = new List<GameObject>();
			for (int i = 0; i < item.amountToPool; i++) {
				// TODO: use dynamic casting? (in C#)
				GameObject obj = (GameObject)Instantiate(item.objectToPool);
				obj.SetActive(false);
				item.pooledObjects.Add(obj);
			}
		}
	}

	public GameObject GetPooledObject(string tag) {
		foreach (ObjectPoolItem item in itemsToPool) {
//			Debug.Log ("pooledObjects.Count: " + item.pooledObjects.Count);
			if (item.objectToPool.tag == tag) {
				for (int i = 0; i < item.pooledObjects.Count; i++) {
					if (!item.pooledObjects [i].activeInHierarchy) {
						return item.pooledObjects [i];
					}
				}
			}
		}
		return null;
	}

	void Update () {

	}
}