using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public TriggerToChange triggerToChange = new TriggerToChange();
    public bool triggerChangeTo;

    public void UseItem()
    {
        if(triggerToChange == TriggerToChange.puppyTrigger)
        {
            Debug.Log(triggerChangeTo);
        }
    }

    public enum TriggerToChange
    {
        none,
        puppyTrigger
    };
}
