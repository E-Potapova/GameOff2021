using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{

    [SerializeField] private float attackTime;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bullet;
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
        bullet[FindBullet()].transform.position = firePoint.position;
        bullet[FindBullet()].GetComponent<projectile>().ActiveProjectile();
    }

    private int FindBullet()
    {
        for(int i = 0; i < bullet.Length; i++)
        {
            if (!bullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

}
