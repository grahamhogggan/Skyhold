using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PeriodicSaver
{
    int saveCount { get; set; } // Property that implementing classes must provide

    void Save(); // Implicitly abstract method; classes must implement this

    void TrySave() // Default implementation
    {
        if (saveCount < SaveCaptain.count)
        {
            Save();
            saveCount = SaveCaptain.count;
        }
    }
}
