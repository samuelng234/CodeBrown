using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private GameObject collisionObj;
	private bool _isAlive;
	private bool _chunkTriggerCollision;
	private int _distance;
	private int camResetCount;
	private int currentSize;
	private int _score;
	private int previousScore;
	private float startTime;
	private float runSpeed;
	private bool _isInvulnerable;
	private bool enemyTriggered = false;
	private bool _backgroundTriggered = false;
	private bool _liquidTriggered = false;
	private Animator _animator;
	private AudioClip[] farts;
	
	public bool chunkTriggerCollision {
		get { return _chunkTriggerCollision; }
		set { _chunkTriggerCollision = value; }
	}
	
	public bool isAlive {
		get { return _isAlive; }
		set { _isAlive = value; }
	}

	public bool IsInvulnerable {
		get { return _isInvulnerable; }
		set { _isInvulnerable = value; }
	}

	public bool BackgroundTriggered {
		get { return _backgroundTriggered; }
		set { _backgroundTriggered = value; }
	}

	public bool LiquidTriggered {
		get { return _liquidTriggered; }
		set { _liquidTriggered = value; }
	}

	public Animator Animator {
		get { return _animator; }
		set { _animator = value; }
	}
	
	public int Distance {
		get { return (camResetCount * 100) + (int)transform.position.y + 3; }
		internal set { _distance = value; }
	}

	public int Score {
		get { return _score; }
		internal set { _score = value; }
	}
	
	// Use this for initialization
	void Start () {
		isAlive = true;
		IsInvulnerable = false;
		chunkTriggerCollision = false;
		Animator = this.GetComponent<Animator>();
		Distance = 0;
		camResetCount = 0;
		currentSize = 1;
		Score = 0;
		startTime = Time.time;
		runSpeed = Variables.RunSpeed;

		farts = new AudioClip[Variables.Farts.Length];

		for (int i = 0; i < farts.Length; i++)
			farts[i] = (AudioClip)Resources.Load("Audio/" + Variables.Farts[i], typeof(AudioClip));
	}
	
	// Update is called once per frame
	//void Update () {
	//	if (currentSize < Variables.MinSize)
	//		isAlive = false;
	//	
	//	run ();
	//	strafe ();
	//	
	//	Score = (int)(Time.time - startTime);
	//}

	void FixedUpdate () {
		if (currentSize < Variables.MinSize)
			isAlive = false;

		run ();
		strafe ();

		Score = (int)(Time.time - startTime);
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		collisionObj = col.gameObject;
		string tag = col.tag;

		if (!IsInvulnerable) {
			if (tag == "Crab" || tag == "Star" || tag == "Worm1" || tag == "Worm2" ||tag == "Worm3" ||tag == "Worm4" ||tag == "BossHead" || tag == "BossBody" || tag == "BossTail") {

				StartCoroutine ("setInvulnerable");
				decreaseSize();
				playAudio();
				enemyTriggered = true;
			}
		}

		if (tag == "Poop") {
			increaseSize ();
			collisionObj.SetActive (false);
			checkBoss ();
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		collisionObj = null;
	}

	public void Reset () {
		isAlive = true;
		IsInvulnerable = false;
		chunkTriggerCollision = false;
		Animator = this.GetComponent<Animator>();
		Distance = 0;
		camResetCount = 0;
		currentSize = 1;
	}
	
	public IEnumerator ResetCamera() {
		transform.position = new Vector3(transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
		camResetCount++;

        yield break;
	}

	private void run () {
		if (Time.deltaTime > 0) {
			if ((Score % 60) == 0) {
				if (Score != 0 && runSpeed <= Variables.MaxRunSpeed && previousScore != Score) {
					runSpeed = runSpeed + Variables.RunSpeedIncrement;
					previousScore = Score;
				}
			}

			transform.Translate (0, runSpeed, 0);
		}
	}
	
	private void strafe () {
		Transform temp = this.transform;
		// Computer
		temp.Translate (Input.GetAxis ("Horizontal") / 20, 0, 0);

		// Touch Devices
		temp.Translate (Input.acceleration.x * Variables.StrafeSpeed * Time.deltaTime, 0, 0);

		float x = 0;
		if (currentSize == 1)
			x = Mathf.Clamp (temp.position.x, -Variables.SmallBorderX, Variables.SmallBorderX);
		else if (currentSize == 2)
			x = Mathf.Clamp (temp.position.x, -Variables.SmallMediumBorderX, Variables.SmallMediumBorderX);
		else if (currentSize == 3)
			x = Mathf.Clamp (temp.position.x, -Variables.MediumBorderX, Variables.MediumBorderX);
		else if (currentSize == 4)
			x = Mathf.Clamp (temp.position.x, -Variables.MediumLargeBorderX, Variables.MediumLargeBorderX);
		else if (currentSize == 5)
			x = Mathf.Clamp (temp.position.x, -Variables.LargeBorderX, Variables.LargeBorderX);
		else if (currentSize == 6)
			x = Mathf.Clamp (temp.position.x, -Variables.MaxBorderX, Variables.MaxBorderX);
		
		this.transform.position = new Vector3(x, temp.position.y, temp.position.z);
	}

	private void playAudio () {
		GetComponent<AudioSource>().clip = farts[Random.Range(0,farts.Length)];
		GetComponent<AudioSource>().Play();
	}

	private void checkBoss () {
		int spawnChance = UnityEngine.Random.Range (0, 100);
		
		if (spawnChance < (Variables.BossSpawnPerc * 100)) {
			LevelChunkFactory factory = GameObject.FindGameObjectWithTag("ChunkFactory").GetComponent<LevelChunkFactory>();
			factory.createBoss ();
		}
	}

	public void decreaseSize(){
		currentSize--;
		Animator.SetInteger ("CurrentSize", currentSize);
	}

	IEnumerator setInvulnerable () {
		IsInvulnerable = true;
		Animator.SetBool ("IsInvulnerable", IsInvulnerable);
		yield return new WaitForSeconds (1);
		IsInvulnerable = false;
		Animator.SetBool ("IsInvulnerable", IsInvulnerable);
	}

	public void increaseSize(){
		currentSize = currentSize == 6 ? currentSize : currentSize + 1;
		Animator.SetInteger ("CurrentSize", currentSize);
	}
}
