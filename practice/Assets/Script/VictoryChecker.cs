using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum victoryState //im still not used to enum
{
    battle,
    playerWin,
    enemyWin,
}
public class VictoryChecker : MonoBehaviour
{
    // While victory checkers are generally checking for the existence of enemy, there are various victory check
    // I still have no clue tbh to make those kinds

    public int Check(List<EntityBase> entityAlive) //this is way too hardcoded, need better fixing
    {
        bool player = false;
        bool enemy = false;
        foreach (EntityBase entity in entityAlive) 
        {
            if (entity.entityType == "Player")
            {
                player = true;
            }

            else 
            {
                enemy = true;
            }
        }

        if (player == true && enemy == true)
        {
            return (int)victoryState.battle;
        }

        else if (player == true) 
        {
            return (int)victoryState.playerWin;
        }

        return (int)victoryState.enemyWin;
    }
}
