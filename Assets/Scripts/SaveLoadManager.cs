using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoadManager
{
    bool isJetpackActive;
    float jetpackCharge;

    bool isTabletActive;
    float tabletCharge;

    (float, float) playerPosition;

    // int = index of the gameobject inside the group of filter gameobjects. only includes the active ones.
    // so when saving, only the active ones are considered and when loading, some active objects by default get deactivated to
    // recreate the state before saving.
    List<int> nofilterDeactivatedObjects; 
    List<int> redDeactivatedObjects;
    List<int> greenDeactivatedObjects;
    List<int> blueDeactivatedObjects;

}
