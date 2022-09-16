using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TerrainManager : MonoBehaviour
{
    public enum TerrainTypes { Street, Path, BikeRoad, Offroad  }

    [SerializeField]
    private TerrainTypes _terraintype;

    [SerializeField]
    private float _walkSpeedMultiplier = 1.0f;

    [SerializeField]
    private float _bikeSpeedMultiplier = 1.0f;

    private BoxCollider2D _terrainTrigger;

    public TerrainTypes Terraintype { get { return _terraintype; } }
    public float WalkSpeedMultiplier { get { return _walkSpeedMultiplier; } }
    public float BikeSpeedMultiplier { get { return _bikeSpeedMultiplier; } }

    private void Awake()
    {
        _terrainTrigger = gameObject.AddComponent<BoxCollider2D>();
        _terrainTrigger.isTrigger = true;
        _terrainTrigger.size = gameObject.GetComponent<SpriteRenderer>().size * 0.8f;
    }
}
