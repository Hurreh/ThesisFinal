using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utilities 
{
    public static Room DefineWalls(Room room)
    {
        foreach (var tile in room.Tiles) //FROM MOST COMMON TO LEAST COMMON
        {
            //MID 
            if (room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                && room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))

            {
                tile.TileType = TileType.MID;
                continue;
            }

            //LEFT
            if (room.Tiles.Exists(x => (x.X == tile.X + 1 && x.Z == tile.Z)
                           && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                           && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.L;
                continue;
            }
            //LEFT DOWN
            if (room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                           && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                           && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                           && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.LD;
                continue;
            }
            //Blocked left and right
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.LR;
                continue;
            }
            //Blocked down, left and right
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.DLR;
                continue;

            }
            //LEFT UPPER
            if (room.Tiles.Exists(x => (x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.LU;
                continue;
            }

            //Blocked up, down and left
            if (room.Tiles.Exists(x => (x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.UDL;
                continue;
            }
            //Blocked up, left and right
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.ULR;
                continue;
            }

            //TOP
            if (room.Tiles.Exists(x => (x.X == tile.X - 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.TOP;
                continue;
            }
            //RIGHT UPPER
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.RU;
                continue;
            }
            //Blocked up and down
            if (room.Tiles.Exists(x => (x.X == tile.X + 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.UD;
                continue;
            }
            //Blocked up, down and right
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.UDR;
                continue;
            }

            //BOT
            if (room.Tiles.Exists(x => (x.X == tile.X - 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)))
            {
                tile.TileType = TileType.BOT;
                continue;
            }
            //Right
            if (room.Tiles.Exists(x => (x.X == tile.X - 1 && x.Z == tile.Z)
                           && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                           && room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z)))
            {
                tile.TileType = TileType.R;
                continue;
            }
            //RIGHT DOWN
            if (!room.Tiles.Exists(x => x.X == tile.X + 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X - 1 && x.Z == tile.Z)
                                   && room.Tiles.Exists(x => x.X == tile.X && x.Z + 1 == tile.Z)
                                   && !room.Tiles.Exists(x => x.X == tile.X && x.Z - 1 == tile.Z))
            {
                tile.TileType = TileType.RD;
                continue;
            }

        }
        return room;
    }
    //fill array with data.
    public static bool[,] PopulateArray(bool[,] array)
    {
        for (int x = 0; x < array.GetLength(0); x++)
        {
            for (int y = 0; y < array.GetLength(1); y++)
            {
                array[x, y] = false;
            }
        }
        return array;
    }
    //Widen a generated map by one each direction.
    public static bool[,] Thickener(bool[,] map, int amount)
    {

        bool[,] thickened = new bool[map.GetLength(0), map.GetLength(1)];
        thickened = PopulateArray(thickened);
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == true)
                {
                    for (int r = -amount; r <= amount; r++)
                    {
                        for (int c = -amount; c <= amount; c++)
                        {
                            if (!(x + c < 0 || x + c > map.GetLength(0) - 1 || y + r < 0 || y + r > map.GetLength(1) - 1)) //Check if it overflows
                            {
                                thickened[x + c, y + r] = true;
                                continue;

                            }
                            else
                            {
                                continue;
                            }

                        }
                    }
                }
            }
        }
        return thickened;

    }
    public static Room DefineObjects(Room room, int doorCount, int riddleCount, int specialTilesCount, int finalRiddleCount)
    {      
        //Define doors
        foreach (var tile in room.Tiles)
        {
            double prop = Random.value;
            if (tile.TileType == TileType.L || tile.TileType == TileType.R || tile.TileType == TileType.TOP || tile.TileType == TileType.BOT)
            {

                if (doorCount != 2)
                {

                    //if this side already has doors skip
                    if (room.Tiles.Where(x => x.TileType == tile.TileType && x.IsDoor == true).Any())
                    {
                        goto SpecialCheck;
                    }
                    else
                    {

                        if (prop >= 0.9)
                        {
                            tile.IsDoor = true;
                            doorCount++;

                            if (doorCount == 2)
                                continue;

                        }
                    }
                }
            SpecialCheck:
                //special tile (window etc.) (30% chance)
                if (tile.IsDoor == false && prop < 0.9 && prop >= 0.6)
                {
                    tile.IsSpecialWall = true;
                    specialTilesCount++;
                }
            }
            if (tile.TileType == TileType.MID && riddleCount != finalRiddleCount)
            {
                if (prop >= 0.95)
                {
                    tile.HasRiddle = true;
                    riddleCount++;
                }
            }


        }
        if (doorCount != 2 && riddleCount != finalRiddleCount)
        {
            DefineObjects(room, doorCount, riddleCount, specialTilesCount, finalRiddleCount);
        }
        return room;
    }
}

