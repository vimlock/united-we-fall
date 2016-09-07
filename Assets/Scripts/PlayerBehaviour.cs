using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    // Should probably think of a better name for this.
    public enum PlayerId {
        LEFT,
        RIGHT
    };

    [Tooltip("Interval between bullets")]
	public float shootingSpeed = 0.5f;

    [Tooltip("Time it takes for the player to reload")]
	public float reloadSpeed = 2.0f;

	public int ammo;
	public int ammoMax = 50;

	public float bulletSpeed = 50.0f;
    public AudioSource shootingSound;
    public AudioSource lastBulletSound;
    public AudioSource reloadSound;

    public Transform gun;
	public Transform shootingPoint;

    // Should be set in prefabs
    public GameObject bulletType;

    // Time it takes the reload to complete
    private float reloadTimer = 0.0f;

    // Time it takes us to be ready to fire again (in seconds)
    private float shotTimer = 0.0f;

    // Time in seconds it takes player to swap position
    private float swapTimer = 0.0f;

    // How many bullets we're shoothing currently
    // Used for sound and particle effects
    public float firerate = 0.0f;

    // The controller component.
    public IController controller;

    // Which one of the players this components is.
    // Should be assigned by a prefab or something else.
    public PlayerId id;

    public UnityEngine.UI.Text ammoText;


	// Use this for initialization
    void Start ()
    {
		
        ammo = ammoMax;
		gun = transform.Find ("gun");
        if (gun == null) {
            Debug.LogError("gun transform missing from player");
        }
	
        if (gun != null) {
            shootingPoint = gun.Find ("shootingpoint");
        }

		if (id == PlayerId.RIGHT) {
			gun.rotation = Quaternion.AngleAxis (0, Vector3.forward);

		}

        if (controller == null) {
            Debug.LogError("PlayerBehaviour does not have IController component assigned");
        }

        if (bulletType == null) {
            Debug.LogError("PlayerBehaviour does not have bulletType assigned");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (shotTimer > 0.0f) {
            shotTimer -= Time.deltaTime;
        }

        if (reloadTimer > 0.0f)  {
            reloadTimer -= Time.deltaTime;

            if (reloadTimer <= 0.0f) {
                FinishWeaponReload();
            }
        }

        if (swapTimer > 0.0f) {
            swapTimer -= Time.deltaTime;

            if (swapTimer <= 0.0f) {
                FinishSwap();
            }
        }

        if (firerate >= 0.25f) {
            firerate = firerate * 0.8f;

            if (firerate < 0.25f) {
                firerate = 0.0f;
                shootingSound.loop = false;
            }
        }

        if (ammo == 0) {
            StartWeaponReload();
        }

        // No controller? We're out of luck then.
        if (controller == null) {
            return;
        }

        // We should have implemented a better system for this...
        
        bool deadzone = false;

        bool trigger = false;
        bool stick = false;
        float angle = 0.0f;
    
        if (id == PlayerId.LEFT) {
            deadzone = controller.deadzoneL;
            trigger = controller.triggerL;
            stick = controller.stickL;
            angle = controller.angleLeft;
        }
        else if (id == PlayerId.RIGHT){
            deadzone = controller.deadzoneR;
            trigger = controller.triggerR;
            stick = controller.stickR;
            angle = controller.angleRight;
        }

        if (!deadzone) {
			gun.rotation = Quaternion.Euler (new Vector3(0,0,angle));
        }

        if (trigger) {
            Shoot();
        }

		if (stick) {
			StartWeaponReload();
		}

        // Update GUI
        if (ammoText) {
            if (reloadTimer > 0.0f) {
                ammoText.text = "Reloading";
            }
            else {
                ammoText.text = string.Format("{0}/{1}", ammo, ammoMax);
            }

        }
    }

	void Shoot()
    {
        // Can't shoot while swapping weapons
        if (swapTimer > 0.0f) {
            return;
        }

        // Can't shoot while reloading
        if (reloadTimer > 0.0f) {
            return;
        }

        // Can't shoot without ammo
        if (ammo <= 0) {
            return;
        }

        // Can't shoot too fast
        if (shotTimer > 0.0f) {
            return;
        }

        GameObject tmp = Instantiate(bulletType, shootingPoint.position, shootingPoint.rotation) as GameObject;
        tmp.GetComponent<BulletBehaviour>().speed = bulletSpeed;

        ammo--;
        firerate += 1.0f;
        shotTimer = shootingSpeed;

        if(ammo == 0)
        {
            lastBulletSound.Play();
        }
        else
        {
			shootingSound.Stop ();
            shootingSound.Play();
        }

    }

    // Called when the weapon reload is started
    // Can be called even if the reload is not complete!
    public void StartWeaponReload()
    {
        if (reloadSound != null) {
            reloadSound.Play();
        }

        // still in middle of reloading?
        if (reloadTimer > 0.0f) {
            return;
        }

        reloadTimer = reloadSpeed;
    }

    void InterruptWeaponReload()
    {
        reloadTimer = -1.0f;
    }

    // Called when the weapon reload is complete
    void FinishWeaponReload()
    {
        ammo = ammoMax;
    }

    // Called when position swap is starte
    // Can be called even if the swap is not complete!
    public void StartSwap(float time)
    {
        InterruptWeaponReload();
        swapTimer = time;
    }

    // Called when the position swap is complete
    void FinishSwap()
    {
        if (id == PlayerId.LEFT) {
            id = PlayerId.RIGHT;
        }
        else {
            id = PlayerId.LEFT;
        }
    }
}

