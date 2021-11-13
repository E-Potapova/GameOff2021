using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : DoesDamage
{
    private float projectileSpeed;

    public void SetSpeed(float speed)
    {
        projectileSpeed = speed;
    }

    public void Update()
    {
        float pSpeed = projectileSpeed * Time.deltaTime;
        transform.Translate(pSpeed, 0, 0); 
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Destroy(gameObject);
    }
}
