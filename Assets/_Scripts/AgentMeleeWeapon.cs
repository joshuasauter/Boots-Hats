using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMeleeWeapon : MonoBehaviour
{
    protected float desiredAngle;

    [SerializeField]
    protected MeleeWeaponRenderer meleeWeaponRenderer;

    [SerializeField]
    protected MeleeWeapon meleeWeapon;

    private void Awake()
    {
        AssignMeleeWeapon();
    }

    private void AssignMeleeWeapon()
    {
        meleeWeapon = GetComponentInChildren<MeleeWeapon>();
        meleeWeaponRenderer = GetComponentInChildren<MeleeWeaponRenderer>();
    }

    public virtual void AimWeapon(Vector2 pointerPosition)
    {
        var aimDirection = (Vector3)pointerPosition - transform.position;
        desiredAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        AdjustWeaponRendering();
        transform.rotation = Quaternion.AngleAxis(desiredAngle, Vector3.forward);
    }

    protected void AdjustWeaponRendering()
    {
        if(meleeWeaponRenderer != null)
        {
            meleeWeaponRenderer.FlipSprite(desiredAngle > 90 || desiredAngle < -90);
        }
    }

    public void Fire()
    {
        if(meleeWeapon != null)
        {
            meleeWeapon.TryFiring();
        }
    }

    public void StopFiring()
    {
        if(meleeWeapon != null)
        {
            meleeWeapon.StopFiring();
        }
    }
}
