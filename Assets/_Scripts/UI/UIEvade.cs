using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEvade : MonoBehaviour
{
    [SerializeField]
    private Image statusCheckPrefab = null;

    [SerializeField]
    private Sprite checkmark = null, xMark = null;

    public void UpdateUI(bool canEvade)
    {
        if (canEvade)
        {
            statusCheckPrefab.sprite = checkmark;
        }
        else
        {
            statusCheckPrefab.sprite = xMark;
        }   
    }
}
