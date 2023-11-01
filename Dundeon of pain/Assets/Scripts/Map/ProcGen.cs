using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class NewBehaviourScript : MonoBehaviour
{
    public void GenerateDungeon(int mapWidth,int mapHeight,int roomMaxSize,int roomMinSize,int maxRooms,List<RectangularRoom> rooms)
    {
        for(int roomNum = 0; roomNum < maxRooms; roomNum++)
        {
            int roomWidth = Random.Range(roomMinSize, roomMaxSize);
            int roomHeight = Random.Range(roomMinSize, roomMaxSize);

            int roomX = Random.Range(0, mapWidth - roomWidth - 1);
            int roomY = Random.Range(0, mapHeight - roomHeight - 1);

            RectangularRoom newRoom = new RectangularRoom(roomX, roomY, roomHeight, roomWidth);

            if (newRoom.Overlaps(rooms))
                continue;

            for(int x = roomX; x < roomX+roomWidth; x++)
            {
                for (int y = roomY; y < roomY + roomHeight; y++)
                {
                    if (x == roomX || x == roomX + roomWidth - 1 || y == roomY || y == roomY + roomHeight - 1) 
                    { 
                        if (SetWallTileIfEmpty(new Vector3Int(x, y, 0)))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if(MapManager.instance.ObstacleMap.GetTile(new Vector3Int(x, y, 0)))
                        {
                            MapManager.instance.ObstacleMap.SetTile(new Vector3Int(x, y, 0), null);
                        }
                        MapManager.instance.FloorMap.SetTile(new Vector3Int(x, y, 0), MapManager.instance.FloorTile);
                    }
                }
            }
            if (MapManager.instance.Rooms.Count == 0)
            {
                MapManager.instance.CreatePlayer(newRoom.Center());
            }
            rooms.Add(newRoom);
        }
    }

    private bool SetWallTileIfEmpty(Vector3Int pos)
    {
        if(MapManager.instance.FloorMap.GetTile(new Vector3Int(pos.x, pos.y, 0)))
        {
            return true;
        }
        else
        {
            MapManager.instance.ObstacleMap.SetTile(new Vector3Int(pos.x, pos.y, 0), MapManager.instance.WallTile);
            return false;
        }
    }
}
