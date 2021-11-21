using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAbilityMenu : MonoBehaviour
{
    float keyPressTimer = 2.0f;
    float elapsedSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedSeconds += Time.unscaledDeltaTime;
        if (elapsedSeconds >= keyPressTimer)
        {
            if (Input.anyKey)
            {
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }
    }
}
