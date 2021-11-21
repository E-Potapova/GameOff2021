using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    private float projectileSpeedX;
    private float projectileSpeedY;

    public void SetSpeed(float speedX, float speedY)
    {
        projectileSpeedX = speedX;
        projectileSpeedY = speedY;
    }

    private void Update()
    {
        float pSpeedX = projectileSpeedX * Time.deltaTime;
        float pSpeedY = projectileSpeedY * Time.deltaTime;
        transform.Translate(pSpeedX, pSpeedY, 0); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
