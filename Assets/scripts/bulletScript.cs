using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	// Use this for initialization
	public float bulletSpeed = 75f;
    public enum bulletType
    {
        bullet1,
        bullet2
    }
	Rigidbody rb;
    public bulletType type;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		if (type == null) {
			type = bulletType.bullet1;
		}
    }
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.forward * bulletSpeed;
	}

}
