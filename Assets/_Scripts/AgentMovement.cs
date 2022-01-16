using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentMovement : MonoBehaviour
{
    protected Rigidbody2D rigidbody2d;

    [field: SerializeField]
    public MovementDataSO MovementData { get; set; }

    [SerializeField]
    protected float currentVelocity = 3;

    protected Vector2 movementDirection;

    protected bool directionIsRight = true;

    protected bool isKnockedBack = false;

    protected bool isDodgeRolling = false;

    protected bool canDodgeRoll = true;

    public bool CanDodgeRoll {
        get => canDodgeRoll;
        set 
        {
            canDodgeRoll = value;
            uiEvade.UpdateUI(canDodgeRoll);
        }
    }

    [field: SerializeField]
    public UIEvade uiEvade { get; set; }

    [field: SerializeField]
    public UnityEvent<float> OnVelocityChange { get; set; }

    [field: SerializeField]
    public UnityEvent<bool> OnXMovmentDirectionChanged { get; set; }

    [field: SerializeField]
    public UnityEvent OnDodgeRollStart { get; set; }

    [field: SerializeField]
    public UnityEvent OnDodgeRollEnd { get; set; }

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    public void MoveAgent(Vector2 moveInput)
    {
        if (isDodgeRolling == false)
        {
            if (moveInput.magnitude > 0)
            {
                movementDirection = moveInput.normalized;
            }
            if ((directionIsRight && movementDirection.x < 0) || 
                (directionIsRight == false && movementDirection.x > 0))
            {
                ChangeXDirectionParameters();
            }

            currentVelocity = CalculateSpeed(moveInput);
        }
    }

    private void ChangeXDirectionParameters()
    {
        directionIsRight = !directionIsRight;
        if (directionIsRight)
        {
            OnXMovmentDirectionChanged?.Invoke(false);
        }
        else
        {
            OnXMovmentDirectionChanged?.Invoke(true);
        }
    }

    private float CalculateSpeed(Vector2 moveInput)
    {
        if(moveInput.magnitude > 0)
        {
            currentVelocity += MovementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= MovementData.deacceleration * Time.deltaTime;
        }
        
        return Mathf.Clamp(currentVelocity, 0, MovementData.maxSpeed);
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(currentVelocity);
        if (isKnockedBack == false)
        {
            rigidbody2d.velocity = movementDirection.normalized * currentVelocity;
        }
    }

    public void StopImmediately()
    {
        currentVelocity = 0;
        rigidbody2d.velocity = Vector2.zero;
    }

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if(isKnockedBack == false)
        {
            isKnockedBack = true;
            StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

    public void ResetKnockBack()
    {
        ResetKnockBackParameters();
    }

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        rigidbody2d.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParameters();
    }

    private void ResetKnockBackParameters()
    {
        currentVelocity = 0;
        rigidbody2d.velocity = Vector2.zero;
        isKnockedBack = false;
    }

    public void DodgeRoll()
    {
        if (isDodgeRolling == false && canDodgeRoll)
        {
            isDodgeRolling = true;
            CanDodgeRoll = false;
            StartCoroutine(DodgeRollCoroutine());
        }
    }

    IEnumerator DodgeRollCoroutine()
    {
        currentVelocity = MovementData.dodgeSpeed;
        OnDodgeRollStart?.Invoke();
        yield return new WaitForSeconds(MovementData.dodgeDuration);
        OnDodgeRollEnd?.Invoke();
        ResetDodgeRoll();
    }

    public void ResetDodgeRoll()
    {
        StopAllCoroutines();
        StopCoroutine("DodgeRollCoroutine");
        ResetDodgeRollParameters();
        StartCoroutine(RechargeDodgeRollCoroutine());
    }

    private void ResetDodgeRollParameters()
    {
        currentVelocity = MovementData.maxSpeed;
        rigidbody2d.velocity = Vector2.zero;
        isDodgeRolling = false;
    }

    IEnumerator RechargeDodgeRollCoroutine()
    {
        yield return new WaitForSeconds(MovementData.dodgeCoolDownDuration);
        CanDodgeRoll = true;
    }
}
