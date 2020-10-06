using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ActionSelectionState : BaseAbilityMenuState 
{
    public static int category;
    string[] whiteMagicOptions = new string[] { "Cure", "Raise", "Holy" };
    string[] blackMagicOptions = new string[] { "Fire", "Ice", "Lightning" };

    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuOptions = new List<string>(3);
        }
        if (category == 0)
        {
            menuTitle = "White Magic";
            SetOptions(whiteMagicOptions);
        }
        else
        {
            menuTitle = "Black Magic";
            SetOptions(blackMagicOptions);
        }
        abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    protected override void Confirm ()
    {
        turn.hasUnitActed = true;
        if (turn.hasUnitMoved)
        {
            turn.lockMove = true;
        }
        owner.ChangeState<CommandSelectionState>();
    }

    protected override void Cancel ()
    {
        owner.ChangeState<CategorySelectionState>();
    }

    void SetOptions (string[] options)
    {
        menuOptions.Clear();
        for (int i = 0; i < options.Length; ++i)
        {
            menuOptions.Add(options[i]);
        }
    }
}