using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeElementVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelName;
    [SerializeField] private TextMeshProUGUI labelCost;
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Image imgIcon;

    private UpgradeElement upg;
    public float UpgradeCost => upg.upgradeCost;
    public Button ButtonUpgrade => btnUpgrade;
    
    void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeClicked);
    }

    public void Initialize(UpgradeElement upgrade)
    {
        upg = upgrade;
        labelName.text = upgrade.name;
        labelCost.text = "$" + upgrade.upgradeCost.ToString(); 
        imgIcon.sprite = upgrade.icon;
    }

    private void UpgradeClicked()
    {
        upg.upgradeCost = (upg.upgradeCost * upg.upgradeCostMultiplier) + upg.upgradeCostAdditional;
        
        GameManager.Instance.UpdateUpgradeCost(labelName.text, upg.upgradeCost);        
        labelCost.text = "$" + upg.upgradeCost.ToString();
    }
    
    
}
