using UnityEngine;
using System.Collections;

public class PoopItem : MonoBehaviour {

	private bool triggered = false;

	public void ResetCamera() {
		transform.position = new Vector3(transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}

}
