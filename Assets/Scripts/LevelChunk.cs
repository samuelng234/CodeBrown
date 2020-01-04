﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelChunk : MonoBehaviour {
	
	private ArrayList enemySpawns = new ArrayList();
	private ArrayList itemSpawns = new ArrayList();
	private ArrayList enemyList = new ArrayList ();
	private ArrayList itemList = new ArrayList ();
	private Transform chunkTrigger;
	private Player player;
	private ObjectPool pool;
	
	public LevelChunk Chunk {
		get { return this; }
	}
	
	// Use this for initialization
	void Start () {
		player = (Player)GameObject.FindGameObjectWithTag (Variables.PlayerPrefabName).GetComponent (Variables.PlayerPrefabName);
		pool = (ObjectPool)GameObject.FindGameObjectWithTag ("GameEngine").GetComponent ("ObjectPool");
		getSpawnPoints ();
		randomiseEnemySpawn ();
		randomiseItemSpawn ();
	}
	
	public void ResetCamera() {
		transform.position = new Vector3(transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
		resetCameraEnemies ();
		resetCameraItems ();
	}
	
	public void DestroyAll() {
		destroyEnemies ();
		destroyItems ();
	}
	
	private void getSpawnPoints() {
		foreach (Transform child in transform) {
			switch (child.tag) {
			case ("EnemySpawn") : 
				enemySpawns.Add (child);
				break;
			case ("ItemSpawn") :
				itemSpawns.Add (child);
				break;
			case ("NewChunk Trigger") :
				chunkTrigger = child;
				break;
			}
		}
	}

	private void randomiseEnemySpawn () {
		foreach (Transform spawn in enemySpawns) {
			int spawnChance = UnityEngine.Random.Range (0, 100);

			if (spawnChance < (Variables.EnemySpawnPerc * 100)) {
				string path = "Prefabs/" + chooseRandomEnemy();

				GameObject obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(spawn.position.x, spawn.position.y, 0.1f), spawn.rotation);
				enemyList.Add(obj);
			}
		}
	}

	private void randomiseItemSpawn() {
		foreach (Transform spawn in itemSpawns) {
			int spawnChance = UnityEngine.Random.Range (0, 100);
			
			if (spawnChance < (Variables.ItemSpawnPerc * 100)) {
				string path = "Prefabs/" + chooseRandomItem();
				
				GameObject obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(spawn.position.x, spawn.position.y, 0.1f), spawn.rotation);
				itemList.Add(obj);
			}
		}
	}


	private void resetCameraEnemies() {
		foreach (GameObject obj in enemyList) {
			if(obj != null) {
				if (obj.tag == "WormCluster")
					((WormGroup)obj.GetComponent("WormGroup")).ResetCamera ();
				else
					((CameraReset)obj.GetComponent("CameraReset")).ResetCamera ();
			}
		}
	}
	
	private void resetCameraItems() {
		foreach (GameObject obj in itemList) {
			if(obj != null) {
				PoopItem item = (PoopItem)obj.GetComponent("PoopItem");
				item.ResetCamera ();
			}
		}
	}

	private string chooseRandomEnemy () {
		int enemySelection = UnityEngine.Random.Range (0, Variables.EnemySprites.Length);

		if (enemySelection == 2) {
			int wormSelection = UnityEngine.Random.Range (0, Variables.WormClusters.Length);

			return Variables.WormClusters[wormSelection];
		}

		return Variables.EnemySprites[enemySelection];
	}

	private string chooseRandomItem () {
		int itemSelection = UnityEngine.Random.Range (0, Variables.PoopSprites.Length);
		
		return Variables.PoopSprites[itemSelection];
	}
	
	private void destroyEnemies() {
		foreach (GameObject obj in enemyList) {
			if (obj.tag == "WormCluster") {
				//((WormGroup)obj.GetComponent("WormGroup")).DestroyEnemies();
				List<GameObject> worms = ((WormGroup)obj.GetComponent("WormGroup")).GetWorms();
				foreach (GameObject o in worms) {
					if (o.tag == "Worm1")
						pool.AddWorm1(o);
					else if (o.tag == "Worm2")
						pool.AddWorm2(o);
					else if (o.tag == "Worm3")
						pool.AddWorm3(o);
					else if (o.tag == "Worm4")
						pool.AddWorm4(o);
				}
			}

			Destroy (obj);
		}

		enemyList.Clear ();
	}
	
	private void destroyItems() {
		foreach (GameObject obj in itemList)
			Destroy (obj);

		itemList.Clear ();
	}
}