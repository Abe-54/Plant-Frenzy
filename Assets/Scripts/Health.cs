using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 30;

    public void SubtractHealth(int amount)
    {
        health -= amount;
    }
    
    public void AddHealth(int amount)
    {
        health += amount;
    }
}
