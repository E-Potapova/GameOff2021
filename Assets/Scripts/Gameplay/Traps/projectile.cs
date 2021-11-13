using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;


    public void ActiveProjectile()
    {
        gameObject.SetActive(true);
    }

    public void Update()
    {
        float pSpeed = projectileSpeed * Time.deltaTime;
        transform.Translate(pSpeed, 0, 0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

}
