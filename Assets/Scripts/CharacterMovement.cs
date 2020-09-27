using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;
    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;
    public int move = 5;
    public float jumpHeight = 1;
    public float moveSpeed = 2;
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

    public Tile GetTargetTile(GameObject targetTile)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(targetTile.transform.position, -Vector3.up, out hit, 1))
        {
            tiles = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

}
