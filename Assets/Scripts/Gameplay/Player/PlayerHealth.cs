using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private int playerHealth;
    private int maxHealth;
    public Image[] hp;
    public Sprite fullheart;
    public Sprite emptyheart;

    public void setMaxHealth(int health)
    {
        maxHealth = health;
        playerHealth = 3;
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
    }

    public void takeDamage()
    {
        playerHealth--;
        if (playerHealth < 0)
        {
            Respawn();
        }
        else
        {
            updateHp();
        }
    }

    public void updateHp()
    {
        if(playerHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        for (int i = 0; i < hp.Length; i++ ){
            if(i < playerHealth)
            {
                hp[i].sprite = fullheart;
            }
            else
            {
                hp[i].sprite = emptyheart;
            }
        }
    }

    public void Respawn()
    {

    }





}