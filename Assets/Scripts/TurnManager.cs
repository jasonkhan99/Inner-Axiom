using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<CharacterMovement>> units = new Dictionary<string, List<CharacterMovement>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<CharacterMove> turnTeam = new Queue<CharacterMove>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    static void InitTeamTurnQueue()
    {
        List<CharacterMove> teamList = units[turnKey.Peek()];
        
        foreach (CharacterMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }
    }
}
