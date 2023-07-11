using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SimpleLayerChangerOnTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject _homeLayer;
    [SerializeField]
    private GameObject _targetLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _targetLayer.SetActive(true);
            _homeLayer.SetActive(false);
        }
    }
}
