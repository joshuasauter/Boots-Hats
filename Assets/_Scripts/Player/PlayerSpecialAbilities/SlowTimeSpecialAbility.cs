using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SlowTimeSpecialAbility : SpecialAbility
{
    [SerializeField]
    private float freezeTimeDelay = 0.01f, unfreezeTimeDelay = 10f;

    [SerializeField]
    [Range(0,1)]
    private float timeFreezeValue = 0.5f;

    [field: SerializeField]
    public UnityEvent OnSpecialAbilityEnd { get; set; }

    public override void CompletePreviousSpecialAbility()
    {
        if (TimeController.instance != null)
        {
            TimeController.instance.ResetTimeScale();
        }
    }

    public override void CreateSpecialAbility()
    {
        if (isCharged == false)
        {
            return;
        }
        IsCharged = false;
        OnUseSpecialAbility?.Invoke();
        TimeController.instance.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, 
            ()=> TimeController.instance.ModifyTimeScale(1, unfreezeTimeDelay));
        StartCoroutine(WaitForRechargeCoroutine(unfreezeTimeDelay));
    }

    IEnumerator WaitForRechargeCoroutine(float duration)
    {
        DOTween.To(() => TimeLeftUntilCharged, x => TimeLeftUntilCharged = x, 
            rechargeDelay, duration * timeFreezeValue);
        yield return new WaitForSecondsRealtime(duration);
        OnSpecialAbilityEnd?.Invoke();
        Recharge();
    }

    private void Recharge()
    {
        DOTween.To(() => TimeLeftUntilCharged, x => TimeLeftUntilCharged = x, 
            0, rechargeDelay);
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        yield return new WaitForSeconds(rechargeDelay);
        IsCharged = true;
    }
}
