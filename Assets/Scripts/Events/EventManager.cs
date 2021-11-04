using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Take Damage
    // char take damage event support
    static List<GameObject> takeDamageInvokers = new List<GameObject>();
    static List<UnityAction<float>> takeDamageListeners = new List<UnityAction<float>>();

    public static void AddTakeDamageInvoker(GameObject invoker)
    {
        takeDamageInvokers.Add(invoker);
        foreach (UnityAction<float> listener in takeDamageListeners)
        {
            //if (invoker is Player)
            //invoker.AddTakeDamageListener(listener);
        }
    }

    public static void AddTakeDamageListener(UnityAction<float> listener)
    {
        takeDamageListeners.Add(listener);
        foreach (GameObject invoker in takeDamageInvokers)
        {
            //invoker.AddTakeDamageListener(listener);
        }
    }
    #endregion

    #region Heal
    // char heal event support
    static List<GameObject> healInvokers = new List<GameObject>();
    static List<UnityAction<float>> healListeners = new List<UnityAction<float>>();

    public static void AddHealInvoker(GameObject invoker)
    {
        healInvokers.Add(invoker);
        foreach (UnityAction<float> listener in healListeners)
        {
            //if (invoker is Player)
            //invoker.AddHealListener(listener);
        }
    }

    public static void AddHealListener(UnityAction<float> listener)
    {
        healListeners.Add(listener);
        foreach (GameObject invoker in healInvokers)
        {
            //invoker.AddHealListener(listener);
        }
    }
    #endregion

    #region Spawn At Flag
    // player spawn at flag event support
    static List<GameObject> spawnAtFlagInvokers = new List<GameObject>();
    static List<UnityAction<Vector3>> spawnAtFlagListeners = new List<UnityAction<Vector3>>();

    public static void AddSpawnAtFlagInvoker(GameObject invoker)
    {
        spawnAtFlagInvokers.Add(invoker);
        foreach (UnityAction<Vector3> listener in spawnAtFlagListeners)
        {
            //if (invoker is Player)
            //invoker.AddSpawnAtFlagListener(listener);
        }
    }

    public static void AddSpawnAtFlagListener(UnityAction<Vector3> listener)
    {
        spawnAtFlagListeners.Add(listener);
        foreach (GameObject invoker in spawnAtFlagInvokers)
        {
            //invoker.AddSpawnAtFlagListener(listener);
        }
    }
    #endregion
}
