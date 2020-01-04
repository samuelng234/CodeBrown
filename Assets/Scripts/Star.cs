using UnityEngine;
using System.Collections;

public class Star : ObjectEntity {

	private float speed;

	// Use this for initialization
	void Start () {
		float maxSpeed = Variables.StarMaxSpeed * 1000;
		speed = Random.Range (-maxSpeed, maxSpeed) / 1000;
	}

	//void Update () {
	//	if (Time.deltaTime > 0) {
	//		Transform temp = transform;
	//		temp.transform.Translate (new Vector3(speed, 0, 0));
	//		float x = Mathf.Clamp (temp.position.x, -Variables.SmallBorderX, Variables.SmallBorderX);
	//		transform.position = new Vector3(x, temp.position.y, temp.position.z);
	//
	//		if (x >= Variables.SmallBorderX || x <= -Variables.SmallBorderX)
	//			speed = -speed;
	//	}
	//}

	void FixedUpdate () {
		if (Time.deltaTime > 0) {
			Transform temp = transform;
			temp.transform.Translate (new Vector3(speed, 0, 0));
			float x = Mathf.Clamp (temp.position.x, -Variables.SmallBorderX, Variables.SmallBorderX);
			transform.position = new Vector3(x, temp.position.y, temp.position.z);
			
			if (x >= Variables.SmallBorderX || x <= -Variables.SmallBorderX)
				speed = -speed;
		}
	}
}
