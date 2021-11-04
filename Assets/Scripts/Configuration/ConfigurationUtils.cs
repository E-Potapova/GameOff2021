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

    #endregion

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
