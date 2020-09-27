using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;
    public int move = 5;
    public float jumpHeight = 1;
    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

}
