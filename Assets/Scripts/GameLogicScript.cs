using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameLogicScript : MonoBehaviour
{
    public ActionBasedController controller;
    public static GameLogicScript current;
    public AudioSource source;
    public AudioClip clipWin;
    public AudioClip clipFail;
    public MapGenerator mapGenerator;
    private GameObject[] riddles;
    public GameObject player;
    public bool allSolved;
    public bool levelLoading;
    public bool isLevelFailed;

    public event Action onRiddlesSolvedEvent;
    public event Action loadNextRoundEvent;
    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        

        mapGenerator.dungeonSizeX = SceneChangeScript.dungeonSizeX;
        mapGenerator.dungeonSizeY = SceneChangeScript.dungeonSizeY;
        mapGenerator.iterations = SceneChangeScript.iterations;
        mapGenerator.generationMode = Enum.Parse<GenerationMode>(SceneChangeScript.generationMode);
        mapGenerator.algorithmType = Enum.Parse<AlgorithmType>(SceneChangeScript.algorithmType);
        mapGenerator.DrawMapInEditor();

        controller.GetComponent<ActionBasedController>();
        controller.selectAction.action.performed += actionPerformed;
        controller.activateAction.action.performed += returnToMenu;
    }

    private void returnToMenu(InputAction.CallbackContext obj)
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        
    }

    private void actionPerformed(InputAction.CallbackContext obj)
    {
        if(mapGenerator.generationMode.ToString() != "FloorsAndWalls")
        {
            mapGenerator.DrawMapInEditor();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        checkIfAllRiddlesSolved();
    }


    void checkIfAllRiddlesSolved()
    {
        if (allSolved == false && levelLoading != true && !isLevelFailed)
        {
            this.riddles = null;
            this.riddles = GameObject.FindGameObjectsWithTag("Riddle");
            Debug.Log(this.riddles.Length);
            int solvedRiddleCount = 0;

            for (int i = 0; i < this.riddles.Length; i++)
            {
                if (this.riddles[i].GetComponent<RiddleLogic>().wasFailed)
                {
                    isLevelFailed = true;
                    source.PlayOneShot(clipFail);
                    Invoke("levelFailed", 5);
                    
                }
                if (this.riddles[i].GetComponent<RiddleLogic>().wasTouched)
                {
                    solvedRiddleCount++;
                }
            }
            if (solvedRiddleCount == this.riddles.Length && !isLevelFailed)
            {
                allSolved = true;
                riddlesSolved();
                source.PlayOneShot(clipWin);
            }
        }
        
    }
    public void riddlesSolved()
    {
        onRiddlesSolvedEvent?.Invoke();
    }
    public void loadNextRound()
    {
        this.levelLoading = true;
        this.allSolved = false;
        this.isLevelFailed = false;
        Debug.Log("loading next round");
        loadNextRoundEvent?.Invoke();
    }
    void levelFailed()
    {
        SceneManager.LoadScene(1);
    }
}
