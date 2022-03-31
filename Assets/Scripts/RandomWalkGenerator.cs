using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalkGenerator 
{
    public Room DrunkardWalkGenerator(Dungeon dungeon, int steps, bool improved)
    {
        double diceRoll = 0;
        Room room = new Room();
        bool[,] walkpath = new bool[dungeon.DungeonSizeX, dungeon.DungeonSizeY];
        walkpath = Utilities.PopulateArray(walkpath);


        int startingPointX = Random.Range(0, dungeon.DungeonSizeX);
        int startingPointY = Random.Range(0, dungeon.DungeonSizeY);
        walkpath[startingPointX, startingPointY] = true;
        (int x, int y) previousStep = (startingPointX, startingPointY);
        for (int i = 0; i < steps; i++)
        {
            diceRoll = Random.value;
            //move up if there's still place. Otherwise roll again and add one more step. Drunkard never gives up!.
            if (diceRoll >= 0.75f)
                if (previousStep.y + 1 >= dungeon.DungeonSizeY - 1)
                {
                    steps++;
                    continue;
                }
                else
                {
                    walkpath[previousStep.x, previousStep.y + 1] = true;
                    previousStep = (previousStep.x, previousStep.y + 1);
                    continue;
                }
            //move down
            if (diceRoll < 0.75f && diceRoll >= 0.5f)
                if (previousStep.y - 1 >= 1)
                {
                    walkpath[previousStep.x, previousStep.y - 1] = true;
                    previousStep = (previousStep.x, previousStep.y - 1);
                    continue;
                }
                else
                {

                    steps++;
                    continue;
                }
            //move left
            if (diceRoll < 0.5f && diceRoll >= 0.25f)
                if (previousStep.x - 1 >= 1)
                {
                    walkpath[previousStep.x - 1, previousStep.y] = true;
                    previousStep = (previousStep.x - 1, previousStep.y);
                    continue;
                }
                else
                {
                    steps++;
                    continue;
                }
            //move right
            if (diceRoll < 0.25f)
                if (previousStep.x + 1 >= dungeon.DungeonSizeX - 1)
                {
                    steps++;
                    continue;
                }
                else
                {
                    walkpath[previousStep.x + 1, previousStep.y] = true;
                    previousStep = (previousStep.x + 1, previousStep.y);
                    continue;

                }

        }
        room.drawMap = walkpath;
        for (int x = 0; x < dungeon.DungeonSizeX; x++)
        {
            for (int y = 0; y < dungeon.DungeonSizeY; y++)
            {
                if (walkpath[x,y])
                {
                    room.Tiles.Add(new Tile(x, y));
                }
            }
        }
        room = Utilities.DefineWalls(room);
        room = Utilities.DefineObjects(room, 0, 5, 10,10);
        return room;
    }
}
