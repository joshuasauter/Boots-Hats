using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponRenderer : MonoBehaviour
{
    public void FlipSprite(bool val)
   {
        int flipModifier = val ? -1 : 1;
        transform.localScale = new Vector3(transform.localScale.x, 
            flipModifier * Mathf.Abs(transform.localScale.y),
            transform.localScale.z);
   }
}
