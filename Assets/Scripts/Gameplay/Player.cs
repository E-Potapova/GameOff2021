using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public WooperController2d controller;

    public float walkspeed;

    float horizontalMove = 0f;


    bool jump = false;

    bool crouch = false;

    bool sprint = false;


    // Start is called before the first frame update
    void Start()
    {
        walkspeed = ConfigurationUtils.PlayerSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * walkspeed;
        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


        if (Input.GetButtonDown("Sprint"))
        {
            sprint = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            sprint = false;
        }

    }

    private void FixedUpdate()
    {
        if (sprint)
        {
            controller.Move((horizontalMove * 2) * Time.fixedDeltaTime, crouch, jump);
        }
        else if (crouch)
        {
            controller.Move((horizontalMove / 2) * Time.fixedDeltaTime, crouch, jump);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }




        jump = false;
    }
}
