using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {
	//ukko1.transform.rotation = Quaternion.AngleAxis (angleL, Vector3.forward);
	// Use this for initialization

	public int player = 1;
	public GamePadController controller;
	void Start () {
		controller = GameObject.FindGameObjectWithTag("GamePad").GetComponent<GamePadController>();


	}
		
	public bool triggerL = false;
	public bool triggerR = false;
	public bool shoulderL = false;
	public bool shoulderR = false;
	public bool stickL = false;
	public bool stickR = false;
	// Update is called once per frame
	void Update () {

		//stick control
		if (player == 1) {
			if (!controller.deadzoneL) {
				transform.rotation = Quaternion.AngleAxis (controller.angleL, Vector3.forward);
			}
		} else {
			if (!controller.deadzoneR) {
				transform.rotation = Quaternion.AngleAxis (controller.angleR, Vector3.forward);
			}
		}

		//not sure how we will do this, this saves the state to boolean and in fixed update does the function
		if (controller.shoulderL) {
			shoulderL = true;
		}

		if (controller.shoulderR) {
			shoulderR = true;
		}

		if (controller.triggerL) {
			triggerL = true;
		}

		if (controller.triggerR) {
			triggerR = true;
		}

		if (controller.stickL) {
			stickL = true;
		}

		if (controller.stickR) {
			stickR = true;
		}


	}


	void FixedUpdate(){

		if (shoulderL) {
			Debug.Log ("Painettu shoulderL");
			shoulderL = false;
		}

		if (shoulderR) {
			Debug.Log ("Painettu shoulderR");
			shoulderR = false;
		}

		if (triggerL) {
			Debug.Log ("Painettu triggerL");
			triggerL = false;
		}

		if (triggerR) {
			Debug.Log ("Painettu triggerR");
			triggerR = false;
		}

		if (stickL) {
			Debug.Log ("Painettu StickL");
			stickL = false;
		}

		if (stickR) {
			Debug.Log ("Painettu StickR");
			stickR = false;
		}


	}

}
