using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : Entity
{
    [SerializeField] private bool isAlive;

    [SerializeField] private int fieldOfViewRange = 8;

    [SerializeField] private List<Vector3Int> fieldOfView = new List<Vector3Int>();

    [SerializeField] private AI aI;

    AdamMilVisibility algorithm;

    public bool IsAlive { get => isAlivet; }

    private void OnValidate()
    {
        if (GetComponent<AI>())
        {
            aI = GetComponent<AI>();
        }
    }

    void Start()
    {
        AddToGameManager();
        if (GetComponent<Player>())
        {
            GameManager.instance.InsertActor(this, 0);
        }
        else
        {
            GameManager.instance.AddActor(this);
        }
        
        algorithm = new AdamMilVisibility();
        UpdateFieldOfView();

    }

    public void UpdateFieldOfView()
    {
        Vector3Int gridPosition=MapManager.instance.FloorMap.WorldToCell(transform.position);

        fieldOfView.Clear();
        algorithm.Compute(gridPosition, fieldOfViewRange, fieldOfView);

        if (GetComponent<Player>())
        {
            MapManager.instance.UpdateFogMap();
            MapManager.instance.SetEntitiesVisibilities();
        }
    }

}
