using UnityEngine;
using System.Collections;

public class Crab : ObjectEntity {

	//void Update () {
	//	if(Time.deltaTime > 0)
	//		transform.Translate (Variables.CrabSpeed);
	//}

	void FixedUpdate () {
		if(Time.deltaTime > 0)
			transform.Translate (Variables.CrabSpeed);
	}

}
