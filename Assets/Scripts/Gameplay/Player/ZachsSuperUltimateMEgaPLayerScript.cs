using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private float direction = 0;

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
    private float wallJumpTimer = 0.05f;
    #endregion

    //get the rigidbody
    private Rigidbody2D playerrigidbody;

    //get the collider
    private BoxCollider2D playerCollider;

    //spawn flag
    private Vector2 spawnPosition;
    
    //Soft spawn
    private Vector2 softSpawn;
    private float onGroundTime;
    private bool setGroundTime = false;

    // for player smoothing
    private Vector3 velocity = Vector3.zero;

    // get animator
    private Animator animator;

    // player health support
    private int playerHealth;
    private int maxHealth;
    [SerializeField] Image[] hp;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    #endregion

    //Player abilites toggle
    private bool unlockedWallJump = false;
    private bool unlockedDash = false;
    private bool unlockedDoubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        playerrigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        spawnPosition = transform.position;

        walkSpeed = ConfigurationUtils.PlayerSpeed;
        jumpForce = ConfigurationUtils.JumpForce;
        crouchSpeed = ConfigurationUtils.CrouchSpeed;
        dashSpeed = ConfigurationUtils.DashSpeed;
        fallMultiplier = ConfigurationUtils.FallMultiplier;
        wallSlideSpeed = ConfigurationUtils.WallSlideSpeed;
        wallJumpX = ConfigurationUtils.WallJumpX;
        wallJumpY = ConfigurationUtils.WallJumpY;


        maxHealth = ConfigurationUtils.Health;
        SetMaxHealth();

        // event support
        EventManager.AddTakeDamageListener(TakeDamage);

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
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (Input.GetButtonDown("Dash"))
        {
            dash = true;
        }

        if (onWall && !onGround)
        {
            wallSlide = true;
            animator.SetBool("onWall", true);
        }
        else
        {
            wallSlide = false;
            animator.SetBool("onWall", false);
        }

        // update animations
        if (horizontalMove != 0 && onGround) { animator.SetBool("isRunning", true); }
        else if (horizontalMove == 0 && onGround){ animator.SetBool("isRunning", false); }
    }

    private void FixedUpdate()
    {
        Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, dash);
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
            setGroundTime = false;
            //animation support
            animator.SetBool("isFalling", true);
        }

        #region Soft Spawn
        //Soft Spawn
        if (onGround && (Time.unscaledTime - onGroundTime) > 5 && setGroundTime)
        {
            softSpawn = transform.position;
            setGroundTime = false;
            print("hellooo");
        }
        #endregion

    }

    public void Move(float move, bool crouch, bool jump, bool dash)
    {

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
            animator.SetBool("isCrouched", true);
        }
        else
        {
            playerCollider.size = standingColliderSize;
            animator.SetBool("isCrouched", false);
        }

        if (dash && dashReset)
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

            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, 2);
        }

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


        //hey dumbo jumping stuff handled here
        if (onGround)
        {
            jumpReset = true;
            dashReset = true;
            if (!setGroundTime)
            {
                onGroundTime = Time.unscaledTime;
                setGroundTime = true;
            }
            // animation support
            animator.SetBool("isFalling", false);
        }

        if (onGround && jump)
        {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpForce);
            // animation support
            animator.SetTrigger("jump");
        }
        else if (wallJump)
        {
            playerrigidbody.velocity = new Vector2(wallJumpX * -direction, wallJumpY);
            Invoke("SetWallJumpToFalse", wallJumpTimer);
            animator.SetTrigger("jump");
        }
        // double jump
        else if (!onGround && doubleJump && jumpReset)
        {
            playerrigidbody.velocity = new Vector2(playerrigidbody.velocity.x, jumpForce);
            jumpReset = false;
        }

    }

    void SetWallJumpToFalse()
    {
        wallJump = false;
        jumpReset = true;
        dashReset = true;
    }
    #endregion

    #region Health
    private void TakeDamage(int damage)
    {
        playerHealth -= damage;
        print(playerHealth);
        UpdateHp();
        if (playerHealth < 1)
        {
            SpawnAtFlag();
        }
        else
        {
            //SpawnAtFlag();
            SoftSpawn();
        }
    }

    private void SpawnAtFlag()
    {
        SetMaxHealth();
        transform.position = spawnPosition;
        playerrigidbody.velocity = new Vector2(0, 0);
    }

    private void SoftSpawn()
    {
        transform.position = softSpawn;
        playerrigidbody.velocity = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SpawnFlag"))
        {
            spawnPosition = collision.gameObject.transform.position;
        }
    }

    // visual
    private void SetMaxHealth()
    {
        playerHealth = maxHealth;
        for (int i = 0; i < hp.Length; i++)
        {
            if (i < maxHealth)
            {
                hp[i].enabled = true;
            }
            else
            {
                hp[i].enabled = false;
            }
        }
        UpdateHp();
    }

    //update sprites
    private void UpdateHp()
    {
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        for (int i = 0; i < hp.Length; i++)
        {
            if (i < playerHealth)
            {
                hp[i].sprite = fullHeart;
            }
            else
            {
                hp[i].sprite = emptyHeart;
            }
        }
    }
    #endregion
    //function to flip character sprite
    public void Flip()
    {
        facingRight = !facingRight;
        direction = Input.GetAxisRaw("Horizontal");
        //flip hit box
        Vector3 hitboxScale = transform.localScale;
        hitboxScale.x = (hitboxScale.x * -1);
        transform.localScale = hitboxScale;
    }

}
