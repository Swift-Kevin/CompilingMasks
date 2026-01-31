using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradeElement
{
    public string name;
    public float currentCost;
    public float costMultiplier;
}

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeElementPrefab;
    [SerializeField] private List<UpgradeElement> upgrades;
    public List<UpgradeElement> Upgrades => upgrades;

    [SerializeField] private Transform upgradeMenuSlotParent;

    public void Start()
    {
        IntializeUpgrades();
    }
    
    public void IntializeUpgrades()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject obj = Instantiate(upgradeElementPrefab, upgradeMenuSlotParent);
            
        }
    }
    
}
