using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradeElement
{
    public string name;
    public float currentCost;
    public float costMultiplier;
    public Sprite icon;
}

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeElementPrefab;
    [SerializeField] private Transform upgradeMenuSlotParent;
    [SerializeField] private List<UpgradeElement> upgrades;
    
    private Dictionary<string, UpgradeElement> upgradesDB;

    public void Start()
    {
        upgradesDB = new Dictionary<string, UpgradeElement>();
        
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgradesDB[upgrades[i].name] = upgrades[i];
        }
        
        IntializeUpgrades();
    }
    
    public void IntializeUpgrades()
    {
        foreach (KeyValuePair<string, UpgradeElement> ele in upgradesDB)
        {
            UpgradeElementVisual obj = Instantiate(upgradeElementPrefab, upgradeMenuSlotParent).GetComponent<UpgradeElementVisual>();
            obj.Initialize(ele.Value); 
        }
    }
    
    public void UpdateUpgradeCost(string _key, float _value)
    {
        if (upgradesDB.ContainsKey(_key))
        {
            UpgradeElement ele = upgradesDB[_key]; 
            ele.currentCost = _value;
            upgradesDB[_key] = ele;
        }
    }
    
}
