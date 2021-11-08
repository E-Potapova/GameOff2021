using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZachsSuperUltimateMEgaPLayerScript : MonoBehaviour
{

    //intilazie stuff
    private float jumpForce;
    private float crouchSpeed;
    private float walkSpeed;
    private float dashSpeed;
    private float movementSmoothing = .05f;
    float horizontalMove = 0;

    Vector3 actualMove;
    private bool facingRight = true; 

    //check for key events
    private bool crouch = false;
    private bool dash = false;

    //Gravity falling
    private float fallMulitplier = 2.5f; 
    //private float lowJump

    //Jump variables
    private bool jump = false;
    private bool doubleJump = false;
    private bool jumpReset = false;

    //dash variables
    private bool dashReset = false;

    //crouch variables
    private Vector2 standingColliderSize = new Vector2 (2.9f, 1.76f);
    private Vector2 crouchingcolliderSize = new Vector2(1, .5f);

    //variable jump height
    private float jumpTimeCounter;
    private float jumpTime = 0.55f;
    private bool isJumping = false; 

    //variables to check if are player is on the ground
    private bool onGround;
    public Transform toes;
    private float checkRadius;
    public LayerMask thisisGround;

    //get the rigidbody
    private Rigidbody2D playerrigidbody;

    //get the collider
    private BoxCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        playerrigidbody = GetComponent<Rigidbody2D>();

        playerCollider = GetComponent<BoxCollider2D>();

        walkSpeed = ConfigurationUtils.PlayerSpeed;

        jumpForce = ConfigurationUtils.JumpForce;

        crouchSpeed = ConfigurationUtils.CrouchSpeed;

        dashSpeed = ConfigurationUtils.DashSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

        onGround = Physics2D.OverlapCircle(toes.position, checkRadius, thisisGround); 


        //check for key press
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            doubleJump = true;
        }
/*
        if (Input.GetButtonUp("Jump"))
        {
            jump = false;
            //doubleJump = false;
        }*/

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            
        }

        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        if(playerrigidbody.velocity.y < 0 && !dash)
        {
            playerrigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMulitplier - 1) * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
        jump = false;
        doubleJump = false;
        dash = false;
    }

    public void Move(float move, bool crouch, bool jump, bool dash)
    {

        //check for crouching
        //if crouching
        if (crouch)
        {
            playerCollider.size = crouchingcolliderSize;
            move = move * (crouchSpeed / 100);
        }
        else
        {
            playerCollider.size = standingColliderSize;
        }

        if (dash && dashReset)
        {

            if (facingRight) {
                move = dashSpeed;
            }

            if (!facingRight)
            {
                move = dashSpeed * -1;
            }
            dashReset = false;
            onGround = false;
            actualMove = new Vector2(move, 0);
        }
        else {

        //actually move the character along y axis
        actualMove = new Vector2(move, playerrigidbody.velocity.y); }
        //make movement smooth
        playerrigidbody.velocity = Vector3.SmoothDamp(playerrigidbody.velocity, actualMove, ref velocity, movementSmoothing);

        if (move > 0 && !facingRight)
        {
            Flip();
        }
        else if (move < 0 && facingRight)
        {
            Flip();
        }

        if (onGround)
        {
            jumpReset = true;
            dashReset = true; 
        }

        if (onGround && jump)
        {

            playerrigidbody.velocity = new Vector2 (playerrigidbody.velocity.x, jumpForce);
            //Jump(jump);
        }
        else if (!onGround && doubleJump && jumpReset)
        {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpForce);
            //Jump(jump);
            jumpReset = false;
        }

    }

 /*   public void Jump(bool jump)
    {
        jumpTimeCounter = jumpTime;
        if (jumpTimeCounter > 0 && jump)
        {
            playerrigidbody.velocity = (Vector2.up * jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }

    }*/

    //function to flip character sprite
    public void Flip()
    {
        facingRight = !facingRight;

        //flip hit box
        Vector3 hitboxScale = transform.localScale;
        hitboxScale.x = (hitboxScale.x * -1);
        transform.localScale = hitboxScale;
    }

}
