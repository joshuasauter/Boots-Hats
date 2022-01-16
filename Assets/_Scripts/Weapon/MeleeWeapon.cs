using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject muzzle;

    [SerializeField]
    protected MeleeWeaponDataSO meleeWeaponData;

    protected bool isFiring = false;

    protected bool rechargeCoroutine = false;

    public bool RechargeCoroutine {
        get => rechargeCoroutine;
        set 
        {
            rechargeCoroutine = value;
            if (uiMelee != null)
            {
                uiMelee.UpdateUI(!rechargeCoroutine);
            }
        }
    }

    [field: SerializeField]
    public UIMelee uiMelee { get; set; }

    [field: SerializeField]
    public UnityEvent OnFire { get; set; }

    public void TryFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void Update()
    {
        UseMeleeWeapon();
    }

    private void UseMeleeWeapon()
    {
        if(isFiring && rechargeCoroutine == false)
        {
            OnFire?.Invoke();   
            FireMeleeAttack();
            FinishFiring();
        }
    }

    private void FinishFiring()
    {
        StartCoroutine(DelayNextMeleeAttackCoroutine());
        isFiring = false;
    }

    protected IEnumerator DelayNextMeleeAttackCoroutine()
    {
        RechargeCoroutine = true;
        yield return new WaitForSeconds(meleeWeaponData.WeaponDelay);
        RechargeCoroutine = false;
    }

    private void FireMeleeAttack()
    {
        SpawnMeleeAttack(muzzle.transform.position, muzzle.transform.rotation);
    }

    private void SpawnMeleeAttack(Vector3 position, Quaternion rotation)
    {
        var meleeAttackPrefab = Instantiate(meleeWeaponData.meleeAttackPrefab, position, rotation);
        meleeAttackPrefab.transform.localScale = new Vector3(
            meleeAttackPrefab.transform.localScale.x, transform.localScale.y, 
            meleeAttackPrefab.transform.localScale.z);
        meleeAttackPrefab.GetComponent<MeleeAttack>().MeleeWeaponData = meleeWeaponData;
    }
}
