using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SimpleOffsetChangeOnTrigger : MonoBehaviour
{
    [SerializeField]
    private float _offsetChange = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<ProtoPlayer4>().ChangeOffset += _offsetChange;
        }
    }
}
