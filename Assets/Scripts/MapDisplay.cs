using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData mesh, Texture2D texture)
    {
        Shader shader;
        Color color = Color.gray;

        GameObject room = new GameObject();
        room.name = "room";
        room.tag = "room";
        room.AddComponent<MeshFilter>();
        room.AddComponent<MeshRenderer>();
        
        
        Renderer mat = room.GetComponent<Renderer>();
        mat.sharedMaterial = new Material(Shader.Find("Standard"));
        mat.sharedMaterial.color = color;

        
        
        room.GetComponent<MeshFilter>().mesh = mesh.CreateMesh();
        room.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
    }
}
