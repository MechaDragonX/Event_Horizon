using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public float topSpeed = 1f;
    public float accelerationFactor = 0.6f;
    public float jumpStrength = 1f;
    private Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
        rigid = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame`
	void FixedUpdate () {
        //Stop the player from spinning
        rigid.angularVelocity = 0f;
        this.transform.eulerAngles = new Vector3();

        float dT = Time.deltaTime;

        //Controls
        //The way this is designed to work is that it is fast for the player to turn around.
        Vector2 currentVector = rigid.velocity;
        float intendedVelocity = 0f;

        if(Input.GetKey(KeyCode.A))
        {
            intendedVelocity -= topSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            intendedVelocity += topSpeed;
        }

        //When pressed the player moves up. If the button is held down, like Mario the player jump a greater height.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentVector.y += (accelerationFactor * jumpStrength);
        }
        //When released the player moves down.
        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentVector.y -= (accelerationFactor * jumpStrength);
        }

        currentVector.x += accelerationFactor * (intendedVelocity - currentVector.x);
        rigid.velocity = currentVector;

   
    }
}
