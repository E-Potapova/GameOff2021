using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
	void Awake()
    {
        //initialize Config Util
        ConfigurationUtils.Initialize();
    }
}
