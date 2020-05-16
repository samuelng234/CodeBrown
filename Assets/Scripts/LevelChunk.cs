using UnityEngine;
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
		player = GameObject.FindGameObjectWithTag (Variables.PlayerPrefabName).GetComponent<Player>();
		pool = GameObject.FindGameObjectWithTag ("GameEngine").GetComponent<ObjectPool>();
		getSpawnPoints ();
		StartCoroutine(randomiseEnemySpawn());
        StartCoroutine(randomiseItemSpawn());
	}
	
	public void ResetCamera() {
		transform.position = new Vector3(transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
		StartCoroutine(resetCameraEnemies());
		StartCoroutine(resetCameraItems());
	}
	
	public void DestroyAll() {
		StartCoroutine(destroyEnemies());
		StartCoroutine(destroyItems());
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

	private IEnumerator randomiseEnemySpawn () {
		foreach (Transform spawn in enemySpawns) {
			int spawnChance = UnityEngine.Random.Range (0, 100);

			if (spawnChance < (Variables.EnemySpawnPerc * 100)) {
				string path = "Prefabs/" + chooseRandomEnemy();

				GameObject obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(spawn.position.x, spawn.position.y, 0.1f), spawn.rotation);
				enemyList.Add(obj);
			}
		}

        yield break;
	}

	private IEnumerator randomiseItemSpawn() {
		foreach (Transform spawn in itemSpawns) {
			int spawnChance = UnityEngine.Random.Range (0, 100);
			
			if (spawnChance < (Variables.ItemSpawnPerc * 100)) {
				string path = "Prefabs/" + chooseRandomItem();
				
				GameObject obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(spawn.position.x, spawn.position.y, 0.1f), spawn.rotation);
				itemList.Add(obj);
			}
		}

        yield break;
    }


	private IEnumerator resetCameraEnemies() {
		foreach (GameObject obj in enemyList) {
			if(obj != null) {
				if (obj.tag == "WormCluster")
					obj.GetComponent<WormGroup>().ResetCamera();
				else
					obj.GetComponent<CameraReset>().ResetCamera ();
			}
		}

        yield break;
	}
	
	private IEnumerator resetCameraItems() {
		foreach (GameObject obj in itemList) {
			if(obj != null) {
				PoopItem item = obj.GetComponent<PoopItem>();
				item.ResetCamera ();
			}
		}

        yield break;
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
	
	private IEnumerator destroyEnemies() {
		foreach (GameObject obj in enemyList) {
			if (obj.tag == "WormCluster") {
				//((WormGroup)obj.GetComponent("WormGroup")).DestroyEnemies();
				List<GameObject> worms = obj.GetComponent<WormGroup>().GetWorms();
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

        yield break;
	}
	
	private IEnumerator destroyItems() {
		foreach (GameObject obj in itemList)
			Destroy (obj);

		itemList.Clear ();

        yield break;
    }
}
