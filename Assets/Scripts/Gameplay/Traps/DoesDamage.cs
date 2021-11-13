using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoesDamage : MonoBehaviour
{
    // take damage event support
    TakeDamage takeDamageEvent;

    // Start is called before the first frame update
    void Start()
    {
        takeDamageEvent = new TakeDamage();
        EventManager.AddTakeDamageInvoker(this);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            takeDamageEvent.Invoke(1);
        }
    }

    public void AddTakeDamageListener(UnityAction<int> listener)
    {
        takeDamageEvent.AddListener(listener);
    }
}
