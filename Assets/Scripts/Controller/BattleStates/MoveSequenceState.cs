using UnityEngine;
using System.Collections;
public class MoveSequenceState : BattleState 
{
    public override void Enter ()
    {
        base.Enter ();
        StartCoroutine("Sequence");
    }
    
    IEnumerator Sequence ()
    {
        Movement m = owner.GetComponent<Movement>();
        yield return StartCoroutine(m.Traverse(owner.currentTile));
        owner.ChangeState<SelectUnitState>();
    }
}
