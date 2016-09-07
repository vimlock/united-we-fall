using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIBehaviour : MonoBehaviour {

    public float timer;
    public float IntroTimer;

    public Image ControlsImage;
    public IController controller;
    public AudioSource BackgroundStart;
    public AudioSource BackgroundLoop;

	// Use this for initialization
	void Start () {
        controller.GetComponent<GamePadController>().enableControls = false;
        BackgroundStart.Play();
        BackgroundLoop.PlayDelayed(IntroTimer);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Time.timeSinceLevelLoad);
        if (Time.timeSinceLevelLoad >= timer)
        {
            controller.GetComponent<GamePadController>().enableControls = true;
            ControlsImage.enabled = false;           
        }

    }
}
