using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractableBase : MonoBehaviour
{
    [SerializeField]
    private Collider2D _interactionTrigger;

    private void Awake()
    {
        _interactionTrigger = gameObject.GetComponent<Collider2D>();
        _interactionTrigger.isTrigger = true;
        gameObject.tag = "Interactable";
    }

    public abstract void StartInteraction();

    public abstract void EndInteraction();
}
