using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollideableTile : Tile
{
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public Transform GetTransform()
    {
        return gameObject.transform;
    }
}
