using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {

    // The controller component.
    public IController controller;

    // Prefab referense to the players
    public GameObject players;

    public GameObject guiWeaponLeft;
    public GameObject guiWeaponRight;

    [Tooltip("Time it takes for the players to swap positions")]
    public float swapSpeed = 1.0f;

    // Time in seconds it takes players to swap positions
    private float swapTimer = 0.0f;

    // Last state of the swap button
    private bool swapButtonState = false;

    private float angle = 0.0f;
    private float startAngle = 0.0f;
    private float targetAngle = 180.0f;

    // Use this for initialization
    void Start () {
        if (guiWeaponLeft == null) {
            guiWeaponLeft = GameObject.Find("Canvas/WeaponLeft");
        }

        if (guiWeaponRight == null) {
            guiWeaponRight = GameObject.Find("Canvas/WeaponRight");
        }

        if (guiWeaponLeft == null) {
            Debug.LogError("weaponLeft not assigned in GameBehaviour");
        }

        if (guiWeaponRight == null) {
            Debug.LogError("weaponRight not assigned in GameBehaviour");
        }

        if (players == null) {
            Debug.LogError("players not assigned in GameBehaviour");
        }
    }
	
	// Update is called once per frame
	void Update () {
        bool shoulder = controller.shoulderL || controller.shoulderR;
        if (shoulder && !swapButtonState) {
            SwapPlayers();
        }

        swapButtonState = shoulder;

        if (swapTimer > 0.0f) {
            swapTimer -= Time.deltaTime;
            if (swapTimer <= 0.0f) {
                angle = targetAngle;
                swapTimer = 0.0f;
                FinishPlayerSwap();
            }
        }
        else {
            return;
        }

        float t = swapTimer / swapSpeed;
        angle = (1.0f - t) * startAngle + t * targetAngle;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        players.transform.rotation = q;
    }

    void SwapPlayers()
    {
        // Currently in the middle of swapping
        if (swapTimer > 0.0f) {
            return;
        }

        swapTimer = swapSpeed;
        players.transform.Find("PlayerLeft").GetComponent<PlayerBehaviour>().StartSwap(swapSpeed);
        players.transform.Find("PlayerRight").GetComponent<PlayerBehaviour>().StartSwap(swapSpeed);

        if (targetAngle > 90.0f) {
            targetAngle = 0.0f;
            startAngle = 180.0f;
        }
        else {
            targetAngle = 180.0f;
            startAngle = 0.0f;
        }
    }
    
    void FinishPlayerSwap()
    {
        // Swap the weapons on gui
        GameObject weaponLeft;
        GameObject ammoLeft;

        GameObject weaponRight;
        GameObject ammoRight;

        weaponLeft = guiWeaponLeft.transform.FindChild("Weapon").gameObject;
        ammoLeft = guiWeaponLeft.transform.FindChild("Ammo").gameObject;

        weaponRight = guiWeaponRight.transform.FindChild("Weapon").gameObject;
        ammoRight = guiWeaponRight.transform.FindChild("Ammo").gameObject;

        weaponLeft.transform.SetParent(guiWeaponRight.transform, false);
        ammoLeft.transform.SetParent(guiWeaponRight.transform, false);

        weaponRight.transform.SetParent(guiWeaponLeft.transform, false);
        ammoRight.transform.SetParent(guiWeaponLeft.transform, false);

        // fucking hell, why is there no operator overloads in unity
        weaponRight.transform.localScale = Vector3.Scale(weaponRight.transform.localScale, new Vector3(-1.0f, 1.0f, 1.0f));
        weaponLeft.transform.localScale = Vector3.Scale(weaponLeft.transform.localScale, new Vector3(-1.0f, 1.0f, 1.0f));
    }
}
