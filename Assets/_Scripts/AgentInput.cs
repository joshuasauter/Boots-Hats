using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour, IAgentInput
{
    private Camera mainCamera;

    private bool fireButtonDown = false;

    private bool fireSecondaryButtonDown = false;

    private bool eButtonDown = false;

    [field: SerializeField]
    public UnityEvent<Vector2> OnMovementKeyPressed { get; set; }

    [field: SerializeField]
    public UnityEvent<Vector2> OnPointerPositionChange { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireSecondaryButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnFireSecondaryButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent OnJumpButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnEButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnEButtonReleased { get; set; }

    [field: SerializeField]
    public UnityEvent OnRButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnOneButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnTwoButtonPressed { get; set; }

    [field: SerializeField]
    public UnityEvent OnThreeButtonPressed { get; set; }


    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        GetMovementInput();
        GetPointerInput();
        GetFireInput();
        GetJumpInput();
        GetSpecialInput();
        GetReloadInput();
        GetNumberInput();
    }

    private void GetJumpInput()
    {
        if (Input.GetAxisRaw("Jump") > 0)
        {
            OnJumpButtonPressed?.Invoke();
        }
    }

    private void GetFireInput()
    {
        if(Input.GetAxisRaw("Fire1") > 0)
        {
            if (!fireButtonDown)
            {
                fireButtonDown = true;
                OnFireButtonPressed?.Invoke();
            }
        }
        else
        {
            if(fireButtonDown)
            {
                fireButtonDown = false;
                OnFireButtonReleased?.Invoke();
            }
        }

        if (Input.GetAxisRaw("Fire2") > 0)
        {
            if (!fireSecondaryButtonDown)
            {
                fireSecondaryButtonDown = true;
                OnFireSecondaryButtonPressed?.Invoke();
            }
        }
        else
        {
            if(fireSecondaryButtonDown)
            {
                fireSecondaryButtonDown = false;
                OnFireSecondaryButtonReleased?.Invoke();
            }
        }
    }

    private void GetPointerInput()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = mainCamera.nearClipPlane;
        var mouseInWorldSpace = mainCamera.ScreenToWorldPoint(mousePos);
        OnPointerPositionChange?.Invoke(mouseInWorldSpace);
    }

    private void GetMovementInput()
    {
        OnMovementKeyPressed?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), 
            Input.GetAxisRaw("Vertical")));
    }

    private void GetSpecialInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!eButtonDown)
            {
                eButtonDown = true;
                OnEButtonPressed?.Invoke();
            }
        }
        else
        {
            if(eButtonDown)
            {
                eButtonDown = false;
                OnEButtonReleased?.Invoke();
            }
        }
    }

    private void GetReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OnRButtonPressed?.Invoke();
        }
    }

    private void GetNumberInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnOneButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            OnTwoButtonPressed?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            OnThreeButtonPressed?.Invoke();
        }
    }
}
