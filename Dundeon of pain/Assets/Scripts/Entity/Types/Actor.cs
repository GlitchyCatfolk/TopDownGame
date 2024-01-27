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


    void Start()
    {
        if (isSentient)
        {
            if (GetComponent<Player>())
            {
                GameManager.instance.InsertEntity(this, 0);
            }
            else
            {
                GameManager.instance.AddEntity(this);
            }
        }

        fieldOfView = new List<Vector3Int>();
        algorithm = new AdamMilVisibility();
        UpdateFieldOfView();

    }

}
