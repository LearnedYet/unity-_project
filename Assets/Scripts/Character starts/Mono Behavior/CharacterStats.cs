using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public event Action<int, int> UpdateHealthBarOnAttack;
    public CharacterData_SO templateData;
    public CharacterData_SO characterData;
    public AttactData_SO attactData;
    public WeaponData currentWeapon;
    [HideInInspector]
    public bool isCritcal;
    #region Read from Data_SO


    private void Awake()
    {
        if (templateData != null)
            characterData = Instantiate(templateData);
    }
    public int MaxHealth 
    {   
        get{ if (characterData != null) return characterData.maxHealth;else return 0; }
        set{ characterData.maxHealth = value;}
    }
    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth; else return 0; }
        set { characterData.currentHealth = value; }
    }
    public int BaseDefence
    {
        get { if (characterData != null) return characterData.baseDefence; else return 0; }
        set { characterData.baseDefence = value; }
    }
    public int CurrentDefence
    {
        get { if (characterData != null) return characterData.currentDefence; else return 0; }
        set { characterData.currentDefence = value; }
    }
    #endregion

    #region Character Combat
    public void EquipWeapon(WeaponData weapon)
    {
        if (weapon == null) return;
        currentWeapon = weapon;
        Debug.Log("×°±¸ÎäÆ÷:" + weapon.weaponName+"£¬¸½¼ÓÉËº¦:" + weapon.damageMin +"-"+weapon.damageMax+"£¬±©»÷¼Ó³É:"+weapon.criticalChance);
    }
    public void TakeDamage(CharacterStats attacker,CharacterStats defener)
    {
        int damage = Mathf.Max(attacker.CurrentDamage() - defener.CurrentDefence,0);
        CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
        if(attacker.isCritcal)
        {
            defener.GetComponent<Animator>().SetTrigger("Hit");
        }
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
            attacker.characterData.UpdateExp(characterData.killPoint);

    }
    public void TakeDamage(int damage,CharacterStats defener)
    {
        int currentDamge = Mathf.Max(damage - defener.CurrentDefence, 0);
        CurrentHealth = Mathf.Max(CurrentHealth - currentDamge, 0);
        UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
        if (CurrentHealth <= 0)
            GameManager.Instance.playerStats.characterData.UpdateExp(characterData.killPoint);
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attactData.minDamge, attactData.maxDamage);
        if(currentWeapon != null)
        {
            float weaponDamge = UnityEngine.Random.Range(currentWeapon.damageMin, currentWeapon.damageMax);
        }
        float totalCritChance = attactData.criticalChance;
        if (currentWeapon != null)
            totalCritChance += currentWeapon.criticalChance;
        bool isCrit = UnityEngine.Random.value < totalCritChance;
        if(isCrit)
        {
            coreDamage *= attactData.criticalMultiplier;
            Debug.Log("±©»÷£¡" + coreDamage);
            isCritcal = true;
        }
        else
        {
            isCritcal = false;
        }
        return (int)coreDamage;
    }


    #endregion
}
