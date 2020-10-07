using UnityEngine;
using System.Collections;

public class SelectUnitState : BattleState 
{
	int index = -1;

	public override void Enter ()
	{
		base.Enter ();
		StartCoroutine("ChangeCurrentUnit");
	}

	public override void Exit ()
	{
		base.Exit ();
		statPanelController.HidePrimary();
	}

	IEnumerator ChangeCurrentUnit ()
	{
		index = (index + 1) % units.Count;
		turn.Change(units[index]);
		RefreshPrimaryStatPanel(pos);
		yield return null;
		owner.ChangeState<CommandSelectionState>();
	}
}