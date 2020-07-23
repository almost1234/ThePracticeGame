using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Battlefield : MonoBehaviour
{
    public Transform parent;
    public static Dictionary<string, EntityBase> entityData; 
    public Dictionary<string, EntityBase> someData;
    public static List<EntityBase> turnList; //this will create a spaghetti code if u go back just to access the data again
                                               //Try ask if I need to create a specific code to store all values for referencing purposes
    public AttackUI enemyList;
    public VictoryChecker victory;

    public static bool nextTurn; // make an event for more secure flow?
    public bool waiting;

    void Awake()
    {
        entityData = CreateEntityList(parent);
        someData = entityData;
        turnList = SpeedSort(entityData); // need to find a better way for this
        nextTurn = false; //a way to indicate change of turn after attacking
        waiting = false; // a way to check for action taken
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (nextTurn == false && waiting == false)
        {
            //Pop out the attack mechanism here
            Debug.Log("Now it was " + turnList[0].name + " turn");
            waiting = true;
            //Attack Action flow : Generate attack list (Create a dictionary for referencing/calling purposes -> 
            // to provide data regarding attack multiplier + element) -> generate target list (if required) -> call 
            enemyList.GenerateAttack(turnList[0].attackList); // this is a poor way to get the attackList, need to learn how to use interface/ struct idk
            enemyList.SetEntityName();
        }

        else if (nextTurn) 
        {
            //This area use the victory check and rotation
            
            turnList.Add(turnList[0]);
            turnList.Remove(turnList[0]);
            switch (victory.Check(turnList))
            {
                case 0:
                    break;
                case 1:
                    Time.timeScale = 0f;
                    Debug.Log("Player Win");
                    break;
                case 2:
                    Time.timeScale = 0f;
                    Debug.Log("Enemy Win");
                    break;
            }
            nextTurn = false;
            waiting = false;
        }
    }

    public Dictionary<string, EntityBase> CreateEntityList(Transform players) 
    {
        Dictionary<string, EntityBase> baseList = new Dictionary<string, EntityBase>();
        foreach (Transform player in players)
        {
            EntityBase data = player.GetComponent<EntityBase>();
            baseList.Add(data.name, data);

        }
        Debug.Log("Base data generated!");
        return baseList;
    }
    public List<EntityBase> SpeedSort(Dictionary<string, EntityBase> baseList)  
    {
        Dictionary<EntityBase, int> playerList = new Dictionary<EntityBase,int>(); //what a poor way to sort it
        foreach (EntityBase data in baseList.Values) 
        {
            playerList.Add(data, data.speed);
        }

        var something = from pair in playerList orderby pair.Value descending select pair; //maybe learning linq?
        List<EntityBase> dataList = new List<EntityBase>();
        foreach (KeyValuePair<EntityBase, int> entity in something) 
        {
            dataList.Add(entity.Key);
        }
        Debug.Log("Turn data generated!");
        return dataList;
 
        /*foreach (KeyValuePair<string, int> sort in something) 
        {
            Debug.Log(sort.Key);
        }*/
    }
}
