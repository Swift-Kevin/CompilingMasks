using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerUpgrades upgradesStorage;
    public float CurrencyModifier => upgradesStorage.EnrichmentValue;
    
    [SerializeField] private Currency currencyResource;
    public Currency CurrencyResource => currencyResource;

    [SerializeField] private MaskCompiler maskCompScript;
    
    public void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        currencyResource.Initialize(1);
        currencyResource.OnChanged += ResourceUpdatedCallback;

        maskCompScript.Startup();
    }

    private void ResourceUpdatedCallback(float oldValue, float newValue)
    {
        UIManager.Instance.UpdateCurrencyLabel(currencyResource.CurrentValue);
        UIManager.Instance.UpdateUpgradesPurchaseable();
    }
    
    public void UpdateUpgradeCost(string _key, float _value)
    {
        upgradesStorage.UpdateUpgradeCost(_key, _value);
    }
    
    public float GetSimulationMultiplier()
    {
        float factor = 1f;

        foreach (var kvp in upgradesStorage.upgradesDB)
        {
            float value = Mathf.Max(0f, kvp.Value.upgradeResource);
            factor += value;
        }

        factor = Mathf.Max(0.01f, factor);

        return factor;
    }
}
