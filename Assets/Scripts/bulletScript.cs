using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	// Use this for initialization
	public float bulletSpeed = 75f;
    public enum bulletType
    {
        RED,
        BLUE
    }
	Rigidbody2D rb;
    public bulletType type;

	void Start () {
		Destroy (gameObject, 10f);
		rb = GetComponent<Rigidbody2D> ();
		if (type == null) {
			type = bulletType.RED;
		}
    }
	
	// Update is called once per frame
	void Update () {
		rb.velocity = transform.right * bulletSpeed;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		
		if (col.gameObject.tag == "terrain") {
			Destroy (gameObject);
		}
	}

}
