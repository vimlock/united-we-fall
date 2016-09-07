using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void StartGame()
    {
        Debug.Log("Start GAME");
        // Application.LoadLevel(1); This is for now option if we want something else.
    }
}
