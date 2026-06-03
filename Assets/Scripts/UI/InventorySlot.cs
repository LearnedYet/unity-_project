using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InventorySlot
{
    // Start is called before the first frame update
    public WeaponData weapon;
    public int amount;
    public InventorySlot(WeaponData weapon, int amount)
    {
        this.weapon = weapon;
        this.amount = amount;
    }
}
