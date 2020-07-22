using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackUI : MonoBehaviour
{
    //AttackUI flow might be something like this
    //1. When attack, generate the list of enemy  2. Choose the enemy  3.Attack
    //As such, the flow might be When attack is called, 
    //1. When chosen, give the attack data/ saved attack data on the AttackUI(?)/ create another step on attackSystem  
    //2. Generate the EnemyList, and when selected, transfer it to the attackSystem

    public Dropdown enemySelector;

    private void Start()
    {
        //enemySelector = gameObject.GetComponent<Dropdown>();
        enemySelector.onValueChanged.AddListener(delegate { ValueClicked(); }); //I really dont know why they do not want to accept single method
    }

    public void GenerateEnemy(Dictionary<string, EntityBase> enemyList) 
    {
        enemySelector.ClearOptions();
        enemySelector.AddOptions(DumbFunction<string,EntityBase>.GetKeys(enemyList));
    }

    public void ValueClicked() 
    {
        AttackSystem.Attack(Battlefield.turnList[0].attack, Battlefield.entityData[enemySelector.options[enemySelector.value].text]);
        Battlefield.nextTurn = true;
    }
}
