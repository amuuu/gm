using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

// PP = Post Processing

public class PlayerGoggles : MonoBehaviour
{
    private string[] filters = { "NoFilter", "Red", "Green", "Blue" };
    private GameObject[] filtersObjectsArray;
    private Color[] filterColorsForPP;

    private int filterCursor;
    void Start()
    {
        filterCursor = 0;
        
        InitFilterColorsForPP();
        InitFilterObjectsArray();

        ActivateFilter("NoFilter");
        UpdateTextUI("NoFilter");
        UpdatePostProcessingEffect("NoFilter");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            string nextFilter = GetNextFilterName();
            UpdateTextUI(nextFilter);
            ActivateFilter(nextFilter);
            UpdatePostProcessingEffect(nextFilter);
        }
    }

    void UpdateTextUI(string filterName)
    {
        GameObject filterTextUI = GameObject.FindGameObjectsWithTag("GoggleFilterText")[0];

        var filterText = filterTextUI.GetComponent<Text>();

        filterText.text = "GOGGLE FILTER: " + filterName;
    }

    private string GetNextFilterName()
    {
        filterCursor++;
        if (filterCursor == filters.Length)
            filterCursor = 0;
        return filters[filterCursor];
    }

    private void ActivateFilter(string filterName)
    {
        filtersObjectsArray[filterNameToIndex(filterName)].SetActive(true);

        foreach (string filter in filters)
        {
            if (filter != filterName)
            {
                filtersObjectsArray[filterNameToIndex(filter)].SetActive(false);
            }
        }
    }

    private void InitFilterObjectsArray()
    {
        filtersObjectsArray = new GameObject[4];

        int index = 0;
        foreach (string filter in filters)
        {
            filtersObjectsArray[index] = GameObject.FindGameObjectsWithTag(filter)[0];
            index++;
        }
    }

    private int filterNameToIndex(string filterName)
    {
        return Array.IndexOf(filters, filterName); // this can be optimizaed.
    }

    private void UpdatePostProcessingEffect(string filterName)
    {
        GameObject camera = gameObject.transform.GetChild(0).gameObject;
        if (camera.tag != "MainCamera")
            return;

        PostProcessVolume ppv = camera.GetComponent<PostProcessVolume>();
        ColorGrading colorGrading;
        ppv.profile.TryGetSettings(out colorGrading);

        switch (filterName)
        {
            case "NoFilter":
                colorGrading.colorFilter.value = filterColorsForPP[0];
                colorGrading.postExposure.value = -8f;
                break;
            case "Red":
                colorGrading.colorFilter.value = filterColorsForPP[1];
                colorGrading.postExposure.value = -6f;
                break;
            case "Green":
                colorGrading.colorFilter.value = filterColorsForPP[2];
                colorGrading.postExposure.value = -6f;
                break;
            case "Blue":
                colorGrading.colorFilter.value = filterColorsForPP[3];
                colorGrading.postExposure.value = -6f;
                break;
            default:
                break;
        }
    }

    private void InitFilterColorsForPP()
    {
        filterColorsForPP = new Color[4];

        filterColorsForPP[0] = new Color(255.0f, 255.0f, 255.0f, 1.0f); // Pure white tint
        filterColorsForPP[1] = new Color(255.0f, 45.0f, 51.0f, 1.0f); // Red tint
        filterColorsForPP[2] = new Color(98.0f, 255.0f, 83.0f, 1.0f); // Green tint
        filterColorsForPP[3] = new Color(9.0f, 80.0f, 255.0f, 1.0f); // Blue tint
    }

}
