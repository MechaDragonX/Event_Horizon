using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class XInput : MonoBehaviour {
    public float topSpeed = 1f;
    public float accelerationFactor = 1f;
    public float jumpStrength = 1f;
    private Rigidbody2D rigid;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    // Use this for initialization
    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // SetVibration should be sent in a slower rate.
        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

        //Stop the player from spinning
        rigid.angularVelocity = 0f;
        this.transform.eulerAngles = new Vector3();

        float dT = Time.deltaTime;

        //Controls
        //The way this is designed to work is that it is fast for the player to turn around.
        Vector2 currentVector = rigid.velocity;
        float intendedVelocity = 0f;

        //When DPad is pressed on the left, the character will move to the Left
        //if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed)
        if (state.DPad.Left == ButtonState.Pressed)
        {
            intendedVelocity -= topSpeed;
        }
        //if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed)
        if (state.DPad.Right == ButtonState.Pressed)
        {
            intendedVelocity += topSpeed;
        }

        //When pressed the player moves up. If the button is held down, like Mario the player jump a greater height.
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            currentVector.y += (accelerationFactor * jumpStrength);
        }
        //When released the player moves down.
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            currentVector.y -= (accelerationFactor * jumpStrength);
        }

        currentVector.x += accelerationFactor * (intendedVelocity - currentVector.x);
        rigid.velocity = currentVector;
    }

    // Update is called once per frame
    void Update()
        {
            // Find a PlayerIndex, for a single player game
            // Will find the first controller that is connected ans use it
            if (!playerIndexSet || !prevState.IsConnected)
            {
                for (int i = 0; i < 2; ++i)
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
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (state.Buttons.A == ButtonState.Pressed)
        {
            rigid.AddForce(new Vector2(0, jumpStrength));
        }
    }
}