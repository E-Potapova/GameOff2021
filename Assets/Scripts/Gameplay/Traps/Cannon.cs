using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] float attackTime;
    [SerializeField] float bulletSpeed;
    [SerializeField] GameObject prefabBullet;
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
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
        Projectile script = bullet.GetComponent<Projectile>();
        script.SetSpeed(bulletSpeed);
    }
}
