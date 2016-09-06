using UnityEngine;
using System.Collections;

// Interface between different types of controllers, keyboard, gamepad, toaster, etc.
public abstract class IController : MonoBehaviour
{
    // Angle of the left control button.
    // Should be in degrees, clamped between 0 and 360.
	public float angleLeft = 0f;

    // Angle of the right control button.
    // Should be in degrees, clamped between 0 and 360.
	public float angleRight = 0f;
}
