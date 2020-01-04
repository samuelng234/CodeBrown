using UnityEngine;
using System.Collections;

public class CameraReset : MonoBehaviour {

	public void ResetCamera() {
		transform.position = new Vector3 (transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}
}
