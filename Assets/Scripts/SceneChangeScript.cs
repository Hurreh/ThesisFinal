using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeScript : MonoBehaviour
{
    public GameObject rightArrowA;
    public GameObject rightArrowB;
    public GameObject generationModeObj;
    public GameObject algorithmTypeObj;
    public GameObject dungeonSizeXObj;
    public GameObject dungeonSizeYObj;
    public GameObject sizeX;
    public GameObject sizeY;

    //This we pass.
    public static string generationMode;
    public static string algorithmType;
    [Range(50, 250)]
    public static int dungeonSizeX;
    [Range(50, 250)]
    public static int dungeonSizeY;
    [Range(300, 600)]
    public static int iterations;

    private int genIndex = 0;
    private int algIndex = 0;

    List<string> generationModes = new List<string>();
    List<string> algorithmTypes = new List<string>();

    private void Start()
    {
        initData();
        initFields();
    }

    private void initFields()
    {
        var gmo = this.generationModeObj.GetComponent<TextMeshProUGUI>();
        var ato = this.algorithmTypeObj.GetComponent<TextMeshProUGUI>();

        gmo.text = generationModes[0];
        generationMode = generationModes[0];
        ato.text = algorithmTypes[0];
        algorithmType = algorithmTypes[0];

        var dsX = this.dungeonSizeXObj.GetComponent<Slider>();
        var dsY = this.dungeonSizeXObj.GetComponent<Slider>();

        dsX.maxValue = 250;
        dsX.minValue = 50;
        dsY.maxValue = 250;
        dsY.minValue = 50;

        dungeonSizeX = 50;
        dungeonSizeY = 50;
        iterations = 300;

    }

    private void initData()
    {
        generationModes = Enum.GetNames(typeof(MapGenerator.GenerationMode)).ToList();
        algorithmTypes = Enum.GetNames(typeof(MapGenerator.AlgorithmType)).ToList();
    }
    public void changeGenerationMode()
    {
        var gmo = this.generationModeObj.GetComponent<TextMeshProUGUI>();
        if (gmo.text == generationModes.Last())
        {
            genIndex = 0;
            gmo.text = generationModes[genIndex];
            
        }
            
        else
        {
            gmo.text = generationModes[genIndex + 1];
            genIndex++;
        }
        generationMode = gmo.text;
    }
    public void changeAlgorithmMode()
    {
        var ato = this.algorithmTypeObj.GetComponent<TextMeshProUGUI>();
        if (ato.text == algorithmTypes.Last())
        {
            algIndex = 0;
            ato.text = algorithmTypes[algIndex];
        }

        else
        {
            ato.text = algorithmTypes[algIndex + 1];
            algIndex++;
        }
        algorithmType = ato.text;
    }
    public void changeDungeonSizeX(float size)
    {
        var sizeX = this.sizeX.GetComponent<TextMeshProUGUI>();
        dungeonSizeX = (int)size;
        sizeX.text = size.ToString();
    }
    public void changeDungeonSizeY(float size)
    {
        var sizeY = this.sizeY.GetComponent<TextMeshProUGUI>();
        dungeonSizeY = (int)size;
        sizeY.text = size.ToString();
    }
    public void ChangeScene()
    {
     
      SceneManager.LoadScene(0);
      

    }
}
