using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public int move = 5;
    public float jumpHeight = 1;
    public float moveSpeed = 2;
    public float jumpVelocity = 4.5f;

    float halfHeight = 0;
    
    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingToEdge = false;
    Vector3 jumpTarget;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.currentTile = true;
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeTileAdjacencyList()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighborTiles(jumpHeight);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeTileAdjacencyList();
        GetCurrentTile();
        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visitedTile = true;
        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            selectableTiles.Add(t);
            t.selectableTile = true;
            if (t.tileDistance < move)
            {
                foreach (Tile tile in t.tileAdjacencyList)
                {
                    if (!tile.visitedTile)
                    {
                        tile.parentTile = t;
                        tile.visitedTile = true;
                        tile.tileDistance = 1 + t.tileDistance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.targetTile = true;
        moving = true;

        Tile nextTile = tile;
        while (nextTile != null)
        {
            path.Push(nextTile);
            nextTile = nextTile.parentTile;
        }
    }

    public void CharacterMove()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            //Calculate the unit's position on top of the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;
            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;
                if (jump)
                {
                    CharacterJump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }
                //locomotion + animation
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.currentTile = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }
        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void CharacterJump(Vector3 target)
    {
        if (fallingDown)
        {
            FallDownward(target);
        }
        else if (jumpingUp)
        {
            JumpUpward(target);
        }
        else if (movingToEdge)
        {
            MoveToEdge();
        }
        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingToEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2.0f;
        }
        else
        {
            fallingDown = false;
            jumpingUp = true;
            movingToEdge = false;

            velocity = heading * moveSpeed / 3.0f;

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
        }
        
    }

    void FallDownward(Vector3 target)
    {

    }

    void JumpUpward(Vector3 target)
    {

    }

    void MoveToEdge()
    {

    }

}
