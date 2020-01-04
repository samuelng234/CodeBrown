using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public GameObject player = null;
	public GameEngine gameEngine;
	public Vector2 velocity;
	
	// Use this for initialization
	void Start () {

		gameEngine = (GameEngine)GameObject.FindWithTag ("GameEngine").GetComponent("GameEngine");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameEngine.GameState == Variables.GameState.GameStart) {
			if (player == null) {
				player = GameObject.FindWithTag ("Player");
			} else {
				Vector3 playerPos = player.transform.position;
				float targetClampY = Mathf.Lerp (playerPos.y + 2.1f + 0.1f, playerPos.y + 2.3f - 0.1f, playerPos.y + 2.2f);
				transform.position = new Vector3 (transform.position.x, targetClampY, transform.position.z);
			}
		}
	}
	
	public void ResetCamera() {
		transform.position = new Vector3 (transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}
}
