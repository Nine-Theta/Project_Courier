using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Obsolete]
public class ProtoPlayer : MonoBehaviour
{
    public enum Directions { NONE, NORTH, EAST, SOUTH, WEST };

    [SerializeField]
    private int _tileSize;
    [SerializeField]
    private float _moveSpeed;


    private Vector2 _playerPos;
    private Vector2 _moveTargetPos;
    private Vector2 _moveDelta;

    private Directions _moveDir;
    private Directions _nextMoveDir;

    
    public Directions SetNextTargetDir
    {
        set
        {
            bool isOpposite = false;
            switch (value)
            {               
                case Directions.NORTH:
                    if (_moveDir == Directions.SOUTH)
                        isOpposite = true;
                    break;
                case Directions.EAST:
                    if (_moveDir == Directions.WEST)
                        isOpposite = true;
                    break;
                case Directions.SOUTH:
                    if (_moveDir == Directions.NORTH)
                        isOpposite = true;
                    break;
                case Directions.WEST:
                    if (_moveDir == Directions.EAST)
                        isOpposite = true;
                    break;
                default:
                    break;
            }

            _nextMoveDir = value;

           

            if (isOpposite)
                SetMoveDir();            
        }
    }

    public void OnMove(InputValue movementValue)
    {
        Directions dir = Directions.NONE;
    
        if(movementValue.Get<Vector2>().y >= 0.5f)
        {
            dir = Directions.NORTH;
        }
        else if (movementValue.Get<Vector2>().y <= -0.5f)
        {
            dir = Directions.SOUTH;
        }
        else if (movementValue.Get<Vector2>().x >= 0.5f)
        {
            dir = Directions.EAST;
        }
        else if (movementValue.Get<Vector2>().x <= -0.5f)
        {
            dir = Directions.WEST;
        }

        SetNextTargetDir = dir;

        //Debug.Log("beep " + movementValue.Get<Vector2>());
    }

    private void Start()
    {
        _playerPos = transform.position;
        _moveTargetPos = _playerPos;
    }

    private void Update()
    {
        if (_moveTargetPos == _playerPos)
        {
            SetMoveDir();
        }

        _playerPos += _moveDelta;
        transform.position = _playerPos;
    }

    private void SetMoveDir()
    {
        _moveDir = _nextMoveDir;
        _nextMoveDir = Directions.NONE;

        float x = _playerPos.x;
        float y = _playerPos.y;

        switch (_moveDir)
        {
            case Directions.NONE:
                break;
            case Directions.NORTH:
                y += _tileSize;
                break;
            case Directions.EAST:
                x += _tileSize;
                break;
            case Directions.SOUTH:
                y -= _tileSize;
                break;
            case Directions.WEST:
                x -= _tileSize;
                break;
            default:
                break;
        }

        _moveTargetPos = new Vector2(x, y);

        _moveDelta = (_moveTargetPos - _playerPos) * _moveSpeed;

        Debug.Log("beep " + _moveDelta);
    }
}
