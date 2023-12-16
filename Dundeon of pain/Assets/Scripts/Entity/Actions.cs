using UnityEngine;

static public class Action
{
    static public void EscapeAction()
    {
        Debug.Log("Quit");
        //Applications.Quit();
    }

    static public bool BumpAction(Entity entity, Vector2 direction)
    {
        Entity target = GameManager.instance.GetBlockingEntityAtLocation(entity.transform.position + (Vector3)direction);

        if (target)
        {
            MeleeAction(target);
            return false;
        }
        else
        {
            MovementAction(entity,direction);
            return true;
        }
    }

    static public void MeleeAction(Entity target)
    {
        Debug.Log($"“ы ударил {target.name}, но это его только разозлило!");
        GameManager.instance.EndTurn();
    }

    static public void MovementAction(Entity entity, Vector2 direction)
    {
        entity.Move(direction);
        entity.UpdateFieldOfView();
        GameManager.instance.EndTurn();
    }

    static public void SkipAction(Entity entity)
    {
        if (entity.GetComponent<Player>())
        {

        }
        else
        {

        }
        GameManager.instance.EndTurn();
    }
}
