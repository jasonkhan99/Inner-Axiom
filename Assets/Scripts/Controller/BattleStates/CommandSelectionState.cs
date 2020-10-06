using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectionState : BaseAbilityMenuState 
{
    protected override void LoadMenu ()
    {
        if (menuOptions == null)
        {
            menuTitle = "Commands";
            menuOptions = new List<string>(3);
            menuOptions.Add("Move");
            menuOptions.Add("Action");
            menuOptions.Add("Wait");
        }
        owner.abilityMenuPanelController.Show(menuTitle, menuOptions);
        owner.abilityMenuPanelController.SetLocked(0, owner.turn.hasUnitMoved);
        owner.abilityMenuPanelController.SetLocked(1, owner.turn.hasUnitActed);
    }

    protected override void Confirm ()
    {
        switch (owner.abilityMenuPanelController.selection)
        {
            case 0: // Move
                owner.ChangeState<MoveTargetState>();
                break;
            case 1: // Action
                owner.ChangeState<CategorySelectionState>();
                break;
            case 2: // Wait
                owner.ChangeState<SelectUnitState>();
                break;
        }
    }

    protected override void Cancel ()
    {
        if (owner.turn.hasUnitMoved && !owner.turn.lockMove)
        {
            owner.turn.UndoMove();
            owner.abilityMenuPanelController.SetLocked(0, false);
            SelectTile(owner.turn.actor.tile.pos);
        }
        else
        {
            owner.ChangeState<ExploreState>();
        }
    }
}
