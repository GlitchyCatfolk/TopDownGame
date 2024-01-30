using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AStar))]
public class AI : MonoBehaviour
{
    [SerializeField] private AStar aStar;

    public AStar AStar { get => aStar; set => aStar = value; }

    private void OnValidate() => aStar = GetComponent<AStar>();

    public void MoveAlongPath(Vector3Int targetPosition)
    {
        Vector3Int gridPosition = MapManager.instance.FloorMap.WorldToCell(transform.position);
        Vector2Int direction = aStar.Compute((Vector2Int)gridPosition, (Vector2Int)targetPosition.position);
        Action.MovementAction(GetComponent<Actor>(),direction);
    }
}
