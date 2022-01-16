using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMelee : MonoBehaviour
{
    [SerializeField]
    private Image statusCheckPrefab = null;

    [SerializeField]
    private Sprite checkmark = null, xMark = null;

    public void UpdateUI(bool canMelee)
    {
        if (canMelee)
        {
            statusCheckPrefab.sprite = checkmark;
        }
        else
        {
            statusCheckPrefab.sprite = xMark;
        }   
    }
}
