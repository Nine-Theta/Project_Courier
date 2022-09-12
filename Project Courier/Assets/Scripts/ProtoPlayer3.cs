using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoPlayer3 : MonoBehaviour
{
    public enum Directions { NORTH, EAST, SOUTH, WEST, NONE };

    [SerializeField]
    private int _tileSize;
    [SerializeField]
    private float _moveSpeed;

    private bool[] _dirPressed =  new bool[] { false, false, false, false };

    private Vector2 _playerPos;
    private Vector2 _moveTargetPos;
    private Vector2 _moveDelta;
    private Vector2 _moveDirection;

    private Directions _moveDir = Directions.NONE;
    private Directions _nextMoveDir = Directions.NONE;

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

    public void OnMove(InputValue pMoveVec)
    {
        Vector2 vec = pMoveVec.Get<Vector2>();

        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            _moveDirection = new Vector2(Mathf.RoundToInt(vec.x), 0);
        }
        else
        {
            _moveDirection = new Vector2(0, Mathf.RoundToInt(vec.y));
        }

        Debug.Log("Movee: " + _moveDirection);

    }

    private void Start()
    {
        _playerPos = transform.position;
        _moveTargetPos = _playerPos;
    }

    private void Update()
    {
        if ((_moveTargetPos - _playerPos).sqrMagnitude <= 0.01f)
        {
            _playerPos = _moveTargetPos;
            SetMoveDir();
        }

        _playerPos += _moveDelta;
        transform.position = _playerPos;
    }

    private void SetMoveDir()
    {

        //float x = _playerPos.x;
        //float y = _playerPos.y;

        _moveTargetPos = _playerPos + _moveDirection;
       _moveDelta = (_moveDirection) * _moveSpeed;

        Debug.Log(_moveDelta);
        /**
        if (_nextMoveDir == Directions.NONE)
        {
            if (_moveDir == Directions.NONE || !_dirPressed[(int)_moveDir])
            {
                for (int i = 0; i < 4; i++)
                {
                    if (_dirPressed[i])
                    {
                        _moveDir = (Directions)i;
                        break;
                    }
                    _moveDir = Directions.NONE;
                }
            }
        }
        else
        {
            _moveDir = _nextMoveDir;
            _nextMoveDir = Directions.NONE;
        }

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

        _moveTargetPos = new Vector2((int)x, (int)y);

        _moveDelta = (_moveTargetPos - _playerPos) * _moveSpeed;
        */

        //Console.Beep();
    }
}
