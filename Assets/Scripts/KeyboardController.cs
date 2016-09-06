using UnityEngine;
using System.Collections;

// Dummy class which emulates the GamePadController
// Can be used to test the controls without having a xbox controller
// Downside is that its very clumsy to use
public class KeyboardController : IController
{
    // Degrees per second
    public float turnSpeed = 90.0f;

	void Start ()
    {
        // Ain't nobody here but us chickens
	}
	
	void Update ()
    {
        float dr = 0.0f;
        float dl = 0.0f;

        // Left thumb stick
        if (Input.GetKey("a")) {
            dl += turnSpeed;
        }

        if (Input.GetKey("s")) {
            dl -= turnSpeed;
        }

        stickL = Input.GetKey("z");

        // Left back buttons
        triggerL = Input.GetKey("x");
        shoulderL = Input.GetKey("c");

        // Right thumb stick
	    if (Input.GetKey("j")) {
            dr += turnSpeed;
        }

        if (Input.GetKey("k")) {
            dr -= turnSpeed;
        }

        stickR = Input.GetKey("m");

        // Right back buttons
        triggerR = Input.GetKey("n");
        shoulderR = Input.GetKey("b");

        dl *= Time.deltaTime;
        dr *= Time.deltaTime;

        // Apply the rotation delta
        angleLeft += dl;
        angleRight += dr;

        // Keep the angles in 0-360 degree range
        angleLeft %= 360.0f;
        angleRight %= 360.0f;

        deadzoneL = false;
        deadzoneR = false;
	}
}
