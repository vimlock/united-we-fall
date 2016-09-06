using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

	public bool triggeraL = false;
	public bool triggeraR = false;
	public bool shoulderaL = false;
	public bool shoulderaR = false;
	public bool stickaL = false;
	public bool stickaR = false;

	public float shootingSpeed = 0.5f;

	public float bulletSpeed = 50f;
    AudioSource audio;
	float nextShot = 0f;

	public enum bulletType
	{
		bullet1,
		bullet2
	}
	bulletType shotType;

	Transform gun;

	

    // Should probably think of a better name for this.
    public enum PlayerId {
        LEFT,
        RIGHT
    };

    // The controller component.
    public IController controller;

    // Which one of the players this components is.
    // Should be assigned by a prefab or something else.
    public PlayerId id;

	// Use this for initialization
    void Start ()
    {
        audio = FindObjectOfType<AudioSource>();
		gun = transform.Find ("gun");
		shotType = bulletType.bullet1;
		if (id == PlayerId.RIGHT) {
			shotType = bulletType.bullet2;
		}
        if (controller == null) {
            Debug.LogError("PlayerBehaviour does not have IController component assigned");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // No controller? We're out of luck then.
        if (controller == null) {
            return;
        }

        if (id == PlayerId.LEFT) {
			if (!controller.deadzoneL) {
				gun.rotation = Quaternion.AngleAxis (controller.angleLeft, Vector3.forward);
			}
        }
        else if (id == PlayerId.RIGHT){
			if (!controller.deadzoneR) {
				gun.rotation = Quaternion.AngleAxis (controller.angleRight, Vector3.forward);
			}
        }


		//not sure how we will do this, this saves the state to boolean and in fixed update does the function
		if (controller.shoulderL) {
			shoulderaL = true;
		}

		if (controller.shoulderR) {
			shoulderaR = true;
		}

		if (controller.triggerL) {
			triggeraL = true;
		}

		if (controller.triggerR) {
			triggeraR = true;
		}

		if (controller.stickL) {
			stickaL = true;
		}

		if (controller.stickR) {
			stickaR = true;
		}


	}

	void shoot(PlayerId idv){
		if (Time.time > nextShot && id == idv) {
			GameObject tmp = Instantiate(Resources.Load(shotType.ToString())as GameObject,gun.position,gun.rotation) as GameObject;
			tmp.GetComponent<bulletScript> ().type = (bulletScript.bulletType)shotType;
			tmp.GetComponent<bulletScript> ().bulletSpeed = bulletSpeed;
            audio.Play();
			nextShot = Time.time + shootingSpeed;
		}


		}

	void FixedUpdate(){

		if (shoulderaL) {
			
			shoulderaL = false;
		}

		if (shoulderaR) {
			Debug.Log ("Painettu shoulderR");
			shoulderaR = false;
		}

		if (triggeraL) {
			shoot (PlayerId.LEFT);
			Debug.Log ("Painettu triggerL");
			triggeraL = false;
		}

		if (triggeraR) {
			shoot (PlayerId.RIGHT);
			Debug.Log ("Painettu triggerR");
			triggeraR = false;
		}

		if (stickaL) {
			Debug.Log ("Painettu StickL");
			stickaL = false;
		}

		if (stickaR) {
			Debug.Log ("Painettu StickR");
			stickaR = false;
		}


	}


}
