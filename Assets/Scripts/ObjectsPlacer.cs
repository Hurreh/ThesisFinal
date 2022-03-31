using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsPlacer : MonoBehaviour
{
    public GameObject simpleWall;
    public GameObject simpleWindow;
    public GameObject simpleDoor;
    public GameObject riddle;

    int doorCount = 0;

    private void Start()
    {
        
    }
    private void Awake()
    {
       
    }

    public void PlaceObjects(List<Room> roomMaps)
    {
        this.doorCount = 0;
        for (int i = 0; i < roomMaps.Count(); i++)
        {
            int width = roomMaps[i].drawMap.GetLength(0);
            int height = roomMaps[i].drawMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (height - 1) / -2f;

            MeshData meshData = new MeshData(width, height);

            foreach (var tile in roomMaps[i].Tiles)
            {
                PlacementDetails(tile, topLeftX, topLeftZ);
            }
            

        }
    }

    public void PlacementDetails(Tile tile, float tlx, float tlz)
    {
        switch (tile.TileType)
        {
            case TileType.L:
              
                PopulateTiles("L",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.LU:
                PopulateTiles("LU",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                PopulateTiles("LU",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.LD:
                PopulateTiles("LD",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("LD",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.R:
                PopulateTiles("R",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z -0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.RU:
                PopulateTiles("RU",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                PopulateTiles("RU",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.RD:
                PopulateTiles("RD",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("RD",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.TOP:
                PopulateTiles("TOP",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                break;
            case TileType.BOT:
                PopulateTiles("BOT",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                break;
            case TileType.MID:
                PopulateTiles("MID", tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z), Quaternion.identity);
                break;
            case TileType.UD:
                PopulateTiles("UD",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("UD",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                break;
            case TileType.LR:
                PopulateTiles("LR",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                PopulateTiles("LR",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.UDL:
                PopulateTiles("UDL",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("UDL",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                PopulateTiles("UDL",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.UDR:
                PopulateTiles("UDR",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("UDR",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                PopulateTiles("UDR",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.DLR:
                PopulateTiles("DLR",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z - 1f), Quaternion.Euler(0, 90, 0));
                PopulateTiles("DLR",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                PopulateTiles("DLR",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            case TileType.ULR:
                PopulateTiles("ULR",tile, new Vector3(tlx + tile.X + 0.5f, 0.5f, tlz - tile.Z), Quaternion.Euler(0, 90, 0));
                PopulateTiles("ULR",tile, new Vector3(tlx + tile.X + 1f, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                PopulateTiles("ULR",tile, new Vector3(tlx + tile.X, 0.5f, tlz - tile.Z - 0.5f), Quaternion.Euler(0, 0, 0));
                break;
            default:
                break;
        }
    }

    private void PopulateTiles(string name,Tile tile, Vector3 coords, Quaternion rotation)
    {
        if (tile.IsDoor)
        { 
            DoorLogicScript door = simpleDoor.GetComponent<DoorLogicScript>();
            simpleDoor.name = "SimpleDoor" + name;
            door.doorCount = doorCount;    
            Instantiate(simpleDoor, coords, rotation);
            if (doorCount == 0)
            {
                var player = GameObject.FindGameObjectWithTag("Player");
                if (tile.TileType == TileType.L)
                    player.transform.position = new Vector3(coords.x + 1, coords.y, coords.z);
                if (tile.TileType == TileType.R)
                    player.transform.position = new Vector3(coords.x - 1, coords.y, coords.z);
                if (tile.TileType == TileType.TOP)
                    player.transform.position = new Vector3(coords.x, coords.y, coords.z - 1);
                if (tile.TileType == TileType.BOT)
                    player.transform.position = new Vector3(coords.x, coords.y, coords.z + 1);
            }
            doorCount++;

        }
        else if (tile.IsSpecialWall)
        {
            simpleWindow.name = "SimpleWindow" + name;
            Instantiate(simpleWindow, coords, rotation);
        }
        else if (tile.HasRiddle)
        {
            riddle.name = "Riddle" + name;
            Instantiate(riddle, new Vector3(coords.x , coords.y, coords.z ), rotation);
        }
        else if(tile.TileType != TileType.MID)
        {
            simpleWall.name = "SimpleWall" + name;
            Instantiate(simpleWall, coords, rotation);
        }
        
    }
}
