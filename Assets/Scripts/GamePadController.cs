using UnityEngine;
using XInputDotNetPure; // Required in C#

public class GamePadController : IController
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;


//	public bool deadzoneR = true;
//	public bool deadzoneL = true;


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

		shoulderL = state.Buttons.LeftShoulder.Equals(ButtonState.Pressed) && prevState.Buttons.LeftShoulder.Equals(ButtonState.Released);
		shoulderR = state.Buttons.RightShoulder.Equals(ButtonState.Pressed)&& prevState.Buttons.RightShoulder.Equals(ButtonState.Released);
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
				
				angleLeft = Mathf.Atan2 (-state.ThumbSticks.Left.Y, -state.ThumbSticks.Left.X) * Mathf.Rad2Deg;
				
				angleRight = Mathf.Atan2 (-state.ThumbSticks.Right.Y, -state.ThumbSticks.Right.X) * Mathf.Rad2Deg;
				
	


	
		//Debug.Log ("TriggerR: " + triggerR + " TriggerL: " + triggerL + " ShoulderL: " + shoulderL + " ShoulderR: " + shoulderR + " AngleL: " + angleL + " AngleR: " + angleR);


        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);


 
    }


}
