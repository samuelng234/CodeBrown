using UnityEngine;
using System.Collections;

public class BossTrigger : MonoBehaviour {

	private bool triggered = false;
	private Player player;
	private
	
	void Start () {
		player = (Player)GameObject.FindGameObjectWithTag("Player").GetComponent("Player");;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && !triggered) {
			LevelChunkFactory factory = (LevelChunkFactory)GameObject.FindGameObjectWithTag ("ChunkFactory").GetComponent("LevelChunkFactory");
			Boss boss = (Boss)GameObject.FindGameObjectWithTag ("BossHead").GetComponent("Boss");
			factory.RestartChunk ();
			boss.Destroy ();
			triggered = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (triggered)
			triggered = false;
	}
}
