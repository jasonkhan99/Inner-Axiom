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

    void FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectWithTag("Player");

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float d = Vector3.distance(transform.position, obj.transform.position);
            
            if (d < distance)
            {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
