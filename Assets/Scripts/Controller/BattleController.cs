using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleController : StateMachine 
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;

    public GameObject heroPrefab;
    public Tile currentTile { get { return board.GetTile(pos); }}

    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; }}
    public Turn turn { get { return owner.turn; }}
    public List<Unit> units { get { return owner.units; }}

    void Start ()
    {
        ChangeState<InitBattleState>();
    }
}