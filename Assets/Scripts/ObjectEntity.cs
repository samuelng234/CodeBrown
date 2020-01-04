using UnityEngine;
using System.Collections;

public class ObjectEntity : MonoBehaviour {

	private bool _isAlive = true;
	private GameObject collisionObj;
	
	public bool isAlive {
		get { return _isAlive; }
		set { _isAlive = value; }
	}

	// Use this for initialization
	void Start () {
	
	}

	public void ResetCamera() {
		transform.position = new Vector3(transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}
}
