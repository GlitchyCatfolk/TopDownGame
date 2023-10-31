using UnityEngine;

static public class Action
{
    static public void EscapeAction()
    {
        Debug.Log("Quit");
        //Applications.Quit();
    }

    static public void MovementAction(Entity entity, Vector2 direction)
    {
        entity.Move(direction);
        GameManager.instance.EndTurn();
    }

    static public void SkipAction(Entity entity)
    {
        GameManager.instance.EndTurn();
    }
}
