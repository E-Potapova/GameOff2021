using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenu : MonoBehaviour
{
    public void HandleButtonClick()
    {
        AudioManager.PlayMusic();
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
