using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    //This area will be a place for data collection. How I will update/ create will still be difficult for me
    //To prevent any data changes from outside, I will create a static function just for the purpose of calling the needed information
    //from the attack, instead of making the baseData a static variable 
    //EDIT I completely forgot that static varaible doesnt exist anywhere

    //In the future, If I wanted to make a flexible one, creating a data using textFile will be suitable.

    private Dictionary<string, AttackData> attackData = new Dictionary<string, AttackData>() 
    {
        {"BasicAttack", new AttackData(0,2, ElementType.fire)},
        {"AllAttack", new AttackData(1,1, ElementType.fire)},
        {"AllAttack2", new AttackData(1,2, ElementType.nothing) }
    };

    public AttackData GetAttackData(string attackName) 
    {
        if (attackData.ContainsKey(attackName)) 
        {
            return attackData[attackName];
        }
        return new AttackData(-1, 0, ElementType.nothing);
    }
}

public struct AttackData 
{
    public int attackType;
    public int attackModifier;
    public ElementType attackElement;
    //element etc will be added in the future

    public AttackData(int attackType, int attackModifier, ElementType elementType) 
    {
        this.attackType = attackType;
        this.attackModifier = attackModifier;
        this.attackElement = elementType;
    }
    
}

public enum ElementType 
{
    nothing,fire,water,earth,wind
}
