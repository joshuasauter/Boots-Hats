using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : AgentWeapon
{
    [SerializeField]
    private UIAmmo uiAmmo = null;

    public bool AmmoFull { get => weapon != null && weapon.AmmoFull;}

    protected float timeLeftUntilReloaded = 0;

    protected float reloadDelay;

    public float TimeLeftUntilReloaded {
        get => timeLeftUntilReloaded;
        set 
        {
            timeLeftUntilReloaded = value;
            uiAmmo.UpdateReloadRechargeBar(timeLeftUntilReloaded, reloadDelay);
        }
    }

    private void Start()
    {
        if (weapon != null)
        {
            reloadDelay = weapon.weaponData.WeaponReloadDelay;
        }
    }

    private void OnEnable()
    {
        if (weapon != null)
        {
            weapon.OnAmmoChange.AddListener(uiAmmo.UpdateBulletText);
            weapon.OnMagazineAmmoChange.AddListener(uiAmmo.UpdateMagazineText);
        }
    }

    private void OnDisable()
    {
        if (weapon != null)
        {
            weapon.OnAmmoChange.RemoveListener(uiAmmo.UpdateBulletText);
            weapon.OnMagazineAmmoChange.RemoveListener(uiAmmo.UpdateMagazineText);
        }
    }

    public void ToggleUI()
    {
        uiAmmo.UpdateBulletText(weapon.Ammo);
        uiAmmo.UpdateMagazineText(weapon.MagazineAmmo);
    }

    public void AddAmmo(int amount)
    {
        if (weapon != null)
            weapon.Ammo += amount;
    }

    public void ReloadChargeUp()
    {
        if (weapon != null)
        {
            float delay = weapon.weaponData.WeaponReloadDelay;
            TimeLeftUntilReloaded = delay;
            DOTween.To(() => TimeLeftUntilReloaded, 
                x => TimeLeftUntilReloaded = x, 0, delay);
        }
    }
}
