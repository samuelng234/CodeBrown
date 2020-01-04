using UnityEngine;
using System.Collections;

public class BackgroundTrigger : MonoBehaviour {

	private Player player;

	void Start () {
		player = (Player)GameObject.FindGameObjectWithTag ("Player").GetComponent ("Player");
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && !player.BackgroundTriggered) {
			BackgroundController controller = (BackgroundController)GameObject.FindGameObjectWithTag ("BackgroundControl").GetComponent("BackgroundController");
			controller.BackgroundTrigger = true;
			player.BackgroundTriggered = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (player.BackgroundTriggered)
			player.BackgroundTriggered = false;
	}

}
