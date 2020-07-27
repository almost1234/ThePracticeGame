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
    public ElementType[] weakness;
    public ElementType[] resistance;

    public EntityStat properties;
    public List<string> attackList;
    private void Awake()
    {
        properties = new EntityStat(entityName,attack, defense, speed, health, entityType, weakness, resistance);
    }
}

public struct EntityStat // this is getting worse
{
    public string entityName;
    public int attack;
    public int defense;
    public int speed;
    public int health;
    public string entityType;
    public ElementType[] weakness;
    public ElementType[] resistance;

    public EntityStat(string entityName, int attack, int defense, int speed, int health, string entityType, ElementType[] weakness, ElementType[] resistance) 
    {
        this.entityName = entityName;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.health = health;
        this.entityType = entityType;
        this.weakness = weakness;
        this.resistance = resistance;

    }
}
