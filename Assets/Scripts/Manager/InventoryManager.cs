using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<InventorySlot> items = new List<InventorySlot>();
    public Transform slotParent;
    public GameObject slotPrefab;
    public void AddItem(WeaponData weapon,int amount = 1)
    {
        var slot = items.Find(s => s.weapon == weapon);
        if (slot != null) slot.amount += amount;
        else items.Add(new InventorySlot(weapon, amount));
        
    }
    // Start is called before the first frame update
    public void RemoveItem(WeaponData weapon,int amount = 1)
    {
        var slot = items.Find(s => s.weapon == weapon);
        if (slot == null) return;
        slot.amount -= amount;
        if (slot.amount <= 0) items.Remove(slot);

    }
    void RefreshUI()
    {
        foreach (Transform child in slotParent) Destroy(child.gameObject);
        foreach (var slot in items)
        {
            GameObject obj = Instantiate(slotPrefab, slotParent);
            obj.GetComponent<SlotUI>().Setup(slot);
        }
    }
   
}
