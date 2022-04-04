using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomRoomPlacementGenerator 
{
    public static int CalculateNumberOfRooms(Dungeon dungeon)
    {
        //made up equation. 
        int numberOfRooms;
        int dungeonSize = dungeon.DungeonSizeX * dungeon.DungeonSizeY;
        float averageRoomSize = (dungeon.maxRoomSizeX / 2) * (dungeon.maxRoomSizeY / 2);
        float howManyToFill = dungeonSize / averageRoomSize;
        numberOfRooms = Mathf.CeilToInt(howManyToFill / ((1f / 50) * 1000f));
        return numberOfRooms;
    }
    public static List<bool[,]> Generator(Dungeon dungeon)
    {
        List<bool[,]> dungeons = new List<bool[,]>();
        int numberOfRooms = CalculateNumberOfRooms(dungeon);
        //For each room we want to generate
        for (int i = 0; i < numberOfRooms; i++)
        {
            int roomSizeX = 0;
            int roomSizeY = 0;

            roomSizeX = Random.Range(dungeon.minimalRoomSize, dungeon.maxRoomSizeX);
            roomSizeY = Random.Range(dungeon.minimalRoomSize, dungeon.maxRoomSizeY);

            bool[,] room = new bool[roomSizeX, roomSizeY];

            dungeons.Add(room);
        }
        return dungeons;
    }

    public static List<Room> RandomRoomPlacer(List<bool[,]> dungeons, Dungeon dungeon)
    {
        //Nie pozwoli na ¿adne z trzech poni¿szych?

        bool[,] map = new bool[dungeon.DungeonSizeX, dungeon.DungeonSizeY];
        List<List<(int x, int y)>> rooms = new List<List<(int x, int y)>>();
        List<Room> roomsSorted = new List<Room>();
        List<List<(int x, int y)>> fullRooms = new List<List<(int x, int y)>>();
        map = Utilities.PopulateArray(map);

        for (int i = 0; i < dungeons.Count; i++)
        {
            int startingCoordX = 0;
            int startingCoordY = 0;
            //Coords can't begin in a place that would cause one of the sides of room to flow outside the boundaries.
            startingCoordX = Random.Range(1, dungeon.DungeonSizeX - dungeon.maxRoomSizeX );
            startingCoordY = Random.Range(1, dungeon.DungeonSizeY - dungeon.maxRoomSizeY );
            rooms.Add(new List<(int x, int y)>());
            fullRooms.Add(new List<(int x, int y)>());
            for (int x = 0; x < dungeon.DungeonSizeX; x++)
            {
                for (int y = 0; y < dungeon.DungeonSizeY; y++)
                {
                    if ((x >= startingCoordX && y >= startingCoordY) && (x <= startingCoordX + dungeons[i].GetLength(0) && y <= startingCoordY + dungeons[i].GetLength(1)))
                    {
                        map[x, y] = true;
                        fullRooms[i].Add((x, y));
                        //If point is on some corner of rectangle add it.
                        if ((x == startingCoordX || x == startingCoordX + dungeons[i].GetLength(0)) && (y == startingCoordY || y == startingCoordY + dungeons[i].GetLength(1)))
                        {
                            rooms[i].Add((x, y));
                        }
                    }
                }
            }
        }
        roomsSorted = listRooms(rooms, fullRooms);
        for (int i = 0; i < roomsSorted.Count(); i++)
        {
            roomsSorted[i] = Utilities.DefineWalls(roomsSorted[i]);
        }

        return roomsSorted;
    }


    

    public static List<Room> listRooms(List<List<(int x, int y)>> rooms, List<List<(int x, int y)>> fullRooms)
    {
        //foreach room and...

        for (int room = 0; room < rooms.Count(); room++)
        {
            //...each it's vertice...
            for (int vertice = 0; vertice < rooms[room].Count(); vertice++)
            {

                //we have to check each room
                for (int nestedRoom = 0; nestedRoom < rooms.Count(); nestedRoom++)
                {
                    //but we cannot compare a room to itself
                    if (room != nestedRoom)
                    {
                        int i = 0;
                        //universal logic for all configurations. We are checking if current room is in any rectangle inside the bigger shape (if it's a bigger shape if not then it's just rectangle inside the rectangle).
                        for (int nestedVertice = 0; nestedVertice < rooms[nestedRoom].Count() / 4; nestedVertice++)
                        {
                            if (rooms[room][vertice].x >= rooms[nestedRoom][i * 4].x && rooms[room][vertice].y >= rooms[nestedRoom][i * 4].y
                                && rooms[room][vertice].x <= rooms[nestedRoom][3 + i * 4].x && rooms[room][vertice].y <= rooms[nestedRoom][3 + i * 4].y)
                            {
                                //rooms are being used for calculating merge whereas fullRooms are actual rooms.
                                List<(int x, int y)> mergedRooms = new List<(int x, int y)>(rooms[room].Concat(rooms[nestedRoom]));
                                List<(int x, int y)> mergedFullRooms = new List<(int x, int y)>(fullRooms[room].Concat(fullRooms[nestedRoom]));
                                rooms.Add(mergedRooms); fullRooms.Add(mergedFullRooms);
                                rooms.RemoveAt(room); fullRooms.RemoveAt(room);
                                if (room < nestedRoom)
                                    nestedRoom--;
                                rooms.RemoveAt(nestedRoom); fullRooms.RemoveAt(nestedRoom);
                                goto LoopStart;
                            }
                            i++;
                        }
                    }
                }
            }
        LoopStart:
            continue;
        }
        List<Room> listOfRooms = new List<Room>();
        foreach (var singleRoom in fullRooms)
        {
            int maxX = singleRoom.Max(room => room.x) + 1;
            int maxY = singleRoom.Max(room => room.y) + 1;
            bool[,] drawMap = new bool[maxX, maxY];
            Room room = new Room();
            foreach (var tile in singleRoom)
            {

                drawMap[tile.x, tile.y] = true;

                room.Tiles.Add(new Tile(tile.x, tile.y));
            }
            room.drawMap = drawMap;
            listOfRooms.Add(room);
        }
        for (int i = 0; i < rooms.Count(); i++)
        {
            Tile centerTile = new Tile(0, 0);
            int distanceToCenterX = (int)Mathf.Round((rooms[i][2].x - rooms[i][0].x) / 2);
            int distanceToCenterY = (int)Mathf.Round((rooms[i][1].y - rooms[i][0].y) / 2);
            centerTile.X = (rooms[i][2].x - distanceToCenterX);
            centerTile.Z = (rooms[i][1].y - distanceToCenterY);
            listOfRooms[i].RoomCenter = centerTile;
        }
        return listOfRooms;
    }
}
