using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleLogic : MonoBehaviour
{
    public bool wasTouched = false;
    public Renderer materialTexture; 

    private void Start()
    {
        materialTexture = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            this.materialTexture.material.color = Color.green;
            this.wasTouched = true;
        }
    }
}
