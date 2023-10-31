using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private float time = 0.1f;

    [SerializeField] public bool isPlayerTurn = true;

    public bool IsPlayerTurn { get => isPlayerTurn; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instantiate(Resources.Load<GameObject>("Player")).name = "Player";
    }

    public void EndTurn()
    {
        isPlayerTurn = false;
        StartCoroutine(WaitForTurns());
    }

    private IEnumerator WaitForTurns()
    {
        yield return new WaitForSeconds(time);
        isPlayerTurn = true;
    }
}
