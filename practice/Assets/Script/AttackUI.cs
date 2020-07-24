using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Transactions;
using JetBrains.Annotations;

public enum UIState 
{
    attack,target
}
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

    //UPDATED UI FLOW
    //Since I will create a textbox kind system, I need to revamp/ create another way to call the attack.
    //It is possibe that the previous function will be removed, depending how fast I can do
    //In each new turn, call an Image with a textbox on it. The textbox will serve as a string name
    //That will link to the DataManager.attackList for the kind of attack it will perform
    //The AttackData will provide AttackData.attackType which indicate what kind of UI needed to be generated
    //UI will be updated if any input is given. This should trigger the re-render of something (Attack or Target)
    //As such, there may be a revamp on flow in terms of what needs to be re-render (Maybe save the name idk?)
    //This improvement(?) creates a big change on the GenerateAttack() and GenerateTarget() as I do not need that anymore


    //There will be 2 Data Save, List<string> savedData to save the chosen move and target before sending to the ToAttack
    //List<string> choiceData is the list of choice showing on the UI side for data and rendering 
    //purposes. All of this should be called via a delegate/event combo
    //There will be an enum state machine that control what should be rendered etc
    public Button enemyButton;
    public Button attackButton;
    public Text attackButtonText;
    public Text enemyName;

    public DataManager dataManager;

    public GameObject optionGroup;
    public GameObject option;
    public Text optionText;
    public Image optionImage;
    public UIState uiState;
    public int optionValue;
    public List<string> savedData;
    public List<string> choiceData;


    public void Start()
    {
        attackButtonText = attackButton.GetComponentInChildren<Text>();
        uiState = UIState.attack;
        optionImage = option.GetComponent<Image>();
        optionText = option.GetComponentInChildren<Text>();
    }
    public void SetEntityName() 
    {
        enemyName.text = Battlefield.turnList[0].name;
    }

    /*
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
    }*/

    public void RenderOption(List<string> optionList) //maybe create a list for image so that it only render the colour
    {
        DumbFunction<int, int>.DestroyAllChild(transform);
        Transform transformGroup = transform;
        choiceData = optionList;
        for (int i = 0; i < optionList.Count(); i++) //ppl said it has optimization problem
        {
            if (i % 2 == 0) 
            {
                transformGroup = Instantiate(optionGroup, transform).GetComponent <Transform>();
            }
            optionImage.color = optionValue == i ? Color.red : Color.white;
            optionText.text = optionList[i];
            Instantiate(option, transformGroup);
        }
    }
    public void ToAttack(EntityBase target, string attackName) 
    {
        AttackSystem.Attack(Battlefield.turnList[0].attack, target);
        Battlefield.nextTurn = true;
    }

    public void OptionValueChange(int change) //This way is extremely poor due to the amount of remaking, find better way
    {
        if ((optionValue + change) >= 0 && (optionValue + change) <= choiceData.Count()) 
        {
            optionValue += change;
            Debug.Log("NOW VALUE IS " + optionValue.ToString());
            RenderOption(choiceData);
        }
    }

    public void AttackUITransition() 
    {
        DumbFunction<int, int>.DestroyAllChild(transform);
        switch (dataManager.GetAttackData(choiceData[optionValue]).attackType) 
        {
            case 0:
                //single
                RenderOption(DumbFunction<string, EntityBase>.GetKeys(Battlefield.entityData));
                savedData.Add(choiceData[optionValue]);
                Debug.Log(savedData[0] + " was saved!");
                break;
            case 1:
                //multi
                foreach (EntityBase target in Battlefield.turnList) 
                {
                    ToAttack(target, choiceData[optionValue]);
                }
                break;
        }
    }
}
