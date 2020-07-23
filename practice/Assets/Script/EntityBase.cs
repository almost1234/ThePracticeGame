using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public string entityName;
    public int attack;
    public int defense;
    public int speed;
    public int health;
    public string entityType;

    public EntityStat properties;
    public List<string> attackList;
    private void Awake()
    {
        properties = new EntityStat(entityName,attack, defense, speed, health, entityType);
    }
}

public struct EntityStat 
{
    public string entityName;
    public int attack;
    public int defense;
    public int speed;
    public int health;
    public string entityType;

    public EntityStat(string entityName,int attack, int defense, int speed, int health, string entityType) 
    {
        this.entityName = entityName;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.health = health;
        this.entityType = entityType;
    }
}
