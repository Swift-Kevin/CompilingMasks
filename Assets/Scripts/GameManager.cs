using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private PlayerUpgrades upgradesStorage;
    public float CurrencyModifier => upgradesStorage.EnrichmentValue;

    [SerializeField] private Currency currencyResource;
    public Currency CurrencyResource => currencyResource;

    [SerializeField] private MaskCompiler maskCompScript;

    public float StartingCurrencyAmt = 2500f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currencyResource.OnChanged += ResourceUpdatedCallback;
        currencyResource.Initialize(StartingCurrencyAmt);
        maskCompScript.Startup();
    }

    private void OnDestroy()
    {
        currencyResource.OnChanged -= ResourceUpdatedCallback;
    }

    private void ResourceUpdatedCallback(float oldValue, float newValue)
    {
        UIManager.Instance.UpdateCurrencyLabel(newValue);
        UIManager.Instance.UpdateUpgradesPurchaseable();
    }

    public void BuyUpgrade(string key)
    {
        upgradesStorage.ApplyUpgrade(key);
    }

    public float GetSimulationMultiplier()
    {
        float factor = 0f;

        foreach (var kvp in upgradesStorage.upgradesDB)
            factor += Mathf.Max(0f, kvp.Value.upgradeResource);

        return Mathf.Max(0.01f, factor);
    }
    
    public void UpdateUpgradeCost(string key, float unusedValue)
    {
        BuyUpgrade(key);
    }
}