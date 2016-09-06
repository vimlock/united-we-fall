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
	Rigidbody2D rb;
    public bulletType type;

	void Start () {
		Destroy (gameObject, 20f);
		rb = GetComponent<Rigidbody2D> ();
		if (type == null) {
			type = bulletType.bullet1;
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
