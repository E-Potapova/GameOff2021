using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    #region Unlock Ability
    // unlock wall jump event support
    static List<ZachsSuperUltimateMEgaPLayerScript> unlockAbilityInvokers = new List<ZachsSuperUltimateMEgaPLayerScript>();
    static List<UnityAction<int>> unlockAbilityListeners = new List<UnityAction<int>>();

    public static void AddUnlockAbilityInvoker(ZachsSuperUltimateMEgaPLayerScript invoker)
    {
        unlockAbilityInvokers.Add(invoker);
        foreach (UnityAction<int> listener in unlockAbilityListeners)
        {
            invoker.AddUnlockAbilityListener(listener);
        }
    }

    public static void AddUnlockAbilityListener(UnityAction<int> listener)
    {
        unlockAbilityListeners.Add(listener);
        foreach (ZachsSuperUltimateMEgaPLayerScript invoker in unlockAbilityInvokers)
        {
            invoker.AddUnlockAbilityListener(listener);
        }
    }
    #endregion
}
