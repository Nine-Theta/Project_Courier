using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SimpleSceneLoaderOnTrigger : MonoBehaviour
{
    [SerializeField]
    private SceneAsset _sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(_sceneToLoad.name);
    }
}
