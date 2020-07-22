﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    //AttackSystem is used to bridge the data flow of attack damage to health calculation
    //1. Get attack damage  2. Get the enemy targeted  3. alter the information

    //Is there too much static

    public static void Attack(int attackDamage, EntityBase target) 
    {
        target.health -= attackDamage;
        Debug.Log(target.name + " has received " + attackDamage.ToString());
        Debug.Log(target.name + " has " + target.health.ToString() + "left!");
        HealthCheck(target);
    }

    public static void AttackAll(int attackDamage, List<EntityBase> target) 
    {
        foreach (EntityBase enemy in target) 
        {
            Attack(attackDamage, enemy);
        }
    }

    public static void HealthCheck(EntityBase player) 
    {
        if (player.health <= 0) 
        {
            Battlefield.turnList.Remove(player);
            Debug.Log("Bitch " + player.name + "ded");
        }
    }

    public static Dictionary<string, EntityBase> EnemyList(EntityBase[] entityList) 
    {
        Dictionary<string, EntityBase> enemyList = new Dictionary<string, EntityBase>();
        foreach (EntityBase entity in entityList) 
        {
            /*if (entity.entityType == "Enemy") 
            {
                enemyList.Add(entity.name, entity);
            }*/
            enemyList.Add(entity.name, entity);
        }
        return enemyList;
    }


}