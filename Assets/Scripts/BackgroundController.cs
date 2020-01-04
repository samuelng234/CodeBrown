using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundController : MonoBehaviour {

	private GameObject cameraObj;
	private CameraMovement cameraMovement;
	private Queue<GameObject> backgroundSprites;
	private Queue<GameObject> liquidSprites;
	private int previousBackground;
	private int previousLiquid;
	private Vector3 nextBackgroundPos;
	private Vector3 nextLiquidPos;
	private Vector3 treePosition;
	private bool _backgroundTrigger;
	private bool _liquidTrigger;
	private bool _treeTrigger;
	
	public bool BackgroundTrigger {
		get { return _backgroundTrigger; }
		set { _backgroundTrigger = value; }
	}

	public bool LiquidTrigger {
		get { return _liquidTrigger; }
		set { _liquidTrigger = value; }
	}
	
	// Use this for initialization
	void Start () {
		previousBackground = -1;
		previousLiquid = -1;
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraMovement = (CameraMovement)(cameraObj.GetComponent("CameraMovement"));
		backgroundSprites = new Queue<GameObject> ();
		liquidSprites = new Queue<GameObject> ();
		loadStart ();
		BackgroundTrigger = false;
		LiquidTrigger = false;
	}
	
	// Update is called once per frame
	//void Update () {
	//	if (BackgroundTrigger)
	//		addBackground ();
	//
	//	if (LiquidTrigger)
	//		addLiquid ();
	//}

	void FixedUpdate () {
		if (BackgroundTrigger)
			addBackground ();
		
		if (LiquidTrigger)
			addLiquid ();
	}

	public void Reset () {
		while (backgroundSprites.Count > 0) {
			GameObject obj = backgroundSprites.Dequeue ();
			Destroy (obj);
		}

		while (liquidSprites.Count > 0) {
			GameObject obj = liquidSprites.Dequeue ();
			Destroy (obj);
		}
		
		initialise ();
	}
	
	public void ResetCamera() {
		resetCameraBackground ();
		resetCameraLiquid ();
	}

	private void initialise () {
		previousBackground = -1;
		previousLiquid = -1;
		cameraObj = GameObject.FindGameObjectWithTag ("MainCamera");
		cameraMovement = (CameraMovement)(cameraObj.GetComponent("CameraMovement"));
		backgroundSprites = new Queue<GameObject> ();
		liquidSprites = new Queue<GameObject> ();
		loadStart ();
		BackgroundTrigger = false;
		LiquidTrigger = false;
	}
	
	private void loadStart () {
		nextBackgroundPos = Variables.BackgroundStartPosition;
		nextLiquidPos = Variables.LiquidStartPosition;

		while (backgroundSprites.Count < Variables.BackgroundMaxCount) {
			GameObject background = getRandomBackground ();
			backgroundSprites.Enqueue(background);
			nextBackgroundPos = new Vector3(nextBackgroundPos.x, nextBackgroundPos.y + Variables.BackgroundSeperation, nextBackgroundPos.z);
		}

		GameObject liquid = getRandomLiquid ();
		liquidSprites.Enqueue(liquid);
		nextLiquidPos = new Vector3(nextLiquidPos.x, nextLiquidPos.y + Variables.LiquidSeperation, nextLiquidPos.z);
	}
	
	private void addBackground () {
		bool outOfCamera = outCameraFOV ();

		if (outOfCamera) {
			GameObject background = getRandomBackground ();
			backgroundSprites.Enqueue (background);

			nextBackgroundPos = new Vector3 (nextBackgroundPos.x, nextBackgroundPos.y + Variables.BackgroundSeperation, nextBackgroundPos.z);
			backgroundDequeue ();
			BackgroundTrigger = false;
		}
	}
	
	private void addLiquid () {
		GameObject liquid = getRandomLiquid ();
		liquidSprites.Enqueue(liquid);
		
		nextLiquidPos = new Vector3(nextLiquidPos.x, nextLiquidPos.y + Variables.LiquidSeperation, nextLiquidPos.z);
		liquidDequeue ();
		LiquidTrigger = false;
	}
	
	private void resetCameraBackground () {
		foreach (GameObject obj in backgroundSprites) {
			obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - Variables.MapRestartPoint, obj.transform.position.z);
		}
		nextBackgroundPos.y = nextBackgroundPos.y - Variables.MapRestartPoint;
	}

	private void resetCameraLiquid () {
		foreach (GameObject obj in liquidSprites) {
			obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y - Variables.MapRestartPoint, obj.transform.position.z);
		}
		nextLiquidPos.y = nextLiquidPos.y - Variables.MapRestartPoint;
	}
	
	private void backgroundDequeue () {
		if (backgroundSprites.Count > Variables.BackgroundMaxCount) {
			GameObject obj = backgroundSprites.Dequeue ();
			Destroy (obj);
		}
	}

	private void liquidDequeue () {
		if (liquidSprites.Count > Variables.LiquidMaxCount) {
			GameObject obj = liquidSprites.Dequeue ();
			Destroy (obj);
		}
	}

	private GameObject getRandomBackground () {
		int randomBackground;
		GameObject gameObj = null;

		do {
			randomBackground = UnityEngine.Random.Range (0, 6);
		} while (previousBackground == randomBackground);
	
		string path = "Backgrounds/" + Variables.BackgroundChunks[randomBackground];
		Object obj = Resources.Load (path);
		
		gameObj = (GameObject)Instantiate (obj, nextBackgroundPos, Variables.DefaultQuaternion);
		previousBackground = randomBackground;

		return gameObj;
	}

	private GameObject getRandomLiquid () {
		int randomLiquid;
		GameObject gameObj = null;
		
		do {
			randomLiquid = UnityEngine.Random.Range (0, 5);
		} while (previousLiquid == randomLiquid);
		
		string path = "Backgrounds/" + Variables.LiquidChunks[randomLiquid];
		Object obj = Resources.Load (path);
		
		gameObj = (GameObject)Instantiate (obj, nextLiquidPos, Variables.DefaultQuaternion);
		previousLiquid = randomLiquid;
		
		return gameObj;
	}

	private bool outCameraFOV () {
		GameObject obj = backgroundSprites.Peek ();
		float seperation = cameraObj.transform.position.y - obj.transform.position.y;
		if (seperation >= 4.5f)
			return true;

		return false;
	}
}
