using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CategorySelectionState : BaseAbilityMenuState 
{
    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuTitle = "Action";
            menuOptions = new List<string>(3);
            menuOptions.Add("Attack");
            menuOptions.Add("White Magic");
            menuOptions.Add("Black Magic");
        }
        
        owner.abilityMenuPanelController.Show(menuTitle, menuOptions);
    }

    protected override void Confirm ()
    {
        switch (owner.abilityMenuPanelController.selection)
        {
            case 0:
                Attack();
                break;
            case 1:
                SetCategory(0);
                break;
            case 2:
                SetCategory(1);
                break;
        }
    }

    protected override void Cancel ()
    {
        owner.ChangeState<CommandSelectionState>();
    }

    void Attack ()
    {
        owner.turn.hasUnitActed = true;
        if (owner.turn.hasUnitMoved)
        {
            owner.turn.lockMove = true;
        }
        owner.ChangeState<CommandSelectionState>();
    }

    void SetCategory (int index)
    {
        ActionSelectionState.category = index;
        owner.ChangeState<ActionSelectionState>();
    }
}

