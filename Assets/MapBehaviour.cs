using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBehaviour : MonoBehaviour
{
    public Vector2 mapSize;
    public bool[,] map;

    void Start()
    {
        map = new bool[(int)mapSize.x, (int)mapSize.y];

        for (var childIndex = 0; childIndex < transform.childCount; childIndex++)
        {
            var child = transform.GetChild(childIndex);
            map[(int)child.position.x, (int)child.position.y] = true;
        }
    }
}
