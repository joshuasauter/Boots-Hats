using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class SpecialAbility : MonoBehaviour
{
    [field: SerializeField]
    public UnityEvent OnUseSpecialAbility { get; set; }

    [field: SerializeField]
    public UISpecialAbility uiSpecialAbility { get; set; }

    [SerializeField]
    [Range(0, 120)]
    protected int rechargeDelay = 20;

    protected float timeLeftUntilCharged = 0;

    public float TimeLeftUntilCharged {
        get => timeLeftUntilCharged;
        set 
        {
            timeLeftUntilCharged = value;
            uiSpecialAbility.UpdateUI(timeLeftUntilCharged, rechargeDelay);
        }
    }

    protected bool isCharged = true;

    public bool IsCharged {
        get => isCharged;
        set 
        {
            isCharged = value;
            uiSpecialAbility.UpdateUIStatus(isCharged);
        }
    }


    public abstract void CreateSpecialAbility();

    public abstract void CompletePreviousSpecialAbility();

    protected virtual void OnDestroy()
    {
        CompletePreviousSpecialAbility();
    }
}

