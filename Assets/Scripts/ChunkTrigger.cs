using UnityEngine;
using System.Collections;

public class ChunkTrigger : MonoBehaviour {

	//private bool triggered = false;
	private Player player;

	void Start () {
		player = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && !player.chunkTriggerCollision) {
			LevelChunkFactory factory = (LevelChunkFactory)GameObject.FindGameObjectWithTag ("ChunkFactory").GetComponent("LevelChunkFactory");
			factory.ChunkTriggered = true;
			player.chunkTriggerCollision = true;
			//triggered = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (player.chunkTriggerCollision)
			player.chunkTriggerCollision = false;
	}

}
