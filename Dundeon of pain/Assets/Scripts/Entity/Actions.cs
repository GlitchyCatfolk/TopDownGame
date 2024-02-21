using UnityEngine;

static public class Action
{
    static public void PickupAction(Actor actor)
    {
        for(int i = 0; i < GameManager.instance.Entities.Count; i++)
        {
            if (GameManager.instance.Entities[i].GetComponent<Actor>() || actor.transform.position != GameManager.instance.Entities[i].transform.position)
            {
                continue;
            }

            if (actor.Inventory.Items.Count >= actor.Inventory.Capacity)
            {
                UIManager.instanse.AddMessage("����� ������ ���", "#808080");
                return;
            }

            Item item = GameManager.instance.Entities[i].GetComponent<Item>();
            item.transform.SetParent(actor.transform);
            actor.Inventory.Items.Add(item);

            UIManager.instanse.AddMessage($"�� �������� {item.name}", "#ffffff");
            GameManager.instance.RemoveEntity(item);
            GameManager.instance.EndTurn();
        }
    }

    static public void DropAction(Actor actor, Item item)
    {
        actor.Inventory.Drop(item);
        UImanager.instance.ToggleDropMenu();
        GameManager.instance.EndTurn();
    }

    static public void UseAction(Actor actor, int index)
    {
        Item item = actor.Inventory.Items[index];

        bool itemUsed = false;

        if (item.GetComponent<Consumable>())
        {
            itemUsed = item.GetComponent<Consumable>.Activate(actor, item);
        }

        if (!itemUsed)
        {
            return;
        }

        UIManager.ToggleInventory();
        GameManager.instance.EndTurn();
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
