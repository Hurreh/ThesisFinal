using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RiddleLogic : MonoBehaviour
{
    public bool wasTouched = false;
    public bool wasFailed = false;
    public Renderer materialTexture;
    public ActionBasedController controllerInput;
    public GameObject controller;

    private bool isPressed = false;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player");
        controllerInput = controller.GetComponentInChildren(typeof(ActionBasedController)) as ActionBasedController;
        controllerInput.selectAction.action.performed += actionPerformed;
        materialTexture = GetComponent<Renderer>();
    }

    private void actionPerformed(InputAction.CallbackContext obj)
    {
        isPressed = !isPressed;
        Debug.Log(isPressed);
    }

    private void Update()
    {
        Debug.Log(isPressed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (isPressed)
            {
                this.materialTexture.material.color = Color.red;
                this.wasTouched = true;
                this.wasFailed = true;
            }
            else{
                this.materialTexture.material.color = Color.green;
                this.wasTouched = true;
            }
            
        }
    }
}
