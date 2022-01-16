using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIAmmo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ammoText = null;

    [SerializeField]
    private TextMeshProUGUI magazineText = null;

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

    public void UpdateBulletText(int bulletCount)
    {
        if(bulletCount == 0)
        {
            ammoText.color = Color.red;
        }
        else
        {
            ammoText.color = Color.white;
        }
        ammoText.SetText(bulletCount.ToString());
    }

    public void UpdateMagazineText(int bulletCount)
    {
        if(bulletCount == 0)
        {
            magazineText.color = Color.red;
        }
        else
        {
            magazineText.color = Color.white;
        }
        magazineText.SetText(bulletCount.ToString());
    }

    public void UpdateReloadRechargeBar(float timeLeftUntilReloaded, float reloadDelay)
    {
        if (rt != null)
        {
            var newWidth = ((reloadDelay - timeLeftUntilReloaded) / reloadDelay) * barWidth;
            rt.sizeDelta = new Vector2(newWidth, barHeight);
        }
    }
}
