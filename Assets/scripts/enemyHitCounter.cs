﻿using UnityEngine;
using System.Collections;

public class enemyHitCounter : MonoBehaviour {

    public int KillCounter;
    public enum bulletType
    {
        bullet1,
        bullet2
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

    void OnCollisionEnter(Collision col)
    {
        if(type == (bulletType)col.gameObject.GetComponent<bulletScript>().type) // If enemy's "weakness" bullet type is same as player's who shot it
        {
            DestroyObject(type);
            Destroy(col.gameObject);
        }

        if (type != (bulletType)col.gameObject.GetComponent<bulletScript>().type)
        {
            Destroy(col.gameObject);
        }
    }
}
