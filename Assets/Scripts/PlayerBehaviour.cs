using UnityEngine;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour {

    // Should probably think of a better name for this.
    public enum PlayerId {
        LEFT,
        RIGHT
    };

    // The controller component.
    public IController controller;

    // Which one of the players this components is.
    // Should be assigned by a prefab or something else.
    public PlayerId id;

	// Use this for initialization
    void Start ()
    {
        if (controller == null) {
            Debug.LogError("PlayerBehaviour does not have IController component assigned");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        // No controller? We're out of luck then.
        if (controller == null) {
            return;
        }

        if (id == PlayerId.LEFT) {
            transform.rotation = Quaternion.AngleAxis (controller.angleLeft, Vector3.forward);
        }
        else if (id == PlayerId.RIGHT){
            transform.rotation = Quaternion.AngleAxis (controller.angleRight, Vector3.forward);
        }
    }
}
