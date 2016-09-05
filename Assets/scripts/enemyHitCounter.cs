using UnityEngine;
using System.Collections;

public class enemyHitCounter : MonoBehaviour {

    public int osumaluku = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        osumaluku++;
        kuoleeko();
    }

    void kuoleeko()
    {
        if (osumaluku > 1)
        {
            Destroy(gameObject);
        }
    }
}
