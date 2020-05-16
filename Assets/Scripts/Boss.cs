using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Boss : MonoBehaviour {

	private GameObject[] bodies;
	private List<Vector3> bodyMovement;
	private float prevPosX;
	private float strafeSpeed;
	private AudioClip bossRoar;
	private int length;

	// Use this for initialization
	void Start () {
		strafeSpeed = Variables.BossStrafeSpeed;
		length = Random.Range (Variables.BossMinLength, Variables.BossMaxLength);
		bodies = new GameObject[length];
		bodyMovement = new List<Vector3> ();

		initialise (length);

		playAudio ();
	}
	
	// Update is called once per frame
	//void Update () {
	//	move ();
	//	updateMovement ();
	//}

	void FixedUpdate () {
		move ();
		updateMovement ();
	}

	private void initialise (int length) {
		GameObject prevObj = null;
		string path = "Prefabs/" + Variables.BossBodyName;
		Vector3 pos = transform.position;
		prevPosX = transform.position.x;
		for (int i = 0; i < length; i++) {
			GameObject obj = null;

			if (i == 0) {
				pos = new Vector3(pos.x, pos.y + 0.4f, 0.1f);
				obj = (GameObject)Instantiate (Resources.Load(path), pos, transform.rotation);
			}else if (i == length-1) {
				pos = new Vector3(pos.x, pos.y + 0.5f, 0.1f);
				obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.BossTailName), pos, transform.rotation);
			}else {
				pos = new Vector3(pos.x, pos.y + 0.33f, 0.1f);
				obj = (GameObject)Instantiate (Resources.Load(path), pos, transform.rotation);
			}
			bodies[i] = obj;
		}
		/*for (int i = 0; i < length; i++) {
			GameObject obj = null;
			if (i == 0) {
				obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(transform.position.x, transform.position.y, 0.1f), transform.rotation);
				HingeJoint2D joint = gameObject.GetComponent<HingeJoint2D>(); 
				joint.connectedBody = obj.GetComponent<Rigidbody2D>();
			}else if (i == length-1) {
				obj = (GameObject)Instantiate (Resources.Load("Prefabs/" + Variables.BossTailName), new Vector3(transform.position.x, transform.position.y, 0.1f), transform.rotation);
				HingeJoint2D joint = prevObj.GetComponent<HingeJoint2D>(); 
				joint.anchor = new Vector2(0, 0.5f);
				joint.connectedBody = obj.GetComponent<Rigidbody2D>();

				joint = obj.GetComponent<HingeJoint2D>(); 
				joint.connectedBody = prevObj.GetComponent<Rigidbody2D>();
			}else {
				obj = (GameObject)Instantiate (Resources.Load(path), new Vector3(transform.position.x, transform.position.y, 0.1f), transform.rotation);
				HingeJoint2D joint = prevObj.GetComponent<HingeJoint2D>(); 
				joint.connectedBody = obj.GetComponent<Rigidbody2D>();
			}

			bodies.Add (obj);
			prevObj = obj;
		}*/
	}

	private void move () {
		if (Time.deltaTime > 0) {
			Transform temp = transform;
			temp.transform.Translate (strafeSpeed,  Variables.BossRunSpeed, 0);
			float x = Mathf.Clamp (temp.position.x, -Variables.BossBorderX, Variables.BossBorderX);
			transform.position = new Vector3(x, temp.position.y, temp.position.z);
			bodyMovement.Add (transform.position);

			int chance = Random.Range (0, 250);
			if (x >= Variables.BossBorderX || x <= -Variables.BossBorderX || chance <= Variables.BossDirectionChance * 100)
				strafeSpeed = -strafeSpeed;
		}
	}

	private void updateMovement () {
		Vector3[] tempMovement = bodyMovement.ToArray();
		Vector3 pos = transform.position;


		for (int i = 0; i < length; i++) {
			if (tempMovement.Length - 12 - (i * 12) >= 0)
				pos = tempMovement[tempMovement.Length-12-(i*12)];
			else
				pos = new Vector3(0, pos.y + 0.33f, 0.1f);

			bodies[i].transform.position = pos;
		}
	}

	private void playAudio () {
		bossRoar = (AudioClip)Resources.Load("Audio/" + Variables.BossRoar, typeof(AudioClip));
		GetComponent<AudioSource>().clip = bossRoar;
		GetComponent<AudioSource>().Play();
	}

	public void Destroy() {
		foreach (GameObject obj in bodies)
			Destroy (obj);

		Destroy (gameObject);
	}

	public void ResetCamera () {
		foreach (GameObject obj in bodies) {
			if(obj != null) {
				obj.GetComponent<CameraReset>().ResetCamera();
			}
		}

		transform.position = new Vector3 (transform.position.x, transform.position.y - Variables.MapRestartPoint, transform.position.z);
	}
}
