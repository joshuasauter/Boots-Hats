using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponInventoryToggle : MonoBehaviour
{
    [SerializeField]
    private UIWeapon uiWeapon = null;

    [SerializeField]
    public GameObject primaryWeapon = null;

    [SerializeField]
    public GameObject secondaryWeapon = null;

    [SerializeField]
    public GameObject tertiaryWeapon = null;

    private GameObject currentWeapon = null;

    [field: SerializeField]
    public UnityEvent OnWeaponSwap { get; set; }

    private void Start()
    {
        SwitchToPrimary();
    }

    public void SwitchToPrimary()
    {
        ToggleWeapons(true, false, false);
        uiWeapon.HighlightWeapons(true, false, false);
        currentWeapon = primaryWeapon;
        primaryWeapon.GetComponent<PlayerWeapon>().ToggleUI();
    }

    public void SwitchToSecondary()
    {
       ToggleWeapons(false, true, false);
       uiWeapon.HighlightWeapons(false, true, false);
       currentWeapon = secondaryWeapon;
       secondaryWeapon.GetComponent<PlayerWeapon>().ToggleUI();
    }

    public void SwitchToTertiary()
    {
        ToggleWeapons(false, false, true);
        uiWeapon.HighlightWeapons(false, false, true);
        currentWeapon = tertiaryWeapon;
        tertiaryWeapon.GetComponent<PlayerWeapon>().ToggleUI();
    }

    public void TryReloading()
    {
        currentWeapon.GetComponent<PlayerWeapon>().Reload();
    }

    private void ToggleWeapons(bool primary, bool secondary, bool tertiary)
    {
        if(primaryWeapon != null)
        {
            primaryWeapon.SetActive(primary);
        }
        if(secondaryWeapon != null)
        {
            secondaryWeapon.SetActive(secondary);
        }
        if(tertiaryWeapon != null)
        {
            tertiaryWeapon.SetActive(tertiary);
        }

        OnWeaponSwap?.Invoke();
    }
}
