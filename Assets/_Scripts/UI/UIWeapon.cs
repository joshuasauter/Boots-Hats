using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject primaryWeaponPanel;

    [SerializeField]
    private GameObject secondaryWeaponPanel;

    [SerializeField]
    private GameObject tertiaryWeaponPanel;

    public void HighlightWeapons(bool primary, bool secondary, bool tertiary)
    {
        primaryWeaponPanel.GetComponent<Image>().enabled = primary;
        secondaryWeaponPanel.GetComponent<Image>().enabled = secondary;
        tertiaryWeaponPanel.GetComponent<Image>().enabled = tertiary;
    }
}
