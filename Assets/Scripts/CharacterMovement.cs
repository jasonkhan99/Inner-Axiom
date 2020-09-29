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
    public float halfHeight = 0;
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
        t.targetTile = true;
        moving = true;

        Tile nextTile = tile;
        while (nextTile != null)
        {
            path.Push(next);
            nextTile = nextTile.parentTile;
        }
    }

}
