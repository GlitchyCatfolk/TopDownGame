using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
sealed class Fighter : MonoBehaviour
{
    [SerializeField] private int maxHp, hp, defence, power;
    [SerializeField] private Actor target;

    public int Hp
    {
        get => hp; set
        {
            hp=Mathf.Max(0,Mathf.Min(value,maxHp));

            if (GetComponent<Player>())
            {
                UIManager.instance.SetHealth(hp, maxHp);
            }

            if (hp == 0)
            {
                Die();
            }
        }
    }

    public int Defence { get => defence; }
    public int Power { get => power; }
    public Actor Target { get => target; set => target = value; }

    private void Start()
    {
        if (GetComponent<Player>())
        {
            UIManager.instance.SetHealthMax(maxHp);
            UIManager.instance.SetHealth(hp, maxHp);
        }
    }

    private void Die()
    {

        if (GetComponent<Player>())
        {
            UIManager.instance.AddMessage("Вы сыграли в ящик", "#ff0000");
        }
        else
        {
            UIManager.instance.AddMessage($"{name} скопытился", "#ffa500");
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameManager.instance.DeadSprite;
        spriteRenderer.color=new Color(191,0,0,1);
        spriteRenderer.sortingOrder = 0;

        name = $"Ошметки {name}";
        GetComponent<Actor>().BlocksMovement = false;
        if (!GetComponent<Player>())
        {
            GameManager.instance.RemoveActor(this.GetComponent<Actor>());
        }
    }

    public int Heal(int amount)
    {
        if (hp == maxHp)
        {
            return 0;
        }

        int newHpValue = hp + amount;
        if(newHpValue>maxHp)
        {
            newHpValue= maxHp;
        }

        int amountRecovered = newHpValue - hp;
        Hp = newHpValue;
        return amountRecovered;
    }
}
