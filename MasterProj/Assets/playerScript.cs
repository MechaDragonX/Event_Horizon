using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

    public float speed = 500f;
    public float jumpStrength = 1000f;
    public bool canJump = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame`
	void Update () {
        Vector2 dir = new Vector2();
        if(Input.GetKeyDown(KeyCode.A))
        {
            dir.x -= speed;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir.x += speed;
        }

        if (canJump)
        {
            dir.y += jumpStrength;
            canJump = false;
        }

        this.GetComponent<Rigidbody2D>().AddForce(dir);
    }
}
