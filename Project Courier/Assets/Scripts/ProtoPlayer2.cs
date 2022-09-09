using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoPlayer2 : MonoBehaviour
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

    private Directions _moveDir = Directions.NONE;
    private Directions _nextMoveDir = Directions.NONE;

    private PlayerControls controls;

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

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.MoveNorth.performed += ctx => SendMessage(ctx);
        controls.Player.MoveNorth.performed += ctx => UpdateMoveInput(Directions.NORTH, ctx.ReadValueAsButton());
        controls.Player.MoveEast.performed += ctx => UpdateMoveInput(Directions.EAST, ctx.ReadValueAsButton());
        controls.Player.MoveSouth.performed += ctx => UpdateMoveInput(Directions.SOUTH, ctx.ReadValueAsButton());
        controls.Player.MoveWest.performed += ctx => UpdateMoveInput(Directions.WEST, ctx.ReadValueAsButton());

    }

    private void UpdateMoveInput(Directions pDir, bool pState)
    {
        _dirPressed[(int)pDir] = pState;

        if (pState)
            SetNextTargetDir = pDir;
        else
            _nextMoveDir = Directions.NONE;

    }

    private void SendMessage(InputAction.CallbackContext ctx)
    {
        Debug.Log("North +" + ctx.ReadValueAsButton());
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
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

        Debug.Log("beep " + _moveDelta);
        Console.Beep();
    }
}
