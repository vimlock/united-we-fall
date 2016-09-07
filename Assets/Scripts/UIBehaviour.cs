using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBehaviour : MonoBehaviour {

    public float timer;

    public Image ControlsImage;
    public IController controller;

	// Use this for initialization
	void Start () {
        controller.GetComponent<GamePadController>().enableControls = false;
    }
	
	// Update is called once per frame
	void Update () {


        if (Time.timeSinceLevelLoad >= timer)
        {
            controller.GetComponent<GamePadController>().enableControls = true;
            ControlsImage.enabled = false;           
        }


    }
}
