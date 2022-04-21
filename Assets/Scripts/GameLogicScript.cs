using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapGenerator;

public class GameLogicScript : MonoBehaviour
{
    public static GameLogicScript current;
    public AudioSource source;
    public AudioClip clip;
    public MapGenerator mapGenerator;
    private GameObject[] riddles;
    public GameObject player;
    public bool allSolved;
    public bool levelLoading;

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
        
    }
    // Update is called once per frame
    void Update()
    {
        checkIfAllRiddlesSolved();
    }

    void checkIfAllRiddlesSolved()
    {
        if (allSolved == false && levelLoading != true)
        {
            this.riddles = null;
            this.riddles = GameObject.FindGameObjectsWithTag("Riddle");
            Debug.Log(this.riddles.Length);
            int solvedRiddleCount = 0;

            for (int i = 0; i < this.riddles.Length; i++)
            {
                if (this.riddles[i].GetComponent<RiddleLogic>().wasTouched)
                {
                    solvedRiddleCount++;
                }
            }
            if (solvedRiddleCount == this.riddles.Length)
            {
                allSolved = true;
                riddlesSolved();
                source.PlayOneShot(clip);
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
        Debug.Log("loading next round");
        loadNextRoundEvent?.Invoke();
    }
}
