using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configurationData;

    #region Properties

    public static float PlayerSpeed
    {
        get { return configurationData.PlayerSpeed; }
    }

    public static float JumpForce
    {
        get { return configurationData.JumpForce; }
    }

    public static float CrouchSpeed
    {
        get { return configurationData.CrouchSpeed; }
    }

    public static float DashSpeed
    {
        get { return configurationData.DashSpeed; }
    }

    public static float FallMultiplier
    {
        get { return configurationData.FallMultiplier; }
    }

    public static float WallSlideSpeed
    {
        get { return configurationData.WallSlideSpeed; }
    }

    public static float WallJumpX
    {
        get { return configurationData.WallJumpX; }
    }

    public static float WallJumpY
    {
        get { return configurationData.WallJumpY; }
    }

    public static int Health
    {
        get { return configurationData.Health; }
    }

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
