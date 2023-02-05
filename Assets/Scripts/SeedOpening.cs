using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeedOpening : MonoBehaviour
{
    private bool seedOpened = false;

    public void SetSeedOpened()
    {
        seedOpened = true;
    }

    public bool GetSeedOpened()
    {
        return seedOpened;
    }
}
