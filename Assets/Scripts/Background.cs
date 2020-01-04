using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundGrass : MonoBehaviour {

	private List<Transform> stoneTriggers;
	private List<Transform> plantTriggers;
	private List<GameObject> stones;
	private List<GameObject> plants;
	
	// Use this for initialization
	void Start () {
		stoneTriggers = new List<Transform> ();
		plantTriggers = new List<Transform> ();
		stones = new List<GameObject> ();
		plants = new List<GameObject> ();
		getSpawnPoints ();
		randomiseStones ();
		randomisePlants ();
	}
	
	void OnDestroy () {
		destroyStones ();
		destroyPlants ();
	}
	
	private void getSpawnPoints() {
		foreach (Transform child in transform) {
			switch (child.tag) {
			case ("Stone_Trigger") : 
				stoneTriggers.Add (child);
				break;
			case ("Plant_Trigger") :
				plantTriggers.Add (child);
				break;
			}
		}
	}
	
	private void randomiseStones () {
		foreach (Transform obj in stoneTriggers) {
			if (UnityEngine.Random.Range(0,100) > 60)
				stones.Add ((GameObject)Instantiate (Resources.Load ("Prefabs/Stone"), obj.position, obj.rotation));
		}
	}
	
	private void randomisePlants () {
		foreach (Transform obj in plantTriggers) {
			if (UnityEngine.Random.Range(0,100) > 60)
				plants.Add ((GameObject)Instantiate (Resources.Load ("Prefabs/Plant"), obj.position, obj.rotation));
		}
	}
	
	private void destroyStones() {
		foreach (GameObject obj in stones)
			Destroy (obj);
		
		stones.Clear ();
	}
	
	private void destroyPlants() {
		foreach (GameObject obj in plants)
			Destroy (obj);
		
		plants.Clear ();
	}
}
