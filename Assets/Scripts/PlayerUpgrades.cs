using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct UpgradeElement
{
    public string name;
    public Sprite icon;
    
    [Header("Buying")]
    [Tooltip("Set to initial amount")]
    public float upgradeCost;
    public float upgradeCostMultiplier;
    public float upgradeCostAdditional;
    
    [Header("Benefit")]
    [Tooltip("Set to initial amount")]
    public float upgradeResource;
    public float upgradeResourceMultiplier;
    public float upgradeResourceAdditional;
}

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField] private GameObject upgradeElementPrefab;
    [SerializeField] private Transform upgradeMenuSlotParent;
    [SerializeField] private List<UpgradeElement> upgrades;

    private List<UpgradeElementVisual> upgradeOBJs;
    
    public Dictionary<string, UpgradeElement> upgradesDB;
    public float EnrichmentValue => upgradesDB["Enrichment"].upgradeResource;

    public void Start()
    {
        upgradesDB = new Dictionary<string, UpgradeElement>();
        upgradeOBJs = new List<UpgradeElementVisual>();
        
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgradesDB[upgrades[i].name] = upgrades[i];
        }
        
        IntializeUpgrades();

        GameManager.Instance.CurrencyResource.OnChanged += ValidateUpgradePurchaseable;
    }
    
    public void IntializeUpgrades()
    {
        foreach (KeyValuePair<string, UpgradeElement> ele in upgradesDB)
        {
            UpgradeElementVisual obj = Instantiate(upgradeElementPrefab, upgradeMenuSlotParent).GetComponent<UpgradeElementVisual>();
            obj.Initialize(ele.Value); 
            upgradeOBJs.Add(obj);
        }
    }
    
    public void UpdateUpgradeCost(string _key, float _value)
    {
        if (upgradesDB.ContainsKey(_key))
        {
            UpgradeElement ele = upgradesDB[_key]; 
            ele.upgradeCost = _value;
            upgradesDB[_key] = ele;
        }
    }

    public void ValidateUpgradePurchaseable(float oldValue, float newValue)
    {
        for (int i = 0; i < upgradeOBJs.Count; i++)
        {
            bool enableBTN = newValue > upgradeOBJs[i].UpgradeCost;
            upgradeOBJs[i].ButtonUpgrade.enabled = enableBTN;
        }
    }
    
}
