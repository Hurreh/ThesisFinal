using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeshGenerator
{
    public static List<MeshData> GenerateDungeonMesh(List<Room> roomMaps)
    {
        List<MeshData> meshDatas = new List<MeshData>();
        //Debug.Log(roomMaps.Count());
        for (int i = 0; i < roomMaps.Count(); i++)
        {
            int width = roomMaps[i].drawMap.GetLength(0);
            int height = roomMaps[i].drawMap.GetLength(1);
            float topLeftX = (width - 1) / -2f;
            float topLeftZ = (height - 1) / -2f;


            MeshData meshData = new MeshData(width, height);
            int vertexIndex = 0;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, 0, topLeftZ - y);
                    meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                    if (x < width - 1  && y  < height - 1 )
                    {
                        if (roomMaps[i].drawMap[x, y])
                        {
                            meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                            meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);

                        }

                    }                  
                    vertexIndex++;
                }
            }
            meshDatas.Add(meshData);
        }    
        return meshDatas;
    }
}
public class MeshData
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangles;

    int triangleIndex;
    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;

    }
    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
