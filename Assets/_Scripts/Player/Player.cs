using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IAgent, IHittable
{
    [SerializeField]
    private int maxHealth = 2;

    private int health;

    public int Health { 
        get => health;
        set 
        {
            health = Mathf.Clamp(value,0,maxHealth);
            uiHealth.UpdateUI(health);
        } 
    }

    private bool dead = false;

    private bool isDodgeRolling = false;

    private Component[] weaponsList;

    public PlayerWeapon playerWeapon;

    [field: SerializeField]
    public UIHealth uiHealth { get; set; }

    [field: SerializeField]
    public UnityEvent OnDie { get; set; }

    [field: SerializeField]
    public UnityEvent OnGetHit { get; set; }

    private void Awake()
    {
        weaponsList = GetComponentsInChildren<PlayerWeapon>();
        playerWeapon = GetComponentInChildren<PlayerWeapon>();
    }

    private void Start()
    {
        Health = maxHealth;
        uiHealth.Initialize(Health);
    }

    public void AssignCurrentWeapon()
    {
        foreach (PlayerWeapon weapon in weaponsList)
        {
            if(weapon.gameObject.activeSelf)
            {
                playerWeapon = weapon;
            }
        }
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if(dead == false && isDodgeRolling == false)
        {
            Health--;
            OnGetHit?.Invoke();
            if (Health <= 0)
            {
                OnDie?.Invoke();
                dead = true;
            }
        }
    }

    public void ChangeDodgeRollState(bool val)
    {
        isDodgeRolling = val;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            var resource = collision.gameObject.GetComponent<Resource>();

            if (resource != null)
            {
                switch (resource.ResourceData.ResourceType)
                {
                    case ResourceTypeEnum.Health:
                        if(Health >= maxHealth)
                        {
                            return;
                        }
                        Health += resource.ResourceData.GetAmount();
                        resource.PickUpResource();
                        break;
                    case ResourceTypeEnum.Ammo:
                        if (playerWeapon.AmmoFull)
                        {
                            return;
                        }
                        playerWeapon.AddAmmo(resource.ResourceData.GetAmount());
                        resource.PickUpResource();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}