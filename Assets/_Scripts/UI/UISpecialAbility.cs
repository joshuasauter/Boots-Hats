using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpecialAbility : MonoBehaviour
{
    [field: SerializeField]
    private GameObject RechargeBar { get; set; } = null;

    private Image rechargeBarImage = null;

    private RectTransform rt = null;

    private float barHeight = 0;

    private float barWidth = 0;

    private void Awake()
    {
        rt = RechargeBar.GetComponent<RectTransform>();
        rechargeBarImage = RechargeBar.GetComponent<Image>();
        barHeight = rt.sizeDelta.y;
        barWidth = rt.sizeDelta.x;
    }

    public void UpdateUI(float timeLeftUntilCharged, float rechargeDelay)
    {
        var newWidth = ((rechargeDelay - timeLeftUntilCharged) / rechargeDelay) * barWidth;
        rt.sizeDelta = new Vector2(newWidth, barHeight);
    }

    public void UpdateUIStatus(bool isCharged)
    {
        if (isCharged)
        {
            rechargeBarImage.color = Color.white;
        }
        else
        {
            rechargeBarImage.color = Color.red;
        }
    }
}
