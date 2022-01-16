using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeWeaponData")]
public class MeleeWeaponDataSO : ScriptableObject
{
    [field: SerializeField]
    [field: Range(1,10)]
    public int Damage { get; set; } = 1;

    [field: SerializeField]
    [field: Range(0.1f, 5)]
    public float WeaponDelay { get; set; } = .1f;

    [field: SerializeField]
    public GameObject ImpactObstaclePrefab { get; set; }

    [field: SerializeField]
    public GameObject ImpactEnemyPrefab { get; set; } 

    [field: SerializeField]
    [field: Range(1,20)]
    public float KnockBackPower { get; set; } = 5;

    [field: SerializeField]
    [field: Range(0.01f,1f)]
    public float KnockBackDelay { get; set; } = 0.1f;

    [field: SerializeField]
    public GameObject meleeAttackPrefab { get; set; }
}
