using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New Attack",menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
    public float attackRange;
    public float skillRange;
    public float cooldown;
    public int minDamage;
    public int maxDamage;
    public float criticalMultiplier;
    public float criticalChance;

    public void ApplyWeaponData(AttackData_SO weapon)
    {
        attackRange = weapon.attackRange;
        skillRange = weapon.skillRange;
        cooldown = weapon.cooldown;
        minDamage = weapon.minDamage;
        maxDamage = weapon.maxDamage;
        criticalMultiplier = weapon.criticalMultiplier;
        criticalChance = weapon.criticalChance;
    }
}
