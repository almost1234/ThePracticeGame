using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public string name;
    public int attack;
    public int defense;
    public int speed;
    public int health;
    public string entityType;

    public EntityStat properties;
    private void Awake()
    {
        properties = new EntityStat(name,attack, defense, speed, health, entityType);
    }
}

public struct EntityStat 
{
    public string name;
    public int attack;
    public int defense;
    public int speed;
    public int health;
    public string entityType;

    public EntityStat(string name,int attack, int defense, int speed, int health, string entityType) 
    {
        this.name = name;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
        this.health = health;
        this.entityType = entityType;
    }
}
