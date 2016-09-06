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

	public int ammo;
	public int ammoMax = 50;
	public float bulletSpeed = 50f;
	public float reloadTime = 2f;
	float reloadact = 0;
	bool reloading = false;
    AudioSource ShootingSound;
    AudioSource LastBulletSound;
	float nextShot = 0f;

	public enum bulletType
	{
		bullet1,
		bullet2
	}
	bulletType shotType;

	Transform gun;
	Transform shootingPoint;

	

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
        ShootingSound = GameObject.Find("GunLeft").GetComponent<AudioSource>();
        LastBulletSound = GameObject.Find("LastBulletLeft").GetComponent<AudioSource>();
        ammo = ammoMax;
		gun = transform.Find ("gun");
		shootingPoint = transform.Find ("gun").Find ("shootingpoint");
		shotType = bulletType.bullet1;
		if (id == PlayerId.RIGHT) {
            ShootingSound = GameObject.Find("GunRight").GetComponent<AudioSource>();
            LastBulletSound = GameObject.Find("LastBulletRight").GetComponent<AudioSource>();
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
		if (Time.time > nextShot && id == idv && ammo >0) {
			
			GameObject tmp = Instantiate(Resources.Load(shotType.ToString())as GameObject,shootingPoint.position,shootingPoint.rotation) as GameObject;
			tmp.GetComponent<bulletScript> ().type = (bulletScript.bulletType)shotType;
			tmp.GetComponent<bulletScript> ().bulletSpeed = bulletSpeed;
            ShootingSound.Stop();
            if(ammo == 1)
            {
                LastBulletSound.Play();
            }
            else
            {
                ShootingSound.Play();
            }
            nextShot = Time.time + shootingSpeed;
			ammo--;
			Debug.Log ("Ammo: " + ammo);
		}
		if (ammo <= 0) {
			reload (idv);
		}
		}
	void reload(PlayerId idv){
		if (idv == id) {
			if (!reloading) {
				reloadact = Time.time + reloadTime;
			}
			reloading = true;
		}
	}

	void FixedUpdate(){

		if (shoulderaL) {
			swapGun ();
			Debug.Log ("swapped guns!");
			shoulderaL = false;
		}

		if (shoulderaR) {
			swapGun ();
			Debug.Log ("swapped guns!");
			shoulderaR = false;
		}

		if (triggeraL) {
			shoot (PlayerId.LEFT);

			triggeraL = false;
		}

		if (triggeraR) {
			shoot (PlayerId.RIGHT);

			triggeraR = false;
		}

		if (stickaL) {
			Debug.Log ("Reloading Left");
			reload (PlayerId.LEFT);
			stickaL = false;
		}

		if (stickaR) {
			Debug.Log ("Reloading Right");
			reload (PlayerId.RIGHT);
			stickaR = false;
		}

		if (reloading) {
			if (Time.time > reloadact) {
				ammo = ammoMax;
				reloading = false;
			}

		}


	}

	void swapGun(){
		if (shotType == bulletType.bullet1) {
			shotType = bulletType.bullet2;
		} else {
			shotType = bulletType.bullet1;
		}
	}




}
