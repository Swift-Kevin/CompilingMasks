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

    public void OnDestry()
    {
        GameManager.Instance.CurrencyResource.OnChanged -= ValidateUpgradePurchaseable;
    }
    
    public void IntializeUpgrades()
    {
        foreach (var kvp in upgradesDB)
        {
            string key = kvp.Key;

            UpgradeElementVisual obj = Instantiate(upgradeElementPrefab, upgradeMenuSlotParent).GetComponent<UpgradeElementVisual>();

            obj.Initialize(kvp.Value);
            obj.ButtonUpgrade.onClick.AddListener(() => GameManager.Instance.BuyUpgrade(key));

            upgradeOBJs.Add(obj);
        }
    }
    
    public void ApplyUpgrade(string key)
    {
        if (!upgradesDB.ContainsKey(key))
            return;

        UpgradeElement ele = upgradesDB[key];

        if (!GameManager.Instance.CurrencyResource.Spend(ele.upgradeCost))
            return;

        ele.upgradeResource = ele.upgradeResource * ele.upgradeResourceMultiplier + ele.upgradeResourceAdditional;
        ele.upgradeCost = ele.upgradeCost * ele.upgradeCostMultiplier + ele.upgradeCostAdditional;

        upgradesDB[key] = ele;
    }
    
    public void BuyUpgrade(string key)
    {
        if (!upgradesDB.ContainsKey(key))
            return;

        UpgradeElement ele = upgradesDB[key];

        if (!GameManager.Instance.CurrencyResource.Spend(ele.upgradeCost))
            return;

        ele.upgradeResource = ele.upgradeResource * ele.upgradeResourceMultiplier + ele.upgradeResourceAdditional;
        ele.upgradeCost = ele.upgradeCost * ele.upgradeCostMultiplier + ele.upgradeCostAdditional;

        upgradesDB[key] = ele;
    }

    public void ValidateUpgradePurchaseable(float oldValue, float newValue)
    {
        for (int i = 0; i < upgradeOBJs.Count; i++)
        {
            upgradeOBJs[i].ButtonUpgrade.interactable = newValue >= upgradeOBJs[i].UpgradeCost;
        }
    }
    
}
