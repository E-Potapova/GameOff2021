using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    //spawn positions
    private Vector2 softSpawnPosition;
    private Vector2 hardSpawnPosition;

    // player health support
    private int playerHealth;
    private int maxHealth;
    [SerializeField] Image[] hp;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    // velocity support
    private Rigidbody2D playerrigidbody;

    void Start()
    {
        playerrigidbody = GetComponent<Rigidbody2D>();

        maxHealth = ConfigurationUtils.Health;
        SetMaxHealth();

        softSpawnPosition = transform.position;
        hardSpawnPosition = transform.position;
    }

    private void UpdateHealth(int val)
    {
        playerHealth += val;
        //print(playerHealth);
        UpdateHPSprites();
        if (playerHealth < 1)
        {
            SpawnAtFlag(2);
        }
        else
        {
            SpawnAtFlag(1);
        }
    }

    // 1 for soft spawn, 2 for hard spawn
    private void SpawnAtFlag(int type)
    {
        if (type == 1)
        {
            transform.position = softSpawnPosition;
            playerrigidbody.velocity = new Vector2(0, 0);
        }
        else if (type == 2)
        {
            SetMaxHealth();
            transform.position = hardSpawnPosition;
            playerrigidbody.velocity = new Vector2(0, 0);
        }
    }

    // visual
    private void SetMaxHealth()
    {
        playerHealth = maxHealth;
        for (int i = 0; i < hp.Length; i++)
        {
            if (i < maxHealth)
            {
                hp[i].enabled = true;
            }
            else
            {
                hp[i].enabled = false;
            }
        }
        UpdateHPSprites();
    }

    //update sprites
    private void UpdateHPSprites()
    {
        if (playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        for (int i = 0; i < hp.Length; i++)
        {
            if (i < playerHealth)
            {
                hp[i].sprite = fullHeart;
            }
            else
            {
                hp[i].sprite = emptyHeart;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("SoftSpawnFlag"):
                softSpawnPosition = collision.gameObject.transform.position;
                break;
            case ("HardSpawnFlag"):
                hardSpawnPosition = collision.gameObject.transform.position;
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case ("DoesDamage"):
                UpdateHealth(-1);
                break;
            case ("Heals"):
                UpdateHealth(1);
                break;
        }
    }
}
