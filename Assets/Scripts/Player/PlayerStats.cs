using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject charaterData;

    //Current stats
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentRecovery;
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentMight;
    [HideInInspector]
    public float currentProjectileSpeed;
    [HideInInspector]
    public float currentMagnet;

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    public List<LevelRange> levelRanges;

    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible = false;

    //Inventory
    InventoryManager inventory;
    public int weaponIndex = 0;
    public int passiveItemIndex = 0;

    public GameObject secondWeaponTest;
    public GameObject firstPassiveItemTest, secondPassiveItemTest;

    private void Awake()
    {
        charaterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();//free resource

        inventory = GetComponent<InventoryManager>();

        SpawnWeapon(charaterData.StartingWeapon);//set starting weapon

        currentHealth = charaterData.MaxHealth;
        currentRecovery = charaterData.Recovery;
        currentMoveSpeed = charaterData.MoveSpeed;
        currentMight = charaterData.Might;
        currentProjectileSpeed = charaterData.ProjectileSpeed;
        currentMagnet = charaterData.Magnet;

        SpawnWeapon(secondWeaponTest);//set starting weapon
        SpawnPassiveItem(firstPassiveItemTest);
        SpawnPassiveItem(secondPassiveItemTest);
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else
        {
            isInvincible = false;
        }

        PassiveRecovery();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;

            int experienceCapIncrease = 0;

            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }

            experienceCap += experienceCapIncrease;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        invincibilityTimer = invincibilityDuration;

        isInvincible = true;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Kill();
    }

    void Kill()
    {
        Debug.Log("Killed");
        currentMoveSpeed = 0; //TODO temp
        //Destroy(gameObject);
    }

    public void RestoreHealth(float restoreAmount)
    {
        currentHealth += restoreAmount;

        if (currentHealth > charaterData.MaxHealth)//TODO what happens if max health increases?
            currentHealth = charaterData.MaxHealth;
    }

    public void PassiveRecovery()
    {
        if (currentHealth < charaterData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;

            if (currentHealth > charaterData.MaxHealth)
                currentHealth = charaterData.MaxHealth;
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if(weaponIndex >= inventory.weaponSlots.Count -1 )
        {
            Debug.LogError("Weapon inventory is full.");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);//player's child object

        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());
        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Passive item inventory is full.");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);//player's child object

        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());
        passiveItemIndex++;
    }
}