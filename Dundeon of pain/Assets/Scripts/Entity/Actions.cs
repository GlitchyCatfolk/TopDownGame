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
            MeleeAction(target);
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

        string attackDesc = $"{actor} атаковал {target}";

        if (damage > 0)
        {
            Debug.Log($"{attackDesc}, нанеся {damage} урона");
            target.GetComponent<Fighter>().Hp-=damage;
        }
        else
        {
            Debug.Log($"{attackDesc}, но не нанес урона");
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
