using UnityEngine;
using System.Collections;

public class MousePressReplay : MonoBehaviour {

	private GameObject mainCamera;
	private GameEngine gameEngine;
	private bool pressed;

	// Use this for initialization
	void Start () {
		pressed = false;
		gameEngine = (GameEngine)GameObject.FindWithTag ("GameEngine").GetComponent("GameEngine");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
	}

	// Touch Device
	//void Update () {
	//	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
	//		Vector3 wp = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(0).position);
	//		Vector2 touchPos = new Vector2(wp.x, wp.y);
	//		Collider2D collider = Physics2D.OverlapPoint(touchPos);
	//		if (collider.transform.gameObject.tag == "ReplayButton")
	//			gameEngine.ResetGame();
	//	}
	//}

	void FixedUpdate () {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            pressed = true;
            Vector3 wp = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            Collider2D collider = Physics2D.OverlapPoint(touchPos);
            if (collider.transform.gameObject.tag == "ReplayButton")
                gameEngine.ResetGame();
        }
        else if (Input.touchCount > 0 && pressed && Input.GetTouch(0).phase == TouchPhase.Ended)
            pressed = false;
	}

	// Computer
	void OnMouseDown () {
		pressed = true;
	}
	
	void OnMouseUp () {
		if (pressed) {
			gameEngine.ResetGame();
			pressed = false;
		}
	}
}
