using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    protected AnimationClip meleeAttackAnimation;

    [field: SerializeField]
    public virtual MeleeWeaponDataSO MeleeWeaponData { get; set; }

    private void Awake()
    {
        float animDuration = meleeAttackAnimation.length;
        StartCoroutine(DestroyPrefabCoroutine(animDuration));
    }

    IEnumerator DestroyPrefabCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hittable = collision.GetComponent<IHittable>();

        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            HitObstacle(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            hittable?.GetHit(MeleeWeaponData.Damage, gameObject);
            HitEnemy(collision);
        }
    }

    private void HitObstacle(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right);
        if (hit.collider != null)
        {
            Instantiate(MeleeWeaponData.ImpactObstaclePrefab, hit.point, Quaternion.identity);
        }
    }

    private void HitEnemy(Collider2D collision)
    {
        var knockback = collision.GetComponent<IKnockBack>();
        knockback?.KnockBack(transform.right, MeleeWeaponData.KnockBackPower, MeleeWeaponData.KnockBackDelay);
        Instantiate(MeleeWeaponData.ImpactEnemyPrefab, collision.transform.position, Quaternion.identity);
    }
}
