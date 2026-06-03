using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotUI : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public Image iconImage;
    public Text amountText;
    private InventorySlot slot;
    public void Setup(InventorySlot slot)
    {
        this.slot = slot;
        iconImage.sprite = slot.weapon.icon;
        amountText.text = slot.amount > 1 ? slot.amount.ToString() : "";

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.clickCount == 2)
        {

        }
    }
    private void EquipWeapon()
    {
        Transform socket = GameObject.FindGameObjectWithTag("Player").transform.Find("WeaponSocket");
        if(socket == null)
        {
            Debug.Log("未找到武器点");
            return;
        }
        foreach (Transform child in socket) Destroy(child.gameObject);
        GameObject model = Instantiate(slot.weapon.modelPrefab, socket);
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        CharacterStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        if(playerStats !=null)
        {
            playerStats.EquipWeapon(slot.weapon);
            Debug.Log("装备武器" + slot.weapon.weaponName);
        }
    }
}
