using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        // event support
        EventManager.AddUnlockAbilityListener(HandleAbilityUnlockMenus);
    }

    private void Update()
    {
        if (GameObject.Find("PauseMenu(Clone)") == null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Object.Instantiate(Resources.Load("PauseMenu"));
            }
        }
    }

    private void HandleAbilityUnlockMenus(int menuType)
    {
        if (menuType == 0)
        {
            Object.Instantiate(Resources.Load("UnlockWallJumpMenu"));
        }
        else if (menuType == 1)
        {
            Object.Instantiate(Resources.Load("UnlockDashMenu"));
        }
        else if (menuType == 2)
        {
            Object.Instantiate(Resources.Load("UnlockDoubleJumpMenu"));
        }
    }

}
