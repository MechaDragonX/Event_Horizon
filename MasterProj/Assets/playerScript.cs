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
        currentVector.x += accelerationFactor * (intendedVelocity - currentVector.x);
        rigid.velocity = currentVector;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(new Vector2(0,jumpStrength));
        }
    }
}
