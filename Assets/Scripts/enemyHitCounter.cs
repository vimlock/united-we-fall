using UnityEngine;
using System.Collections;

public class enemyHitCounter : MonoBehaviour {

    public int KillCounter;
    public enum bulletType
    {
        RED,
        BLUE,
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

    void OnDestroy()
    {
        GameObject gameController = GameObject.Find("GameController");
        if (gameController) {
            gameController.GetComponent<GameBehaviour>().IncrementKillCount();
        }
    }

    void DestroyObject(bulletType type)
    {
        switch(type)
        {
            case bulletType.RED:
                    Destroy(gameObject);
                    KillCounter++;
                break;
                
            case bulletType.BLUE:
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
				return;
			}
			if (type == (bulletType)col.gameObject.GetComponent<BulletBehaviour> ().type) { // If enemy's "weakness" bullet type is same as player's who shot it
				DestroyObject (type);
				Destroy (col.gameObject);
				return;
			}
			if (type != (bulletType)col.gameObject.GetComponent<bulletScript>().type)
			{
				Destroy(col.gameObject);
				return;
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
