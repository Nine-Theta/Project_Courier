using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicStaticColliderHandler : MonoBehaviour
{
    [SerializeField]
    private int _tileSize = 1;

    [SerializeField]
    private Vector2 _innerArea = new Vector2(1, 1);

    private BoxCollider2D _northBox, _eastBox, _southBox, _westBox;

    [HideInInspector]
    public bool[] DirectionBoxes = new bool[4];

    private void Awake()
    {
        if (DirectionBoxes[0])
        {
            _northBox = gameObject.AddComponent<BoxCollider2D>();
            _northBox.size = new Vector2(_innerArea.x, 1);
            _northBox.offset = new Vector2(0, (_innerArea.y + _tileSize)*0.5f);
        }

        if (DirectionBoxes[1])
        {
            _eastBox = gameObject.AddComponent<BoxCollider2D>();
            _eastBox.size = new Vector2(1, _innerArea.y);
            _eastBox.offset = new Vector2((_innerArea.x + _tileSize) * 0.5f, 0);
        }

        if (DirectionBoxes[2])
        {
            _southBox = gameObject.AddComponent<BoxCollider2D>();
            _southBox.size = new Vector2(_innerArea.x, 1);
            _southBox.offset = new Vector2(0, (_innerArea.y + _tileSize) * -0.5f);
        }

        if (DirectionBoxes[3])
        {
            _westBox = gameObject.AddComponent<BoxCollider2D>();
            _westBox.size = new Vector2(1, _innerArea.y);
            _westBox.offset = new Vector2((_innerArea.x + _tileSize) * -0.5f, 0);
        }
    }
}

