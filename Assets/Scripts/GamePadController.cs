using UnityEngine;
using XInputDotNetPure; // Required in C#

public class GamePadController : MonoBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

	public GameObject ukko1;
	public GameObject ukko2;

	public float angleL = 0f;
	public float angleR = 0f;
	public bool deadzoneR = true;
	public bool deadzoneL = true;
	public bool triggerL = false;
	public bool triggerR = false;
	public bool shoulderL = false;
	public bool shoulderR = false;
	public bool stickL = false;
	public bool stickR = false;

    // Use this for initialization
    void Start()
    {
        // No need to initialize anything for the plugin
    }

    // Update is called once per frame
    void Update()
    {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }



        prevState = state;
        state = GamePad.GetState(playerIndex);


		//Controls to boolean variables

		shoulderL = state.Buttons.LeftShoulder.Equals(ButtonState.Pressed);
		shoulderR = state.Buttons.RightShoulder.Equals(ButtonState.Pressed);
		if (state.Triggers.Left > 0) {
			triggerL = true;
		}else {
			triggerL = false;
		}

		if (state.Triggers.Right > 0) {
			triggerR = true;
		}else {
			triggerR = false;
		}
			
		//
		stickL = state.Buttons.LeftStick.Equals (ButtonState.Pressed);
		stickR = state.Buttons.RightStick.Equals (ButtonState.Pressed);

		if (state.ThumbSticks.Left.X < 0) {
			deadzoneL = false;
		} else {
			deadzoneL = true;
		}

		if (state.ThumbSticks.Right.X > 0) {
			deadzoneR = false;
		} else {
			deadzoneR = true;
		}
				
				angleL = Mathf.Atan2 (-state.ThumbSticks.Left.Y, -state.ThumbSticks.Left.X) * Mathf.Rad2Deg;
				
				angleR = Mathf.Atan2 (-state.ThumbSticks.Right.Y, -state.ThumbSticks.Right.X) * Mathf.Rad2Deg;

	


	
		//Debug.Log ("TriggerR: " + triggerR + " TriggerL: " + triggerL + " ShoulderL: " + shoulderL + " ShoulderR: " + shoulderR + " AngleL: " + angleL + " AngleR: " + angleR);


        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

 
    }


}