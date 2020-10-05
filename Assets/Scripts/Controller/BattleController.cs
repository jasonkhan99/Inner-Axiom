using UnityEngine;
using System.Collections;
public class BattleController : StateMachine 
{
    public CameraRig cameraRig;
    public Board board;
    public LevelData levelData;
    public Transform tileSelectionIndicator;
    public Point pos;
    void Start ()
    {
        ChangeState<InitBattleState>();
    }
}