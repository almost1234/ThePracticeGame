using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyboardControlSwitch 
{
    attack,target
}
public class KeyboardControl : MonoBehaviour
{
    //This script will control the keyboard behaviour in certain situatioins.
    //Since I have no clue on how to create this function initially, I will do it at the best of my ability
    //At this point I can already feel the level of poor management
    public static KeyboardControlSwitch state;
    public AttackUI attackUI;

    private void FixedUpdate()
    {
        switch (state) 
        {
            case KeyboardControlSwitch.attack:
                if (Input.GetKeyDown("up"))
                {
                    attackUI.OptionValueChange(-2);
                }
                else if (Input.GetKeyDown("down"))
                {
                    attackUI.OptionValueChange(2);
                }
                else if (Input.GetKeyDown("left"))
                {
                    attackUI.OptionValueChange(-1);
                }
                else if (Input.GetKeyDown("right"))
                {
                    attackUI.OptionValueChange(1);
                }
                else if (Input.GetKeyDown("x")) 
                {
                    attackUI.AttackUITransition();
                }
                break;
            case KeyboardControlSwitch.target:
                break;
        }
    }

    public void UIControl() 
    {
        //insert switching and accepting control
    }

}