public class Room
{
    public List<Tile> Tiles { get; set; }
    public bool HasDoor { get; set; }
    public int RiddleCount { get; set; }
    public bool Solved { get; set; }
    public Tile RoomCenter { get; set; }
    public bool IsCorridor { get; set; }
    public bool[,] drawMap { get; set; }


    public Room()
    {
        Tiles = new List<Tile>();
        RoomCenter = new Tile(0, 0);
        IsCorridor = false;
    }

}
public class Tile
{
    public int X { get; set; }
    public int Z { get; set; }
    public TileType TileType { get; set; }
    public bool HasRiddle { get; set; }
    public bool IsDoor { get; set; }
    public bool IsSpecialWall { get; set; }

    public Tile(int x, int z)
    {
        X = x;
        Z = z;
    }
}
public enum TileType
{   //                                                     ULR
    //ROOM TILES                                           LR
    L, //LEFT                                           LU TOP RU
    LU, //LEFT UPPER                            UDL UD  L  MID  R  UD UDR
    LD, //LEFT DOWN                                     LD BOT RD
    R, //RIGHT                                             LR 
    RU,//RIGHT UPPER                                       DLR
    RD,//RIGHT DOWN
    TOP,//TOP
    BOT,//BOTTOM
    MID, //MIDDLE

    //Corridors and tightspots
    UD, //Blocked up and down
    LR, //Blocked left and right
    UDL, //Blocked up, down and left
    UDR, //Blocked up, down and right
    DLR, //Blocked down, left and right
    ULR //Blocked up, left and right

    //IF SURROUNDED
    //MID


    //IF NO TILE ON LEFT
    //L, LU, LD, LR, UDL, ULR, DLR, - all options
    //AND
    //IF NO UP
    //L, LD, LR, DLR
    //AND IF UP
    //LU, UDL, ULR



    //IF TILE ON LEFT
    //TOP, BOT, RU,R, RD, UD, UDR - all options
    //AND
    //IF NO UP
    //BOT, R, RD, 
    //AND
    //IF UP
    //TOP, RU, UD, UDR
}
public class Dungeon
{
    public int DungeonSizeX { get; set; }
    public int DungeonSizeY { get; set; }
    public int maxRoomSizeX { get; set; }
    public int maxRoomSizeY { get; set; }
    public int minimalRoomSize { get; set; }
    public Dungeon(int dungeonSizeX, int dungeonSizeY, int maxRoomSizeX, int maxRoomSizeY, int minimalRoomSize)
    {
        this.DungeonSizeX = dungeonSizeX;
        this.DungeonSizeY = dungeonSizeY;

        this.maxRoomSizeX = maxRoomSizeX;
        this.maxRoomSizeY = maxRoomSizeY;

        this.minimalRoomSize = minimalRoomSize;
    }

}
public class Node
{
    public int X { get; set; }
    public int Y { get; set; }
    public int GCost { get; set; }
    public int HCost { get; set; }
    public int FCost { get; set; }
    public Node ParentNode { get; set; }

    public Node(int x, int y, int gCost, int hCost)
    {
        X = x;
        Y = y;
        GCost = gCost;
        HCost = hCost;
        FCost = gCost + hCost;
    }
}
