﻿using System.Collections;
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

    const string ConfigurationDataFileName = "ConfigData.csv";

    // configuration data
    float playerSpeed = 300;
    float jumpForce = 100;
    float crouchSpeed = 10;
    float dashSpeed = 10000;

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

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            //open file
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath,
                ConfigurationDataFileName));
            
            //read values
            string names = input.ReadLine();
            string values = input.ReadLine();
            //read string values into fields
            SetValuesToFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    void SetValuesToFields(string csvValues)
    {
        //split string into array
        string[] values = csvValues.Split(',');

        //set fields
        playerSpeed = float.Parse(values[0]);
        jumpForce = float.Parse(values[1]);
        crouchSpeed = float.Parse(values[2]);
        dashSpeed = float.Parse(values[3]);

    }
}
