using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected GameObject muzzle;

    [SerializeField]
    protected int ammo = 10;

    [SerializeField]
    protected int magazineAmmo = 10;
    
    [SerializeField]
    public WeaponDataSO weaponData;

    public int MagazineAmmo
    {
        get { return magazineAmmo; }
        set {
            magazineAmmo = value;
            OnMagazineAmmoChange?.Invoke(magazineAmmo);
        }
    }

    public int Ammo
    {
        get { return ammo; }
        set {
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity);
            OnAmmoChange?.Invoke(ammo);
        }
    }

    public bool AmmoFull { get => Ammo >= weaponData.AmmoCapacity; }

    protected bool isShooting = false;

    private bool reloadOnNextTry = false;

    [SerializeField]
    protected bool reloadCoroutine = false;

    public bool reloading = false;

    [field: SerializeField]
    public UnityEvent OnShoot { get; set; }

    [field: SerializeField]
    public UnityEvent OnShootNoAmmo { get; set; }

    [field: SerializeField]
    public UnityEvent OnBeginReload { get; set; }

    [field: SerializeField]
    public UnityEvent OnFinishReload { get; set; }

    [field: SerializeField]
    public UnityEvent<int> OnAmmoChange { get; set; }

    [field: SerializeField]
    public UnityEvent<int> OnMagazineAmmoChange { get; set; }

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
        MagazineAmmo = weaponData.MagazineCapacity;
    }

    private void OnDisable()
    {
        reloadCoroutine = false;
        reloading = false;
        StopAllCoroutines();
    }

    public void TryShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }

    public void Reload(int ammo)
    {
        Ammo += ammo;
    }

    public void TryReloading()
    {
        if (ammo > 0 && magazineAmmo < weaponData.MagazineCapacity && reloading == false)
        {
            reloading = true;
            StartCoroutine(ReloadMagazineCoroutine());
        }
    }

    
    private void ReloadMagazine()
    {   
        var difference = weaponData.MagazineCapacity - magazineAmmo;
        var ammoAdd = Math.Min(difference, ammo);
        MagazineAmmo += ammoAdd;
        Ammo -= ammoAdd;
    }

    private void Update()
    {
        UseWeapon();
    }

    private void UseWeapon()
    {
        if(isShooting && reloadCoroutine == false)
        {
            if (magazineAmmo > 0)
            {
                MagazineAmmo--;
                OnShoot?.Invoke();
                for (int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
            }
            else
            {
                if (reloadOnNextTry == false)
                {
                    isShooting = false;
                    reloadOnNextTry = true;
                    OnShootNoAmmo?.Invoke();
                    return;
                }
                else if (reloadOnNextTry)
                {
                    reloadOnNextTry = false;
                    TryReloading();
                }
            }
            FinishShooting();
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if (weaponData.AutomaticFire == false) 
        {
            isShooting = false;
        }
    }

    public IEnumerator ReloadMagazineCoroutine()
    {
        StopCoroutine("DelayNextShootCoroutine");
        reloadCoroutine = true;
        OnBeginReload?.Invoke();
        yield return new WaitForSeconds(weaponData.WeaponReloadDelay);
        OnFinishReload?.Invoke();
        ReloadMagazine();
        reloading = false;
        reloadCoroutine = false;
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay);
        reloadCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }
    
    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }
}