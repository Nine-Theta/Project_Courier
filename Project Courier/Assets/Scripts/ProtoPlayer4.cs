using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ProtoPlayer4 : MonoBehaviour
{
    [SerializeField]
    private float _baseMoveSpeed;
    [SerializeField] private float _moveSpeed;

    private bool _isBiking = false;

    private Vector2 _moveTargetPos;

    private Vector2 _targetDir;
    private Vector2 _nextTargetDir;

    private Rigidbody2D _playerBody;
    [SerializeField]
    private SpriteRenderer _playerSprite;
    //private GameObject _interactable;

    private static ProtoPlayer4 _playerInstance;

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();

        _moveTargetPos = transform.position;

        _moveSpeed = _baseMoveSpeed;

        if (_playerSprite == null)
            _playerSprite = GetComponentInChildren<SpriteRenderer>();

        if (_playerInstance == null)
        {
            _playerInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _nextTargetDir = Vector2.zero;
        SetMoveDir(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Terrain":
                {
                    TerrainManager terrain = other.GetComponent<TerrainManager>();
                    if (!_isBiking)
                    {
                        _moveSpeed = _baseMoveSpeed * terrain.WalkSpeedMultiplier;
                        //Debug.Log("terrain " + terrain.Terraintype + ": " + _moveSpeed + " = " + _baseMoveSpeed + " * " + terrain.WalkSpeedMultiplier);
                    }
                }
                break;

            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            default:
                break;
        }
    }

    public void OnMove(InputValue pMoveVec)
    {
        Vector2 vec = pMoveVec.Get<Vector2>();

        if (Mathf.Abs(vec.x) > Mathf.Abs(vec.y))
        {
            vec = new Vector2(Mathf.RoundToInt(vec.x), 0);
        }
        else
        {
            vec = new Vector2(0, Mathf.RoundToInt(vec.y));
        }

        _nextTargetDir = vec;

        if (_targetDir == -vec)
        {
            SetMoveDir(false);
        }
    }

    private void OnInteract()
    {
        RaycastHit2D[] results = Physics2D.RaycastAll(transform.position, _playerSprite.transform.up, 1);

        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].collider.tag == "Interactable")
            {
                results[i].transform.gameObject.GetComponent<InteractableBase>().StartInteraction();
            }
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log((_moveTargetPos - (Vector2)transform.position).sqrMagnitude);

        if ((_moveTargetPos - (Vector2)transform.position).sqrMagnitude <= (0.005f * _moveSpeed))
        {
            SetMoveDir();
        }
    }

    private void SetMoveDir(bool pSetPos = true)
    {
        _targetDir = _nextTargetDir;

        if (pSetPos)
            transform.position = _moveTargetPos;

        _moveTargetPos = (Vector2)transform.position + _targetDir;

        _moveTargetPos.x = Mathf.RoundToInt(_moveTargetPos.x);
        _moveTargetPos.y = Mathf.RoundToInt(_moveTargetPos.y);

        Debug.Log(_targetDir.x + " " + _targetDir.y + " " + ((-_targetDir.x + (_targetDir.y * 2)) * 90));

        Debug.Log(Quaternion.AngleAxis((-_targetDir.x + (_targetDir.y * 2)) * 90, Vector3.forward).eulerAngles + " " + (_targetDir.y! < 0));

        _playerBody.velocity = Vector2.zero;


        _playerBody.AddForce(_targetDir * _moveSpeed, ForceMode2D.Impulse);

        //Sprite Rotation

        if (_targetDir.x == _targetDir.y) return;

        if ((_targetDir.y > 0))
            _playerSprite.transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        else
            _playerSprite.transform.rotation = Quaternion.AngleAxis((-_targetDir.x + (_targetDir.y * 2)) * 90, Vector3.forward);

        //Console.Beep();
    }
}
