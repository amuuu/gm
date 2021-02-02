using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// PP = Post Processing

struct PPParameter
{
    public Color color;
    public float exposure;
}

struct GoggleFilter
{
    
    public string name;
    public GameObject gameObjects;
    public PPParameter parameters;

    public void Initialize(string name, GameObject gameObjects, Color color, float exposure)
    {
        this.name = name;
        
        this.gameObjects = new GameObject();
        this.gameObjects = gameObjects;
        
        this.parameters = new PPParameter();
        this.parameters.color = new Color();
        this.parameters.color = color;
        this.parameters.exposure = exposure;
    }
}

public class PlayerGoggles : MonoBehaviour
{
    private GoggleFilter[] goggleFilters;
    private GoggleFilter currentFilter;
    private int filtersNum;
    private int filterCursor;

    private GogglesView goggleView;
    private GameObject mainCamera;


    void Start()
    {
        goggleView = new GogglesView();
        
        InitCamera();
        
        filtersNum = 4;
        filterCursor = 0;
        InitGoggleFilters();

        ApplyFilter();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GoToNextFilterCursor();

            ApplyFilter();
        }
    }

    private void ApplyFilter()
    {
        currentFilter = goggleFilters[filterCursor];

        goggleView.UpdateGoggleFilterTextUI(currentFilter.name);

        ActivateCurrentFilterGameObjects();

        UpdatePostProcessingEffect();
    }

    private void UpdatePostProcessingEffect()
    {
        PostProcessVolume ppv = mainCamera.GetComponent<PostProcessVolume>();
        ColorGrading colorGrading;
        
        ppv.profile.TryGetSettings(out colorGrading);

        colorGrading.colorFilter.value = currentFilter.parameters.color;
        colorGrading.postExposure.value = currentFilter.parameters.exposure;
    }

    private void GoToNextFilterCursor()
    {
        filterCursor++;
        
        if (filterCursor == filtersNum)
            filterCursor = 0;
    }

    private void InitGoggleFilters()
    {
        goggleFilters = new GoggleFilter[filtersNum];

        // No Filter
        GoggleFilter nofilter = new GoggleFilter();
        nofilter.Initialize("NoFilter",
                            GameObject.FindGameObjectsWithTag("NoFilter")[0],
                            new Color(255.0f, 255.0f, 255.0f, 1.0f),
                            -8f);
        goggleFilters[0] = nofilter;

        // Red
        GoggleFilter red = new GoggleFilter();
        red.Initialize("Red",
                            GameObject.FindGameObjectsWithTag("Red")[0],
                            new Color(255.0f, 45.0f, 51.0f, 1.0f),
                            -6f);
        goggleFilters[1] = red;

        // Green
        GoggleFilter green = new GoggleFilter();
        green.Initialize("Green",
                            GameObject.FindGameObjectsWithTag("Green")[0],
                            new Color(98.0f, 255.0f, 83.0f, 1.0f),
                            -6f);
        goggleFilters[2] = green;

        // Blue
        GoggleFilter blue = new GoggleFilter();
        blue.Initialize("Blue",
                        GameObject.FindGameObjectsWithTag("Blue")[0],
                        new Color(9.0f, 80.0f, 255.0f, 1.0f),
                        -6f);
        goggleFilters[3] = blue;

    }

    private void ActivateCurrentFilterGameObjects()
    {
        goggleFilters[filterCursor].gameObjects.SetActive(true);

        for (int i=0; i<filtersNum; i++)
            if (i != filterCursor)
                goggleFilters[i].gameObjects.SetActive(false);
    }

    private void InitCamera()
    {
        mainCamera = gameObject.transform.GetChild(0).gameObject;
        if (mainCamera.tag != "MainCamera")
            return;
    }
}

class GogglesView
{
    GameObject textUI;
    
    public GogglesView()
    {
        textUI = GameObject.FindGameObjectsWithTag("GoggleFilterText")[0];
    }

    public void UpdateGoggleFilterTextUI(string filterName)
    {
        var filterText = textUI.GetComponent<Text>();

        filterText.text = "GOGGLE FILTER: " + filterName;
    }
}