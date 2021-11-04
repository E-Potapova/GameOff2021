using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachsSuperUltimateMEgaPLayerScript : MonoBehaviour
{

    //intilazie stuff
    private float jumpForce;
    private float crouchSpeed;
    private float walkSpeed;
    private float movementSmoothing = .05f;

    private bool facingright = true; 
    private bool onground = true;


    //get the rigidbody
    private Rigidbody2D playerrigidbody;

    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        walkSpeed = ConfigurationUtils.PlayerSpeed;

        jumpForce = ConfigurationUtils.JumpForce;

        crouchSpeed = ConfigurationUtils.CrouchSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        
    }

    public void Move(float move, bool crouch, bool jump)
    {

        //check for crouching
        //if crouching
        if (crouch)
        {
            move = crouchSpeed;
        }

        //actually move the character along y axis
        Vector3 actualMove = new Vector2(move, playerrigidbody.velocity.y);
        //make movement smooth
        playerrigidbody.velocity = Vector3.SmoothDamp(playerrigidbody.velocity, actualMove, ref velocity, movementSmoothing);

        if (move > 0 && !facingright)
        {
            Flip();
        }

        else if (move < 0 && facingright)
        {
            Flip();
        }
    }

    //function to flip character sprite
    public void Flip()
    {
        facingright = !facingright;

        //flip hit box
        Vector3 hitboxScale = transform.localScale;
        hitboxScale.x = (hitboxScale.x * 1);
        transform.localScale = hitboxScale;
    }

}
