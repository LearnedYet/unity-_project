using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="NewWeapon",menuName ="RPG/Weapon")]
public class WeaponData : ScriptableObject
{
    // Start is called before the first frame update
    public string weaponName;
    public Sprite icon;
    public GameObject modelPrefab;
    public int damageMin = 10;
    public int damageMax = 20;
    public float criticalChance = 0.3f;
    public float critcalMultiplier = 1.5f;
    [TextArea] public string description;
}
