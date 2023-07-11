using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SimpleTeleporterOnTrigger : MonoBehaviour
{
    [SerializeField]
    private Vector2 _teleportLocation = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.transform.position = _teleportLocation;
    }
}
