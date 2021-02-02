using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public bool isJetpackActive;
    public float jetpackCharge;

    public bool isTabletActive;
    public float tabletCharge;

    public (float, float) playerPosition;

    public int currentGoggleFilterCursor;

    // int = index of the gameobject inside the group of filter gameobjects. only includes the active ones.
    // so when saving, only the active ones are considered and when loading, some active objects by default get deactivated to
    // recreate the state before saving.
    public List<int> nofilterDeactivatedObjects;
    public List<int> redDeactivatedObjects;
    public List<int> greenDeactivatedObjects;
    public List<int> blueDeactivatedObjects;

}
