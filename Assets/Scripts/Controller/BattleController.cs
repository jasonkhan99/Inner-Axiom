using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleController : StateMachine 
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;

    public GameObject heroPrefab;
    public Tile currentTile { get { return board.GetTile(pos); }}

    public AbilityMenuPanelController abilityMenuPanelController { get { return abilityMenuPanelController; }}
    public Turn turn { get { return turn; }}
    public List<Unit> units { get { return units; }}

    void Start ()
    {
        ChangeState<InitBattleState>();
    }
}