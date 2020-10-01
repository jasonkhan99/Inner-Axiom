using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkableTile = true;
    public bool currentTile = false;
    public bool targetTile = false;
    public bool selectableTile = false;
    public List<Tile> tileAdjacencyList = new List<Tile>();
    public int tileDistance = 0;
    public bool visitedTile = false;
    public Tile parentTile = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTile)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        else if (targetTile)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else if (selectableTile)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void Reset()
    {
        tileAdjacencyList.Clear();
        currentTile = false;
        targetTile = false;
        selectableTile = false;
        parentTile = null;
        tileDistance = 0;
        visitedTile = false;
    }

    public void FindNeighborTiles(float jumpHeight, Tile target)
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(.25f, .25f + jumpHeight, .25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);
        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkableTile)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    tileAdjacencyList.Add(tile);
                }
            }
        }
    }


}
