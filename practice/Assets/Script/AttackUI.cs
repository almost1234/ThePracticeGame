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

    //UPDATED FLOW
    //Call the function that creates the attack list, based on 2 different functions ->
    //1. If single attack, call the enemy generation
    //2. If wide attack, ignore enemy generation and call the wide attack (Idk if I should create a seperate "wide" attack when i could dynamic from AttackSystem.Attack())
    //The generation of these attack will be based on the struct/ interface that I will make. Since I could not do anything complicated, using them as a key
    //to call the Move is the best way I can make, and reduce spaghetti code
    //TODO: Reminder that there is random attack too

    //Maybe I should add a one function attack sequence call
    public Button enemyButton;
    public Button attackButton;
    public Text attackButtonText;
    public Text enemyName;
    public DataManager dataManager;

    public void Start()
    {
        attackButtonText = attackButton.GetComponentInChildren<Text>();
    }
    public void SetEntityName() 
    {
        enemyName.text = Battlefield.turnList[0].name;
    }

    public void GenerateAttack(List<string> attackList) 
    {
        DumbFunction<int, int>.DestroyAllChild(transform);
        foreach (string attackName in attackList) 
        {
            AttackData attackData = dataManager.GetAttackData(attackName);
            attackButtonText.text = attackName;
            switch (attackData.attackType) 
            {
                
                case 0:
                    Instantiate(attackButton, transform).onClick.AddListener(delegate {
                        Debug.Log("ENTERING SINGLE TARGET ATTACK");
                        GenerateEnemy(AttackSystem.EnemyList(Battlefield.turnList), attackName);});
                    Debug.Log("Single target generated");
                    break;
                case 1:
                    Instantiate(attackButton, transform).onClick.AddListener(delegate {
                        Debug.Log("ENTERING ALL ROUND ATTACK");
                        foreach (EntityBase target in AttackSystem.EnemyList(Battlefield.turnList).Values) // the turnlist will be changed with the enemyList in the future 
                        {
                            ToAttack(target, attackName);// attack all
                        }
                    });
                    Debug.Log("All attack target generated");
                    break;
                case -1:
                    Debug.Log("Invalid attack");
                    break;
            }
        }
        Debug.Log("Attack Generation done");
    }
    public void GenerateEnemy(Dictionary<string, EntityBase> enemyList, string attackName) 
    {
        DumbFunction<int, int>.DestroyAllChild(transform);
        Text buttonText = enemyButton.GetComponentInChildren<Text>();
        foreach (KeyValuePair<string, EntityBase> data in enemyList) 
        {
            buttonText.text = data.Key;
            Button attackTarget = Instantiate(enemyButton, transform);
            attackTarget.onClick.AddListener(delegate { ToAttack(data.Value, attackName); });
        }
    }

    public void ToAttack(EntityBase target, string attackName) 
    {
        AttackSystem.Attack(Battlefield.turnList[0].attack, target);
        Battlefield.nextTurn = true;
    }
}
