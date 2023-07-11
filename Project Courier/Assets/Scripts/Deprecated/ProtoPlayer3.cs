using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D)), Obsolete]
public class ProtoPlayer3 : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private Vector2 _moveTargetPos;

    private Vector2 _targetDir;
    private Vector2 _nextTargetDir;

    private Rigidbody2D _playerBody;

    private void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();

        _moveTargetPos = transform.position;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        _nextTargetDir = Vector2.zero;
        SetMoveDir(false);
        Debug.Log("got bopped");
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

    private void FixedUpdate()
    {
        //Debug.Log((_moveTargetPos - (Vector2)transform.position).sqrMagnitude);

        if ((_moveTargetPos - (Vector2)transform.position).sqrMagnitude <= 0.0001f)
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

        _playerBody.velocity = Vector2.zero;

        Debug.Log("bep " + _playerBody.velocity);

        _playerBody.AddForce(_targetDir * _moveSpeed, ForceMode2D.Impulse);

        //Console.Beep();
    }
}
