using UnityEngine;
using System.Collections;

public class BulletBehaviour : MonoBehaviour
{
    public enum BulletType
    {
        RED,
        BLUE
    }

    public BulletType type;
    public float lifetime = 0.0f;
    public float speed;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        lifetime += Time.deltaTime;
        if (lifetime > 5.0f) {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "terrain") {
			Destroy (gameObject);
		}
	}
}
