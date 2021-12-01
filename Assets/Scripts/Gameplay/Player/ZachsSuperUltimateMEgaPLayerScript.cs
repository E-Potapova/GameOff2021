using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZachsSuperUltimateMEgaPLayerScript : MonoBehaviour
{
    #region Instance Variables 

    #region Movement Vars
    //initialize stuff
    private float jumpForce;
    private float crouchSpeed;
    private float walkSpeed;
    private float dashSpeed;
    private float movementSmoothing = .05f;
    float horizontalMove = 0;
    private float direction = 1;

    Vector3 actualMove;
    private bool facingRight = true;

    //check for key events
    private bool crouch = false;
    private bool dash = false;

    //Gravity falling
    private float fallMultiplier;

    //Jump variables
    private bool jump = false;
    private bool doubleJump = false;
    private bool jumpReset = false;

    //dash variables
    private bool dashReset = false;
    private float dashTimer = 0;

    //crouch variables
    private Vector2 standingColliderSize = new Vector2(1f, 2f);
    private Vector2 crouchingcolliderSize = new Vector2(1f, 1.25f);

    //variables to check if are player is on the ground
    private bool onGround;
    public Transform toes;
    private float checkRadius = 0.01f;
    public LayerMask thisisGround;

    //variables to check for ceilling
    public Transform head;
    #endregion

    #region Wall Vars
    //variables for wall sliding
    private bool onWall = false;
    public Transform wallhands;
    private bool wallSlide = false;
    private float wallSlideSpeed;

    //variables for wall jumping
    private bool wallJump = false;
    private float wallJumpX;
    private float wallJumpY;
    #endregion

    //get the rigidbody
    private Rigidbody2D playerrigidbody;

    //get the collider
    private BoxCollider2D playerCollider;

    // for player smoothing
    private Vector3 velocity = Vector3.zero;

    //Player abilites toggle
    private bool unlockedWallJump = false;
    private bool unlockedDash = false;
    private bool unlockedDoubleJump = false;
    UnlockAbility unlockAbilityEvent;

    // get animator
    private Animator animator;

    // particle fx support
    public GameObject dirtEffect;
    public GameObject dirtOnLandingEffect;
    private float spawnDirtElapsed;
    private bool spawnedDirtOnLanding = true;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        playerrigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();

        walkSpeed = ConfigurationUtils.PlayerSpeed;
        jumpForce = ConfigurationUtils.JumpForce;
        crouchSpeed = ConfigurationUtils.CrouchSpeed;
        dashSpeed = ConfigurationUtils.DashSpeed;
        fallMultiplier = ConfigurationUtils.FallMultiplier;
        wallSlideSpeed = ConfigurationUtils.WallSlideSpeed;
        wallJumpX = ConfigurationUtils.WallJumpX;
        wallJumpY = ConfigurationUtils.WallJumpY;

        // event support
        unlockAbilityEvent = new UnlockAbility();
        EventManager.AddUnlockAbilityInvoker(this);

        // animation support
        animator = GetComponent<Animator>();
    }

    #region Movement
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;

        onGround = Physics2D.OverlapCircle(toes.position, checkRadius, thisisGround);

        onWall = Physics2D.OverlapCircle(wallhands.position, checkRadius, thisisGround);


        //check for key press
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            doubleJump = true;
            if (wallSlide)
            {
                wallJump = true;
            }
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            animator.SetBool("isCrouched", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            animator.SetBool("isCrouched", false);
        }

        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        if (unlockedWallJump && onWall && !onGround)
        {
            wallSlide = true;
            dashReset = true;
            jumpReset = true;
            animator.SetBool("onWall", true);
            animator.SetBool("isFalling", false);
        }
        else
        {
            wallSlide = false;
            animator.SetBool("onWall", false);
        }

        // update animations
        if (horizontalMove != 0)
        { 
            animator.SetBool("isMoving", true); 
        }
        else if (horizontalMove == 0)
        { 
            animator.SetBool("isMoving", false);
            // particle timing support
            spawnDirtElapsed = 0;
        }
        if (onGround)
        { 
            animator.SetBool("onGround", true);
            if (!spawnedDirtOnLanding)
            {
                Instantiate(dirtOnLandingEffect, (transform.position + new Vector3(0, -(playerCollider.size.y) / 2, 0)), Quaternion.identity);
                spawnedDirtOnLanding = true;
            }
        }
        else if (!onGround)
        { 
            animator.SetBool("onGround", false);
            spawnedDirtOnLanding = false;
        }

        // spawn particles when walking
        if (onGround && horizontalMove != 0)
        {
            // spawn dirt every 0.5 seconds while crouched on the ground
            if (crouch && (spawnDirtElapsed > 0.5))
            {
                Instantiate(dirtEffect,
                    (transform.position + new Vector3(playerCollider.size.x/2 * direction, -(playerCollider.size.y) / 2, 0)),
                    Quaternion.identity);
                spawnDirtElapsed = 0;
            }
            // 0.4 seconds while running
            else if (!crouch && (spawnDirtElapsed > 0.4))
            {
                Instantiate(dirtEffect, 
                    (transform.position + new Vector3(playerCollider.size.x * direction, -(playerCollider.size.y)/2, 0)),
                    Quaternion.identity);
                spawnDirtElapsed = 0;
            }
            else
            {
                spawnDirtElapsed += Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, crouch);
        jump = false;
        doubleJump = false;
        dash = false;

        if (wallSlide)
        {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, Mathf.Clamp(playerrigidbody.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else if (playerrigidbody.velocity.y < 0)
        {
            playerrigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            //animation support
            animator.SetBool("isFalling", true);
        }
        // animation support
        else if (playerrigidbody.velocity.y >= 0)
        {
            animator.SetBool("isFalling", false);
        }
    }

    public void Move(float move, bool crouch)
    {
        #region crouch move
        if (!crouch)
        {
            if (Physics2D.OverlapCircle(head.position, checkRadius, thisisGround))
            {
                crouch = true;
            }
        }

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
        #endregion

        #region dash 
        if (unlockedDash && dash && dashReset && (dashTimer < (Time.unscaledTime - 1)))
        {
            if (facingRight)
            {
                move = dashSpeed;
            }

            if (!facingRight)
            {
                move = dashSpeed * -1;
            }
            dashReset = false;
            onGround = false;
            dashTimer = Time.unscaledTime;
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, 0);
        }

        #endregion

        #region normal move
        //actually move the character along y axis
        actualMove = new Vector2(move, playerrigidbody.velocity.y);
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
        #endregion

        #region jump movement
        //hey dumbo jumping stuff handled here
        if (onGround)
        {
            jumpReset = true;
            dashReset = true;
        }

        if (onGround && jump)
        {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpForce);
            // visual support
            animator.SetTrigger("jump");
            Instantiate(dirtEffect, (transform.position + new Vector3(0, -(playerCollider.size.y) / 2, 0)), Quaternion.identity);
        }
        else if (wallJump)
        {
            playerrigidbody.velocity = new Vector2(wallJumpX * -direction, wallJumpY);
            SetWallJumpToFalse();
            // visual support
            animator.SetTrigger("jump");
            Instantiate(dirtEffect, (transform.position + new Vector3(playerCollider.size.x / 2 * direction, -(playerCollider.size.y) / 2, 0)), Quaternion.identity);
        }
        else if (unlockedDoubleJump && !onGround && doubleJump && jumpReset)
        {
            // double jump
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpForce);
            jumpReset = false;
            // visual support
            animator.SetTrigger("doubleJump");
        }
        #endregion
    }

    void SetWallJumpToFalse()
    {
        wallJump = false;
        jumpReset = true;
        dashReset = true;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("UnlockWallJump"))
        {
            print("Acquired Wall Jump");
            unlockedWallJump = true;
            unlockAbilityEvent.Invoke(0);
            // he now has gloves in animations
            animator.SetBool("hasGloves", true);
            // delete flag
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("UnlockDash"))
        {
            print("Acquired Dash");
            unlockedDash = true;
            unlockAbilityEvent.Invoke(1);
            // delete flag
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("UnlockDoubleJump"))
        {
            print("Acquired DoubleJump");
            unlockedDoubleJump = true;
            unlockAbilityEvent.Invoke(2);
            // delete flag
            Destroy(collision.gameObject);
        }
    }

    //function to flip character sprite
    private void Flip()
    {
        facingRight = !facingRight;
        direction = Input.GetAxisRaw("Horizontal");
        //flip hit box
        Vector3 hitboxScale = transform.localScale;
        hitboxScale.x = (hitboxScale.x * -1);
        transform.localScale = hitboxScale;
        // particle timing support
        spawnDirtElapsed = 0;
    }

    #region Event Support
    public void AddUnlockAbilityListener(UnityAction<int> listener)
    {
        unlockAbilityEvent.AddListener(listener);
    }
    #endregion
}
