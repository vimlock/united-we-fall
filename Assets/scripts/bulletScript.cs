using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	// Use this for initialization
    
    public enum bulletType
    {
        bullet1,
        bullet2
    }

    public bulletType type;

	void Start () {
        if (gameObject.tag == "Bullet1") // Using these if statements until we know which player shoots bullet
            type = bulletType.bullet1;

        if (gameObject.tag == "Bullet2")
            type = bulletType.bullet2;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

}
