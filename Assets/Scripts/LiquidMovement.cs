using UnityEngine;
using System.Collections;

public class LiquidMovement : MonoBehaviour {

	//void Update () {
	//	if(Time.deltaTime > 0)
	//		transform.Translate (0,  Variables.LiquidSpeed, 0);
	//}

	void FixedUpdate () {
		if(Time.deltaTime > 0)
			transform.Translate (0,  Variables.LiquidSpeed, 0);
	}

}
