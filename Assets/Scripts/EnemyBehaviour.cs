using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

    // Target where the enemy moves towards
    public Vector3 moveTarget = new Vector3(0.0f, 0.0f, 0.0f);

    // Speed at which the enemy moves towards the target
    public float moveSpeed;

    // How long the enemy has been alive?
    // Could be used for some game mechanics
    public float lifetime = 0.0f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, moveTarget, Time.deltaTime * moveSpeed);
        lifetime += Time.deltaTime;

        if (lifetime > 30)
        {
            Destroy(this);
        }
    }
}
