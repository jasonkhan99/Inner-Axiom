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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTile)
        {
            
        }
    }

    public void Reset()
    {
        tileAdjacencyList.Clear();
        currentTile = false;
        targetTile = false;
        selectableTile = false;
        tileDistance = 0;
    }
}
