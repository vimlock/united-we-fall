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

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            Debug.Log("A painettu");
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            
        }


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

	


	



        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

 
    }


}