using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
	
	public static ObjectPool current;
	private List<GameObject> EnemyPool;
	private List<GameObject> ItemPool;
	private List<GameObject> WormPool1;
	private List<GameObject> WormPool2;
	private List<GameObject> WormPool3;
	private List<GameObject> WormPool4;
	
	void Awake () {
		current = this;
	}
	
	// Use this for initialization
	void Start () {
		EnemyPool = new List<GameObject> ();
		ItemPool = new List<GameObject> ();
		WormPool1 = new List<GameObject> ();
		WormPool2 = new List<GameObject> ();
		WormPool3 = new List<GameObject> ();
		WormPool4 = new List<GameObject> ();
		//populateEnemyPool ();
		//populateItemPool ();
		populateWormPool1 ();
		populateWormPool2 ();
		populateWormPool3 ();
		populateWormPool4 ();
	}
	
	public GameObject GetInactiveZombie () {
		foreach (GameObject obj in EnemyPool) {
			if (!obj.activeInHierarchy) {
				EnemyPool.Remove (obj);
				return obj;
			}
		}
		return null;
	}
	
	public GameObject GetInactiveItem () {
		foreach (GameObject obj in ItemPool) {
			if (!obj.activeInHierarchy) {
				ItemPool.Remove (obj);
				return obj;
			}
		}
		return null;
	}

	public GameObject GetInactiveWorm1 () {
		foreach (GameObject obj in WormPool1) {
			if (!obj.activeInHierarchy) {
				WormPool1.Remove (obj);
				return obj;
			}
		}
		return null;
	}

	public GameObject GetInactiveWorm2 () {
		foreach (GameObject obj in WormPool2) {
			if (!obj.activeInHierarchy) {
				WormPool2.Remove (obj);
				return obj;
			}
		}
		return null;
	}

	public GameObject GetInactiveWorm3 () {
		foreach (GameObject obj in WormPool3) {
			if (!obj.activeInHierarchy) {
				WormPool3.Remove (obj);
				return obj;
			}
		}
		return null;
	}

	public GameObject GetInactiveWorm4 () {
		foreach (GameObject obj in WormPool4) {
			if (!obj.activeInHierarchy) {
				WormPool4.Remove (obj);
				return obj;
			}
		}
		return null;
	}
	
	public void AddEnemy (GameObject obj) {
		obj.SetActive (false);
		EnemyPool.Add (obj);
	}
	
	public void AddItem (GameObject obj) {
		obj.SetActive (false);
		ItemPool.Add (obj);
	}

	public void AddWorm1 (GameObject obj) {
		obj.SetActive (false);
		WormPool1.Add (obj);
	}

	public void AddWorm2 (GameObject obj) {
		obj.SetActive (false);
		WormPool2.Add (obj);
	}

	public void AddWorm3 (GameObject obj) {
		obj.SetActive (false);
		WormPool3.Add (obj);
	}

	public void AddWorm4 (GameObject obj) {
		obj.SetActive (false);
		WormPool4.Add (obj);
	}

	public void ResetPools () {
		for (int i = 0; i < Variables.WormPoolSize; i++) {
			WormPool1[i].SetActive (false);
			WormPool2[i].SetActive (false);
			WormPool3[i].SetActive (false);
			WormPool4[i].SetActive (false);
		}
	}
	
	private void populateEnemyPool () {
		for (int i = 0; i < Variables.EnemyPoolSize; i++) {
			GameObject obj = null;//(GameObject)Instantiate (Resources.Load("Prefabs/Zombie"));
			obj.SetActive(false);
			EnemyPool.Add(obj);
		}
	}
	
	private void populateItemPool () {
		for (int i = 0; i < Variables.ItemPoolSize; i++) {
			GameObject obj = null;//(GameObject)Instantiate (Resources.Load("Prefabs/Lawn_Mower_Item"));
			obj.SetActive(false);
			ItemPool.Add(obj);
		}
	}

	private void populateWormPool1 () {
		for (int i = 0; i < Variables.WormPoolSize; i++) {
			GameObject obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.WormSprites[0]));
			obj.SetActive(false);
			WormPool1.Add(obj);
		}
	}

	private void populateWormPool2 () {
		for (int i = 0; i < Variables.WormPoolSize; i++) {
			GameObject obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.WormSprites[1]));
			obj.SetActive(false);
			WormPool2.Add(obj);
		}
	}

	private void populateWormPool3 () {
		for (int i = 0; i < Variables.WormPoolSize; i++) {
			GameObject obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.WormSprites[2]));
			obj.SetActive(false);
			WormPool3.Add(obj);
		}
	}

	private void populateWormPool4 () {
		for (int i = 0; i < Variables.WormPoolSize; i++) {
			GameObject obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.WormSprites[3]));
			obj.SetActive(false);
			WormPool4.Add(obj);
		}
	}
}
