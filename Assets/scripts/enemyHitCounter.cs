using UnityEngine;
using System.Collections;

public class enemyHitCounter : MonoBehaviour {

    public int KillCounter;
    public enum bulletType
    {
        bullet1,
        bullet2,
		bullet3
    }

    public bulletType type;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyObject(bulletType type)
    {
        switch(type)
        {
            case bulletType.bullet1:
                    Destroy(gameObject);
                    KillCounter++;
                break;
                
            case bulletType.bullet2:
                    Destroy(gameObject);
                    KillCounter++;
                break;
        }
            
    }

    void OnCollisionEnter2D(Collision2D col)
    {

		if (col.gameObject.tag == "Bullet") {
			if (type == bulletType.bullet3 && col.gameObject.tag == "Bullet") {
			
				Destroy (col.gameObject);
				Destroy (gameObject);
			}
			if (type == (bulletType)col.gameObject.GetComponent<bulletScript> ().type) { // If enemy's "weakness" bullet type is same as player's who shot it
				DestroyObject (type);
				Destroy (col.gameObject);
			}
			if (type != (bulletType)col.gameObject.GetComponent<bulletScript>().type)
			{
				Destroy(col.gameObject);
			}

		}
		if (col.gameObject.tag == "DangerZone") {
			Destroy (gameObject);
		}
     

	

    }

	public void setType(bulletType tyyppi){
		type = tyyppi;
	}
}
