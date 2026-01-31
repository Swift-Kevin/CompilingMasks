using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
struct UpgradeElement
{
    public string name;
    public float currentCost;
    public float costMultiplier;
}

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] List<UpgradeElement> upgrades;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
