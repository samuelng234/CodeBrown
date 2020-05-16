using UnityEngine;
using System.Collections;

public class LiquidTrigger : MonoBehaviour {

	private Player player;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && !player.LiquidTriggered) {
			BackgroundController controller = GameObject.FindGameObjectWithTag ("BackgroundControl").GetComponent<BackgroundController>();
			controller.LiquidTrigger = true;
			player.LiquidTriggered = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D col) {
		if (player.LiquidTriggered) {
			player.LiquidTriggered = false;
		}
	}

}
