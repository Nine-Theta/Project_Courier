using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProtoPlayer2 : MonoBehaviour
{
    public enum Directions { NONE, NORTH, EAST, SOUTH, WEST };

    [SerializeField]
    private int _tileSize;
    [SerializeField]
    private float _moveSpeed;

    bool move;

    private Vector2 _playerPos;
    private Vector2 _moveTargetPos;
    private Vector2 _moveDelta;

    private Directions _moveDir;
    private Directions _nextMoveDir;

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


    public void OnMoveNorth(InputValue g)
    {
        Debug.Log("g");
        Debug.Log(g.isPressed);
        SetNextTargetDir = Directions.NORTH;
    }
    public void OnMoveEast()
    {
        SetNextTargetDir = Directions.EAST;
    }
    public void OnMoveSouth()
    {
        SetNextTargetDir = Directions.SOUTH;
    }
    public void OnMoveWest()
    {
        SetNextTargetDir = Directions.WEST;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.MoveNorth.performed += ctx => SendMessage(ctx);
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
