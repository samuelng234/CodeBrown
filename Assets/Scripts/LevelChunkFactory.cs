using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelChunkFactory : MonoBehaviour {
	
	private CameraMovement camera;
	private bool _chunkTriggered;
	private Queue<LevelChunk> LevelChunkQueue;
	private Boss boss;
	private Vector3 endChunkPos;
	private Player player;
	private int previousChunk;

	public bool ChunkTriggered {
		get { return _chunkTriggered; }
		set { _chunkTriggered = value; }
	}
	
	// Use this for initialization
	void Start () {
		initialise ();
	}
	
	// Update is called once per frame
	//void Update () {
	//	if (ChunkTriggered && boss == null)
	//		triggerNewChunk();
	//}

	void FixedUpdate () {
		if (ChunkTriggered && boss == null)
            StartCoroutine(triggerNewChunk());
	}

	public void Reset () {
		if (boss != null)
			boss.Destroy ();

		while (LevelChunkQueue.Count > 0)
			dequeueChunk();
		
		initialise ();
	}

	private void initialise () {
		LevelChunkQueue = new Queue<LevelChunk> ();
		camera = (CameraMovement)(GameObject.FindGameObjectWithTag ("MainCamera").GetComponent("CameraMovement"));
		GameObject temp = GameObject.FindGameObjectWithTag ("Player");
		player = (Player)temp.GetComponent ("Player");
		enqueueChunk(getRandomChunk(), Variables.FirstMapVector, Variables.DefaultQuaternion);
		endChunkPos = Variables.FirstMapVector;
		ChunkTriggered = false;
		boss = null;
	}
	
	public IEnumerator ResetCamera() {
		endChunkPos.y = endChunkPos.y - Variables.MapRestartPoint;
		foreach (LevelChunk chunk in LevelChunkQueue) {
			if (chunk != null)
				chunk.ResetCamera ();
		}

		if (boss != null)
			boss.ResetCamera ();

        yield break;
    }
	
	private IEnumerator triggerNewChunk() {
		endChunkPos.y = endChunkPos.y + Variables.LevelChunkSeperation;
		enqueueChunk(getRandomChunk(), endChunkPos, Variables.DefaultQuaternion);
		if (LevelChunkQueue.Count > 3)
			dequeueChunk ();

        yield break;
    }
	
	private int getRandomChunk() {
		return UnityEngine.Random.Range (0, Variables.LevelChunks.Length);
	}
	
	private void enqueueChunk(int chunk, Vector3 position, Quaternion rotation) {
		string loadString = "Spawn Chunks/" + Variables.LevelChunks [chunk];
		GameObject g = (GameObject)Instantiate (Resources.Load(loadString), position, rotation);
		LevelChunkQueue.Enqueue((LevelChunk)g.GetComponent (typeof(LevelChunk)));
		ChunkTriggered = false;
	}
	
	private void dequeueChunk() {
		LevelChunk popChunk = LevelChunkQueue.Dequeue ();
		popChunk.DestroyAll ();
		Destroy (popChunk.gameObject);
	}

	public void createBoss () {
		if (boss == null) {
			Vector3 position = new Vector3 (endChunkPos.x, endChunkPos.y + 10, endChunkPos.z);
			GameObject obj = (GameObject)Instantiate (Resources.Load ("Prefabs/" + Variables.BossHeadName), position, Variables.DefaultQuaternion);
			boss = (Boss)obj.GetComponent ("Boss");
		}
	}

	public void RestartChunk () {
		int random = getRandomChunk ();
		string loadString = "Spawn Chunks/" + Variables.LevelChunks [random];
		endChunkPos.y = player.transform.position.y + Variables.ChunkRestartSeperation;
		GameObject g = (GameObject)Instantiate (Resources.Load(loadString), endChunkPos, Variables.DefaultQuaternion);
		LevelChunkQueue.Enqueue((LevelChunk)g.GetComponent (typeof(LevelChunk)));
		ChunkTriggered = false;
	}
}
