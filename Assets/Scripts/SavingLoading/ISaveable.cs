using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;

public interface ISaveable
{
    string SaveID { get; }
    void LoadFromData(SaveData data);
    SaveData SavedData { get; }
}
