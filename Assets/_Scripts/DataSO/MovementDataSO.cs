using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Agent/MovementData")]

public class MovementDataSO : ScriptableObject
{
    [Range(1,10)]
    public float maxSpeed = 5;

    [Range(0.1f, 100)]
    public float acceleration = 50, deacceleration = 50;

    [Range(0.1f, 15)]
    public float dodgeSpeed = 8;

    [Range(0.1f, 5)]
    public float dodgeDuration = 0.3f;

    [Range(0.1f, 5)]
    public float dodgeCoolDownDuration = 1;
}
