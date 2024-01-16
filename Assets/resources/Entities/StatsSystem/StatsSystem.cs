using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class StatsSystem : MonoBehaviour
{
    [HideInInspector] public bool isDead = false;
    
    private float health;
    private bool enbalePassiveHealing;
    [Header("Health")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private GameObject healthBarPreFab;
    [SerializeField] private float healthBarOffset = 0.75f;
    [SerializeField] private float healthBarScale = 1f;
    
    [Header("Stamina")]
    [SerializeField] private float maxStamina = 16;
    private float currentStamina;
    [HideInInspector]public bool hasStaminaRegenerated = true;
    [HideInInspector]public bool isSprinting = false;
    [SerializeField] private float staminaRegen = 2f;
    [SerializeField] private float staminaDrain = 2f;

    [Header("Armor")]
    [SerializeField] private float defense = 0;

    [Header("Coin")]
    [SerializeField] private int coins = 5;
    [SerializeField] public int maxCoins = 99;
    public Inventory inventory;
    [SerializeField] public bool isPlayer = false;
    
    private GameObject healthBar;
    private float passiveHealPerInstance = 5;
    private int passiveHealInterval = 2500;
    private int startTime;
    
    public UnityEvent<GameObject> OnHit, OnDeath;


    void Awake()
    {
        inventory = new Inventory(40);
    }

    void Start()
    {
        startTime = Time.frameCount;
        health = maxHealth;
        healthBar = Instantiate(healthBarPreFab, gameObject.transform);

        currentStamina = maxStamina;

        //Adjusts the local placement of the healthbar
        healthBar.transform.localPosition = new Vector3(healthBar.transform.localPosition.x, healthBarOffset);
        healthBar.transform.localScale *= healthBarScale;


    }

    void Update()
    {
        
        handleStamina();
        executePassiveHeal();

        healthBar.transform.Find("Bar").transform.localScale = new Vector3(getHealthPercent(), 1);
    }


    private void handleStamina()
    {
        if (!isSprinting)
        {
            if (currentStamina <= maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                    hasStaminaRegenerated = true;
                }
            }
        }
    }

    public void consumeStamina()
    {
        if (hasStaminaRegenerated)
        {
            isSprinting = true;
            currentStamina -= staminaDrain * Time.deltaTime;

            if (currentStamina <= staminaDrain  * Time.deltaTime)
            {
                isSprinting = false;
                hasStaminaRegenerated = false;
            }
        }
    }


    private void executePassiveHeal()
    {
        if (health <= maxHealth && (Time.frameCount - startTime)%passiveHealInterval == 0)
        {
            health += maxHealth - health < passiveHealPerInstance
                ? maxHealth - health
                : passiveHealPerInstance;
        }
    }
    public float getHealth()
    {
        return health;
    }
    
    public void setMaxHealth(float toSet)
    {
        maxHealth = toSet;
    }

    public float getMaxHealth()
    {
        return maxHealth;
    }

    public float getHealthPercent()
    {
        return health / maxHealth;
    }

    public void setHealth(float toSet)
    {
        health = toSet;
        if (health <= 0) Destroy(gameObject);
    }

    public void receiveDamage(float removeHealth, GameObject sender, bool ignoreDefense)
    {
        if (isDead )
        {
            return;
        }

        health -= ignoreDefense ? removeHealth : getDamageWithDefense(removeHealth);

        if (health > 0f)
        {
            OnHit?.Invoke(sender);
        }
        else
        {
            OnDeath?.Invoke(sender);
            isDead = true;
            if (isPlayer)
            {
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }
    
    public void heal(float toHeal)
    {
        if (health + toHeal > maxHealth)
        {
            health = maxHealth;
        }
        else health += toHeal;

    }
    
    public void heal()
    {
        health = maxHealth;
    }

    public void setPassiveHealing(int interval, float amount)
    {
        passiveHealInterval = interval;
        passiveHealPerInstance = amount;
    }

    public bool canPassiveHeal()
    {
        return enbalePassiveHealing;
    }

    public void enablePassiveHeal()
    {
        enbalePassiveHealing = true;
    }

    public void disablePassiveHeal()
    {
        enbalePassiveHealing = false;
    }

    public void addCoin() {
        if(coins + 1 <= maxCoins) coins++;
    }

    public void addCoin(int amount) {
        coins = coins + amount <= maxCoins ? coins + amount : maxCoins;
    }
    public bool canAddCoins(int amount) {
        if(coins + amount > maxCoins) return false; 
        return true;
    }

    private float getDamageWithDefense(float damage) {
        //TODO: maxHealth might be needed to be changed if that doesnt work 
        return damage * (1 + defense/100);
    }

 }
