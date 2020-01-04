using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEngine : MonoBehaviour {

	private Variables.GameState _gameState = Variables.GameState.LoadGame;
	private bool gameStarted;
	private bool gameFinished;
	private bool menuVisible;
	private bool showHighScore;
	private SpriteRenderer gameTitle;
	private SpriteRenderer highscoreLabel;
	private MeshRenderer highscore;
	private TextMesh highscoreMesh;
	private GUIText highscoreInGame;
	private SpriteRenderer replayLabel;
	private SpriteRenderer bgDimmer;
	private GameObject title;
	private GameObject startPrompt;
	private GameObject mainCamera;
	private GameObject menuCamera;
    private GameObject player;
	private List<GameObject> visibleButtons;
    private Player playerScript;
	private int highScore;
	private int currentScore;
	private int screenHeight;
	private float alpha;
	private Texture healthForeground;
	private Texture healthBackground;
	private CameraMovement cameraMovement;
	private LevelChunkFactory chunkFactory;
	private BackgroundController backgroundController;
	private AudioClip[] gameMusic;

	public Variables.GameState GameState {
		get { return _gameState; }
		set { _gameState = value; }
	}
	
	// Use this for initialization
	void Start () {
		alpha = Variables.DefaultAlpha;

		if (!PlayerPrefs.HasKey ("HighScore"))
			PlayerPrefs.SetInt ("HighScore", 0);

		highScore = PlayerPrefs.GetInt("HighScore");

		initialiseMenus ();
		initialiseAudio ();

		cameraMovement = (CameraMovement)GameObject.FindGameObjectWithTag ("MainCamera").GetComponent("CameraMovement");
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		menuCamera = GameObject.FindGameObjectWithTag ("MenuCamera");
        
		menuCamera.GetComponent<Camera>().enabled = true;
		mainCamera.GetComponent<Camera>().enabled = false;

        Screen.orientation = ScreenOrientation.Portrait;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape))
			Application.Quit ();

		if (GameState == Variables.GameState.GameStart) {
			if (player == null)
				highscoreInGame.text = string.Empty;
			else
				highscoreInGame.text = playerScript.Score.ToString();
			
			playAudio ();
			checkPlayerDeath ();
			if (cameraMovement.transform.position.y >= Variables.MapRestartPoint)
				resetCamera();
		} else if (GameState == Variables.GameState.GameEnd) {
			showReplayButton ();
		} else if (GameState == Variables.GameState.TitleScreen) {
			if (alpha >= 1)
				checkTitleTouch ();
			else
				fadeInComponents();
			
		} else if (GameState == Variables.GameState.LoadGame) {
			StartCoroutine (checkLoadAnimation());
		}
		
		repositionHighscore ();
	}

	//void FixedUpdate () {
	//	if (GameState == Variables.GameState.GameStart) {
	//		if (player == null)
	//			highscoreInGame.text = string.Empty;
	//		else
	//			highscoreInGame.text = playerScript.Score.ToString();
	//		
	//		playAudio ();
	//		checkPlayerDeath ();
	//		if (cameraMovement.transform.position.y >= Variables.MapRestartPoint)
	//			resetCamera();
	//	} else if (GameState == Variables.GameState.GameEnd) {
	//		showReplayButton ();
	//	} else if (GameState == Variables.GameState.TitleScreen) {
	//		if (alpha >= 1)
	//			checkTitleTouch ();
	//		else
	//			fadeInComponents();
	//		
	//	} else if (GameState == Variables.GameState.LoadGame) {
	//		StartCoroutine (checkLoadAnimation());
	//	}
	//}

	public void ResetGame () {
        if (player == null)
        {
            player = (GameObject)Instantiate(Resources.Load("Prefabs/" + Variables.PlayerPrefabName), Variables.PlayerPos, Variables.DefaultQuaternion);
            playerScript = player.GetComponent<Player>();
            playerScript.Reset();
            chunkFactory.Reset();
            backgroundController.Reset();
            //resetCamera();
            hideReplayButton();
            GameState = Variables.GameState.GameStart;
        }
	}

	private void resetCamera () {
        playerScript.ResetCamera();
		cameraMovement.ResetCamera();
		chunkFactory.ResetCamera();
		backgroundController.ResetCamera();
	}

	private void initialiseMenus () {
		GameObject titleObj = (GameObject)Instantiate (Resources.Load("GUI/" + Variables.GameTitle), Variables.TitlePos, Variables.DefaultQuaternion);
		GameObject highscoreLabelObj = (GameObject)Instantiate (Resources.Load("GUI/" + Variables.HighscoreLabel), Variables.highscoreLabelPos, Variables.DefaultQuaternion);
		GameObject highscoreObj = (GameObject)Instantiate (Resources.Load("GUI/" + Variables.Highscore), Variables.highscorePos, Variables.DefaultQuaternion);
		GameObject replayObj = (GameObject)Instantiate (Resources.Load("GUI/" + Variables.ReplayButton), Variables.DefaultPos, Variables.DefaultQuaternion);
		GameObject bgDimmerObj = (GameObject)Instantiate (Resources.Load("GUI/" + Variables.BackgroundDimmer), Variables.DefaultPos, Variables.DefaultQuaternion);

		gameTitle = titleObj.GetComponent<SpriteRenderer> ();
		highscoreLabel = highscoreLabelObj.GetComponent<SpriteRenderer> ();
		highscore = highscoreObj.GetComponent<MeshRenderer> ();
		highscoreMesh = highscoreObj.GetComponent<TextMesh> ();
		replayLabel = replayObj.GetComponent<SpriteRenderer> ();
		bgDimmer = bgDimmerObj.GetComponent<SpriteRenderer> ();

		highscoreInGame = (GUIText)GameObject.FindGameObjectWithTag (Variables.HighscoreInGame).GetComponent<GUIText> ();

		gameTitle.color = new Color (1f, 1f, 1f, alpha);
		highscoreLabel.color = new Color (1f, 1f, 1f, alpha);
		highscoreMesh.color = new Color (highscoreMesh.color.r, highscoreMesh.color.g, highscoreMesh.color.b, alpha);

		gameTitle.enabled = false;
		highscoreLabel.enabled = false;
		highscore.enabled = false;
		replayLabel.enabled = false;
		bgDimmer.enabled = false;

		highscoreMesh.text = highScore.ToString();
	}

	private void initialiseAudio () {
		gameMusic = new AudioClip[Variables.GameMusic.Length];

		for (int i = 0; i < gameMusic.Length; i++)
			gameMusic[i] = (AudioClip)Resources.Load("Audio/" + Variables.GameMusic[i], typeof(AudioClip));
	}

	private void initialiseGameObjects () {
        player = (GameObject)Instantiate(Resources.Load("Prefabs/" + Variables.PlayerPrefabName), Variables.PlayerPos, Variables.DefaultQuaternion);
		GameObject bgControlObj = (GameObject)Instantiate (Resources.Load("Game Managers/" + Variables.BackgroundControllerName), Variables.DefaultPos, Variables.DefaultQuaternion);
		GameObject chunkFactoryObj = (GameObject)Instantiate (Resources.Load("Game Managers/" + Variables.ChunkFactoryName), Variables.DefaultPos, Variables.DefaultQuaternion);

        //player = playerObj;
        playerScript = player.GetComponent<Player>();
		backgroundController = (BackgroundController)bgControlObj.GetComponent ("BackgroundController");
		chunkFactory = (LevelChunkFactory)chunkFactoryObj.GetComponent ("LevelChunkFactory");

		highscoreInGame.enabled = true;
	}
	
	private void checkPlayerDeath () {
		if (!(player == null || playerScript.isAlive)) {
            setHighScore(playerScript.Score);
            Destroy(player);
			GameState = Variables.GameState.GameEnd;
		}
	}
	
	private void setHighScore (int score) {
		if (score > PlayerPrefs.GetInt ("HighScore")) {
			PlayerPrefs.SetInt ("HighScore", score);
			highScore = score;
		}	
	}
	
	private void startGame () {
		gameFinished = false;
		gameStarted = true;
		Time.timeScale = 1;
	}
	
	private void stopGame (int score) {
		Time.timeScale = 0;
		gameStarted = false;
	}
	
	private void clearButtons () {
		foreach (GameObject button in visibleButtons) {
			button.GetComponent<GUITexture>().enabled = false;
		}
		visibleButtons.Clear ();
	}

	private void playAudio () {
		if (GetComponent<AudioSource>().isPlaying) 
			return;

		GetComponent<AudioSource>().clip = gameMusic[Random.Range(0,gameMusic.Length)];
		GetComponent<AudioSource>().Play();
	}

	private IEnumerator checkLoadAnimation () {
		GameObject obj = GameObject.FindGameObjectWithTag (Variables.TitleAnimationName);
		SpriteRenderer sprite = obj.GetComponent<SpriteRenderer> ();
		if (sprite.sprite.name == "TitleAnimation_0") {
			Animator animator = obj.GetComponent<Animator> ();
			yield return new WaitForSeconds(2);
			animator.SetBool("IsRunning", true);
		}

		if (sprite.sprite.name == "TitleAnimation_54") {
			GameState = Variables.GameState.TitleScreen;
			gameTitle.enabled = true;
			highscoreLabel.enabled = true;
			highscore.enabled = true;
		}
	}

	private void fadeInComponents () {
		alpha += 0.01f;
		gameTitle.color = new Color (1f, 1f, 1f, alpha);
		highscoreLabel.color = new Color (1f, 1f, 1f, alpha);
		highscoreMesh.color = new Color (highscoreMesh.color.r, highscoreMesh.color.g, highscoreMesh.color.b, alpha);
	}

	private void checkTitleTouch () {
		// Computer
		if (Input.GetMouseButton(0))
		{
			Vector3 pos = Input.mousePosition;
			
			Vector3 wp = menuCamera.GetComponent<Camera>().ScreenToWorldPoint(pos);//Camera.main.ScreenToWorldPoint(pos);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			GameObject obj = GameObject.FindGameObjectWithTag (Variables.TitleAnimationName);
			Collider2D collider = obj.GetComponent<Collider2D> ();
			if (collider.Equals(Physics2D.OverlapPoint(touchPos))) {
				GameState = Variables.GameState.GameStart;
		
				initialiseGameObjects();
				mainCamera.GetComponent<Camera>().enabled = true;
				menuCamera.GetComponent<Camera>().enabled = false;
		
				Destroy (obj);
				Destroy (GameObject.FindGameObjectWithTag (Variables.GameTitle));
				Destroy (GameObject.FindGameObjectWithTag (Variables.Highscore));
				Destroy (GameObject.FindGameObjectWithTag (Variables.HighscoreLabel));
			}
		}

		// Touch Devices
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
			Vector3 wp = menuCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(0).position);
			Vector2 touchPos = new Vector2(wp.x, wp.y);
			GameObject obj = GameObject.FindGameObjectWithTag (Variables.TitleAnimationName);
			Collider2D collider = obj.GetComponent<Collider2D> ();
			if (collider == Physics2D.OverlapPoint(touchPos))
			{
				GameState = Variables.GameState.GameStart;
				
				initialiseGameObjects();
				mainCamera.GetComponent<Camera>().enabled = true;
				menuCamera.GetComponent<Camera>().enabled = false;
			}
		}
	}

	private void showReplayButton () {
		Vector3 camPos = mainCamera.transform.position;

		replayLabel.transform.position = new Vector3(camPos.x, camPos.y, -1);
		bgDimmer.transform.position = new Vector3(camPos.x, camPos.y, -1);

		replayLabel.enabled = true;
		bgDimmer.enabled = true;
	}

	private void hideReplayButton () {
		replayLabel.enabled = false;
		bgDimmer.enabled = false;
	}

	private void repositionHighscore () {
		if (screenHeight != Screen.height) {
			float highScoreOffsetX = Variables.HighScoreRatio * -Screen.height;
			float highScoreOffsetY = Screen.height * 0.9f;
			highscoreInGame.pixelOffset = new Vector2 (highScoreOffsetX ,highScoreOffsetY);
			highscoreInGame.fontSize = Screen.height / 10;
			
			screenHeight = Screen.height;
		}
	}
}
