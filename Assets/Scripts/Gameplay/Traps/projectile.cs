using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DoesDamage
{
    private float projectileSpeedX;
    private float projectileSpeedY;
    public void SetSpeed(float speedX, float speedY)
    {
        projectileSpeedX = speedX;
        projectileSpeedY = speedY;
    }

    public void Update()
    {
        float pSpeedX = projectileSpeedX * Time.deltaTime;
        float pSpeedY = projectileSpeedY * Time.deltaTime;
        transform.Translate(pSpeedX, pSpeedY, 0); 
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Destroy(gameObject);
    }
}
