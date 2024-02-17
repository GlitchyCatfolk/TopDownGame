using UnityEngine;

static public class Action
{
    static public void EscapeAction()
    {
        Debug.Log("Quit");
        //Applications.Quit();
    }

    static public bool BumpAction(Actor actor, Vector2 direction)
    {
        Actor target = GameManager.instance.GetBlockingActorAtLocation(actor.transform.position + (Vector3)direction);

        if (target)
        {
            MeleeAction(actor, target);
            return false;
        }
        else
        {
            MovementAction(actor,direction);
            return true;
        }
    }

    static public void MeleeAction(Actor actor, Actor target)
    {
        int damage = actor.GetComponent<Fighter>().Power - target.GetComponent<Fighter>().Defence;

        string attackDesc = $"{actor} �������� {target}";

        string colorHex = "";

        if (actor.GetComponent<Player>())
        {
            colorHex = "#ffffff";
        }
        else
        {
            colorHex = "#d1a3a4";
        }

        if (damage > 0)
        {
            UIManager.instance.AddMessage($"{attackDesc}, ������ {damage} �����", colorHex);
            target.GetComponent<Fighter>().Hp-=damage;
        }
        else
        {
            UIManager.instance.AddMessage($"{attackDesc}, �� �� ����� �����", colorHex);
        }
        GameManager.instance.EndTurn();
    }

    static public void MovementAction(Actor actor, Vector2 direction)
    {
        actor.Move(direction);
        actor.UpdateFieldOfView();
        GameManager.instance.EndTurn();
    }

    static public void SkipAction()
    {
        GameManager.instance.EndTurn();
    }
}
