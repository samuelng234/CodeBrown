using UnityEngine;
using System.Collections;

public class Worm : ObjectEntity {

	private float speed;

	// Use this for initialization
	void Start () {
		//float x = transform.position.x;
		//if (x >= Variables.SmallBorderX || x <= -Variables.SmallBorderX)
		//	Destroy (gameObject);

		//speed = Random.Range (Variables.WormMinSpeed * 1000, Variables.WormMaxSpeed * 1000) / 1000;
	}

	//void Update () {
	//	if (Time.deltaTime > 0) {
	//
	//		transform.Translate (new Vector3 (0, speed, 0));
	//	}
	//}

	void FixedUpdate () {
		if (Time.deltaTime > 0) {
			
			transform.Translate (new Vector3 (0, speed, 0));
		}
	}

	public void ResetCamera() {
		transform.position = new Vector3 (transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}

	public void SetSpeed () {
		speed = Random.Range (Variables.WormMinSpeed * 1000, Variables.WormMaxSpeed * 1000) / 1000;
	}

	public void CheckOutOfBounds () {
		float x = transform.position.x;

		if (x >= Variables.SmallBorderX || x <= -Variables.SmallBorderX)
			gameObject.SetActive (false);
	}
}
