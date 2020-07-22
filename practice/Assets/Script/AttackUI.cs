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

    public Button enemyButton;
    public Text enemyName;
    private void Start()
    {
        //enemySelector = gameObject.GetComponent<Dropdown>();
        
    }

    public void SetEntityName() 
    {
        enemyName.text = Battlefield.turnList[0].name;
    }

    public void GenerateEnemy(Dictionary<string, EntityBase> enemyList) 
    {
        DumbFunction<int, int>.DestroyAllChild(transform);
        Text buttonText = enemyButton.GetComponentInChildren<Text>();
        foreach (KeyValuePair<string, EntityBase> data in enemyList) 
        {
            buttonText.text = data.Key;
            Button attackTarget = Instantiate(enemyButton, transform);
            attackTarget.onClick.AddListener(delegate { ValueClicked(data.Value); });
        }
    }

    public void ValueClicked(EntityBase target) 
    {
        AttackSystem.Attack(Battlefield.turnList[0].attack, target);
        Battlefield.nextTurn = true;
    }
}
