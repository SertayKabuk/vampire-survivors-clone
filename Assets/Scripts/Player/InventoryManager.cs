using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private const int defaultSize = 6;

    public List<WeaponController> weaponSlots = new(defaultSize);
    public int[] weaponLevels = new int[defaultSize];
    public List<Image> weaponUISlots = new(defaultSize);

    public List<PassiveItem> passiveItemSlots = new(defaultSize);
    public int[] passiveItemLevels = new int[defaultSize];
    public List<Image> passiveItemUISlots = new(defaultSize);

    public void AddWeapon(int slotIndex, WeaponController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
        weaponUISlots[slotIndex].enabled = true;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
        passiveItemUISlots[slotIndex].enabled = true;
    }
 
    private void Start()
    {
        foreach (var item in weaponUISlots)
        {
            item.enabled = item.sprite != null;
        }

        foreach (var item in passiveItemUISlots)
        {
            item.enabled = item.sprite != null;
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if (weaponSlots.Count > slotIndex)
        {
            var weapon = weaponSlots[slotIndex];

            if (!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogErrorFormat("Next level weapon prefab is null. Weapon name: {0}, Current level: {1}", weapon.name, weapon.weaponData.Level);
                return;
            }

            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform);

            var weaponController = upgradedWeapon.GetComponent<WeaponController>();
            AddWeapon(slotIndex, weaponController);
            Destroy(weapon.gameObject);

            weaponLevels[slotIndex] = weaponController.weaponData.Level;
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemSlots.Count > slotIndex)
        {
            var passiveItem = passiveItemSlots[slotIndex];

            if (!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogErrorFormat("Next level passive item prefab is null. Item name: {0}, Current level: {1}", passiveItem.name, passiveItem.passiveItemData.Level);
                return;
            }

            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform);

            var passiveItemComponent = upgradedPassiveItem.GetComponent<PassiveItem>();

            AddPassiveItem(slotIndex, passiveItemComponent);
            Destroy(passiveItem.gameObject);

            passiveItemLevels[slotIndex] = passiveItemComponent.passiveItemData.Level;
        }
    }
}