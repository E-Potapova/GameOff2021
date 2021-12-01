using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void HandleButtonClick()
    {
        Object.Instantiate(Resources.Load("ControlsMenu"));
        Destroy(gameObject);
    }
}
