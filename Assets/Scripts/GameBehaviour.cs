using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour {

    // The controller component.
    public IController controller;

    // Prefab referense to the players
    public GameObject players;

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
            }
        }
        else {
            return;
        }

        float t = swapTimer / swapSpeed;
        angle = (1.0f - t) * startAngle + t * targetAngle;

        Debug.LogFormat("angle {0}", angle);

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        players.transform.rotation = q;
    }

    void SwapPlayers()
    {
        // Currently in the middle of swapping
        if (swapTimer > 0.0f) {
            return;
        }

        Debug.Log("swapping");

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
}
