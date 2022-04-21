using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DoorLogicScript : MonoBehaviour
{
    public static DoorLogicScript current;
    public GameObject portal;
    public Renderer material;
    [SerializeField]
    public int doorCount = 0;
    [SerializeField]
    private bool portalActivated = false;

    void Start()
    {
        if (GameLogicScript.current is not null && doorCount == 1)
        {
            GameLogicScript.current.onRiddlesSolvedEvent += activatePortal;
        }
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.portalActivated)
        {
            Debug.Log("invoked portal");
            GameLogicScript.current.loadNextRound();
        }
    }
    void activatePortal()
    {
        if (doorCount == 1)
        {
            if (material != null)
            {
                material.materials[0].color = Color.green;
            }
            this.portalActivated = true;
            
        }
        
    }
}
