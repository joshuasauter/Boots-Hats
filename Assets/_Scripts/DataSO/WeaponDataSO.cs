using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/WeaponData")]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField]
    public BulletDataSO BulletData { get; set; }

    [field: SerializeField]
    [field: Range(1, 300)]
    public int AmmoCapacity { get; set; }

    [field: SerializeField]
    [field: Range(1, 100)]
    public int MagazineCapacity { get; set; }
    
    [field: SerializeField]
    public bool AutomaticFire { get; set; } = false;

    [field: SerializeField]
    [field: Range(0.1f, 2f)]
    public float WeaponDelay { get; set; } = .1f;

    [field: SerializeField]
    [field: Range(0.1f, 5f)]
    public float WeaponReloadDelay { get; set; } = 1f;

    [field: SerializeField]
    [field: Range(0, 10)]
    public float SpreadAngle { get; set; } = 5;

    [SerializeField]
    private bool multiBulletShoot = false;

    [SerializeField]
    [Range(1, 10)]
    private int bulletCount = 1;

    internal int GetBulletCountToSpawn()
    {
        if(multiBulletShoot)
        {
            return bulletCount;
        }
        
        return 1;
    }
}