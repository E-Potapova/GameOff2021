using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Take Damage
    // char take damage event support
    static List<DoesDamage> takeDamageInvokers = new List<DoesDamage>();
    static List<UnityAction<int>> takeDamageListeners = new List<UnityAction<int>>();

    public static void AddTakeDamageInvoker(DoesDamage invoker)
    {
        takeDamageInvokers.Add(invoker);
        foreach (UnityAction<int> listener in takeDamageListeners)
        {
            invoker.AddTakeDamageListener(listener);
        }
    }

    public static void AddTakeDamageListener(UnityAction<int> listener)
    {
        takeDamageListeners.Add(listener);
        foreach (DoesDamage invoker in takeDamageInvokers)
        {
            invoker.AddTakeDamageListener(listener);
        }
    }
    #endregion

    #region Heal
    // char heal event support
    static List<GameObject> healInvokers = new List<GameObject>();
    static List<UnityAction<int>> healListeners = new List<UnityAction<int>>();

    public static void AddHealInvoker(GameObject invoker)
    {
        healInvokers.Add(invoker);
        foreach (UnityAction<int> listener in healListeners)
        {
            //if (invoker is Player)
            //invoker.AddHealListener(listener);
        }
    }

    public static void AddHealListener(UnityAction<int> listener)
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

    #region Unlock Ability
    // unlock wall jump event support
    static List<ZachsSuperUltimateMEgaPLayerScript> unlockAbilityInvokers = new List<ZachsSuperUltimateMEgaPLayerScript>();
    static List<UnityAction<int>> unlockAbilityListeners = new List<UnityAction<int>>();

    public static void AddUnlockWallJumpInvoker(ZachsSuperUltimateMEgaPLayerScript invoker)
    {
        unlockAbilityInvokers.Add(invoker);
        foreach (UnityAction<int> listener in unlockAbilityListeners)
        {
            invoker.AddUnlockWallJumpListener(listener);
        }
    }

    public static void AddUnlockWallJumpListener(UnityAction<int> listener)
    {
        unlockAbilityListeners.Add(listener);
        foreach (ZachsSuperUltimateMEgaPLayerScript invoker in unlockAbilityInvokers)
        {
            invoker.AddUnlockWallJumpListener(listener);
        }
    }
    #endregion
}
