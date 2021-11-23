using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    // configuration data
    float playerSpeed = 300;
    float jumpForce = 12;
    float crouchSpeed = 50;
    float dashSpeed = 200;
    float fallMultiplier = 2.5f;
    float wallSlideSpeed = 2f;
    float wallJumpX = 20;
    float wallJumpY = 10;
    int health = 6;


    #endregion

    #region Properties

    public float PlayerSpeed
    {
        get { return playerSpeed; }
    }

    public float JumpForce
    {
        get { return jumpForce; }
    }

    public float CrouchSpeed
    {
        get { return crouchSpeed; }
    }

    public float DashSpeed
    {
        get { return dashSpeed; }
    }
    public float FallMultiplier
    {
        get { return fallMultiplier; }
    }
    public float WallSlideSpeed
    {
        get { return wallSlideSpeed; }
    }
    public float WallJumpX
    {
        get { return wallJumpX; }
    }
    public float WallJumpY
    {
        get { return wallJumpY; }
    }

    public int Health
    {
        get { return health; }
    }

    #endregion

    public ConfigurationData() { }
}
