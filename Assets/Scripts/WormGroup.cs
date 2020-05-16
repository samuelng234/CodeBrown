using UnityEngine;
using System.Collections.Generic;

public class WormGroup : MonoBehaviour {

	private List<GameObject> worms = new List<GameObject>();
	private ObjectPool pool;

	// Use this for initialization
	void Start () {
		int randChoice;

		pool = GameObject.FindGameObjectWithTag ("GameEngine").GetComponent<ObjectPool>(); ;
        
        foreach (Transform spawn in transform) {
			randChoice = Random.Range (0, Variables.WormSprites.Length);
			GameObject obj = null;
			Worm worm = null;

			if (randChoice == 0)
				obj = pool.GetInactiveWorm1();
			else if (randChoice == 1)
				obj = pool.GetInactiveWorm2();
			else if (randChoice == 2)
				obj = pool.GetInactiveWorm3();
			else if (randChoice == 3)
				obj = pool.GetInactiveWorm4();
					
			obj.transform.position = new Vector3(spawn.position.x, spawn.position.y, 0.1f);
			obj.SetActive(true);
			worm = obj.GetComponent<Worm>();
			worm.SetSpeed ();
			worm.CheckOutOfBounds ();
			worms.Add (worm.gameObject);

			//float x = spawn.position.x;
			//if (!(x >= Variables.SmallBorderX || x <= -Variables.SmallBorderX)) {
			//	string path = "Prefabs/" + Variables.WormSprites[randChoice];
			//	GameObject obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(spawn.position.x, spawn.position.y, 0.1f), spawn.rotation);
			//	worms.Add(obj);s
			//}
		}
	}

	public void DestroyEnemies () {
		//foreach (GameObject obj in worms)
		//	Destroy (obj);
		foreach (GameObject obj in worms) {
			if (obj.tag == "Worm1")
				pool.AddWorm1(obj);
			else if (obj.tag == "Worm2")
				pool.AddWorm2(obj);
			else if (obj.tag == "Worm3")
				pool.AddWorm3(obj);
			else if (obj.tag == "Worm4")
				pool.AddWorm4(obj);
		}

		worms.Clear ();
	}

	public void ResetCamera() {
		foreach (GameObject obj in worms)
			obj.GetComponent<Worm>().ResetCamera ();
	}

	public List<GameObject> GetWorms () {
		return worms;
	}
}
