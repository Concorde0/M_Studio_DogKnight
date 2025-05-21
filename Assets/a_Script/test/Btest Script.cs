using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtestScript : MonoBehaviour
{
    private AtestScript aTestScript;
    private float currentHealth;
    private void Awake()
    {
        aTestScript = GetComponent<AtestScript>();
    }

    private void GetProperty()
    {
        currentHealth = aTestScript._health;
    }
}
