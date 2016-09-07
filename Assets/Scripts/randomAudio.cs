using UnityEngine;
using System.Collections;

public class randomAudio : MonoBehaviour {

	AudioSource[] audios;
	// Use this for initialization
	void Start () {

		audios = GetComponentsInChildren<AudioSource> ();
	
	}
	float timer = 0;
	public float delay = 15;
	float randomDelay = 0;
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= randomDelay) {
			randomDelay = delay+ Random.Range (-3, 3);
			timer = 0;
			int randomi = Random.Range (0, audios.Length);
			audios [randomi].Play ();
			audios [randomi].loop = false;
		}
			
	}
}
