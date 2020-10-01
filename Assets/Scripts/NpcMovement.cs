using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : CharacterMovement
{
    GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }
        
        if (!moving)
        {
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();
        }
        else
        {
            CharacterMove();
        }
    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);

    }
}
