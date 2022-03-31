using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private void Start()
    {
        if (GameLogicScript.current != null)
        {
            GameLogicScript.current.loadNextRoundEvent += DrawMapInEditor;
        }
        
    }
    public enum GenerationMode
    {
        Floors,
        FloorsAndWalls,
    }
    public enum AlgorithmType
    {
        DiffusionLimitedAlgorithm,
        DiffusionLimitedAlgorithmWithCentralAttractor,
        RandomWalkAlgorithm,
        RandomRoomPlacement
    }
    public GenerationMode generationMode;
    public AlgorithmType algorithmType;
    public int dungeonSizeX;
    public int dungeonSizeY;
    [Range(0,100)]
    public int roomDensity;
    public int maxRoomSizeX;
    public int maxRoomSizeY;
    public int minimalRoomSize;
    [Range(300,15000)]
    public int iterations;
    
    public void DrawMapInEditor()
    { 
        Texture2D texture = new Texture2D(16, 16, TextureFormat.RGBA32, false);
        MapData mapData = mapDataGenerator();
        MapDisplay display = FindObjectOfType<MapDisplay>();
        ObjectsPlacer objectsPlacer = FindObjectOfType<ObjectsPlacer>();
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("room");
        GameObject[] buildObjects = GameObject.FindGameObjectsWithTag("Building");
        GameObject[] riddles = GameObject.FindGameObjectsWithTag("Riddle");
        List<MeshData> meshDatas = new List<MeshData>(MeshGenerator.GenerateDungeonMesh(mapData.roomMaps));
        switch (generationMode)
        {
            case GenerationMode.Floors:
                if (rooms != null)
                {
                    CleanUp(rooms, false);
                }         
                foreach (var mesh in meshDatas)
                {
                    display.DrawMesh(mesh, texture);
                }
                break;
            case GenerationMode.FloorsAndWalls:
                if (rooms != null)
                {
                    CleanUp(rooms, false);
                    CleanUp(buildObjects, false);
                    CleanUp(riddles, false);
                }             
                foreach (var mesh in meshDatas)
                {
                    display.DrawMesh(mesh, texture);
                }          
                objectsPlacer.PlaceObjects(mapData.roomMaps);
                //CleanUp(riddles, true);
                riddles = null;
                riddles = GameObject.FindGameObjectsWithTag("Riddle");
                if (riddles.Length == 0)
                {
                    DrawMapInEditor();
                }
                GameLogicScript.current.levelLoading = false;
                break;
            default:
                break;
        }

    }
    public MapData mapDataGenerator()
    {
        DLAGenerator DLA = new DLAGenerator();
        RandomWalkGenerator RWG = new RandomWalkGenerator();
        Dungeon dungeonInfo = new Dungeon(this.dungeonSizeX, this.dungeonSizeY, this.roomDensity, this.maxRoomSizeX, this.maxRoomSizeY, 0, this.minimalRoomSize);
        List<bool[,]> dungeonMap = new List<bool[,]>();
        List<Room> roomMaps = new List<Room>();
        switch (algorithmType)
        {
            case AlgorithmType.DiffusionLimitedAlgorithm:
                roomMaps.Add(DLA.DLAProper(dungeonInfo, iterations));
                break;
            case AlgorithmType.DiffusionLimitedAlgorithmWithCentralAttractor:
                roomMaps.Add(DLA.DLAwCA(dungeonInfo, iterations));
                break;
            case AlgorithmType.RandomWalkAlgorithm:  
                roomMaps.Add(RWG.DrunkardWalkGenerator(dungeonInfo, iterations, false));
                break;
            case AlgorithmType.RandomRoomPlacement:
                dungeonMap = RandomRoomPlacementGenerator.Generator(dungeonInfo);
                roomMaps = RandomRoomPlacementGenerator.RandomRoomPlacer(dungeonMap,dungeonInfo);
                
                break;
            default:
                break;
        }


        return new MapData(roomMaps);
    }
    private void CleanUp(GameObject[] objects, bool zeroCleanup)
    {
        if (zeroCleanup)
        {

            foreach (var item in objects)
            {
                if (item != null && item.transform.position == Vector3.zero)
                {
                    Destroy(item);
                }
            }

        }
        else
        {
            foreach (var item in objects)
            {
                if (item != null)
                {
                    Destroy(item);
                }
                
            }
        }
       
        

        
    }
    
}
[System.Serializable]
public struct MapData
{
    public readonly List<Room> roomMaps;

    public MapData(List<Room> roomMaps)
    {
        this.roomMaps = roomMaps;
    }
}
