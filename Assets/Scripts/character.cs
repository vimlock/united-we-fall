using UnityEngine;
using System.Collections;

public class character : MonoBehaviour {
	//ukko1.transform.rotation = Quaternion.AngleAxis (angleL, Vector3.forward);
	// Use this for initialization

	public int player = 1;
	public GamePadController controller;
	void Start () {

        // !!!!!!!!!!!!!
        // This would probably be better implemented using a prefab, check Scripts/PlayerBehaviour.cs
		controller = GameObject.FindGameObjectWithTag("GamePad").GetComponent<GamePadController>();


	}
	
	// Update is called once per frame
	void Update () {
		if (player == 1) {
			if (!controller.deadzoneL) {
				transform.rotation = Quaternion.AngleAxis (controller.angleLeft, Vector3.forward);
			}
		} else {
			if (controller.deadzoneR) {
				transform.rotation = Quaternion.AngleAxis (controller.angleRight, Vector3.forward);
			}
		}


	}
}
