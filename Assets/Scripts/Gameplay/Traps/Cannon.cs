using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float attackTime;
    [SerializeField] float bulletSpeedX;
    [SerializeField] float bulletSpeedY;
    [SerializeField] GameObject prefabBullet;
    private float cooldown;

    void Update()
    {
        cooldown = Time.deltaTime + cooldown;

        if(cooldown > attackTime)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        cooldown = 0;
        GameObject bullet = Instantiate<GameObject>(prefabBullet);
        bullet.transform.position = transform.position;
        projectile script = bullet.GetComponent<projectile>();
        script.SetSpeed(bulletSpeedX, bulletSpeedY);
    }
}
