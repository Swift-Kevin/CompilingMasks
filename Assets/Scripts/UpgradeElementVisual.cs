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
    
    void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeClicked);
    }

    public void Initialize(UpgradeElement upgrade)
    {
        upg = upgrade;
        labelName.text = upgrade.name;
        labelCost.text = "$" + upgrade.currentCost.ToString(); 
        imgIcon.sprite = upgrade.icon;
    }

    private void UpgradeClicked()
    {
        upg.currentCost = (upg.currentCost * upg.costMultiplier) + upg.addAmt;
        
        GameManager.Instance.UpdateUpgradeCost(labelName.text, upg.currentCost);        
        labelCost.text = "$" + upg.currentCost.ToString();
    }
    
    
}
