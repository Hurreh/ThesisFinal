using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MapDisplay : MonoBehaviour
{
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawMesh(MeshData mesh, Texture2D texture)
    {
        Color color = Color.gray;

        GameObject room = new GameObject();
        room.name = "roomMesh";
        room.tag = "room";
        room.AddComponent<MeshFilter>();
        room.AddComponent<MeshRenderer>();
        room.AddComponent<MeshCollider>();
        
        
        Renderer mat = room.GetComponent<Renderer>();
        mat.sharedMaterial = new Material(Shader.Find("Standard"));
        mat.sharedMaterial.color = color;

        
        
        room.GetComponent<MeshFilter>().mesh = mesh.CreateMesh();
        room.GetComponent<MeshRenderer>().sharedMaterial.mainTexture = texture;
    }
}
