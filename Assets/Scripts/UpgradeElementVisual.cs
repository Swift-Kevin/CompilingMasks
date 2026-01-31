using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeElementVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelName;
    [SerializeField] private TextMeshProUGUI labelCost;
    [SerializeField] private Button btnUpgrade;
    [SerializeField] private Image imgIcon;

    private float currentCost = 0;
    
    void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeClicked);
    }

    public void Initialize(UpgradeElement upgrade)
    {
        currentCost = upgrade.currentCost;
        labelName.text = upgrade.name;
        labelCost.text = "$" + upgrade.currentCost.ToString(); 
        imgIcon.sprite = upgrade.icon;
    }

    private void UpgradeClicked()
    {
        currentCost = currentCost * 5;
        
        GameManager.Instance.UpdateUpgradeCost(labelName.text, currentCost);        
        labelCost.text = "$" + currentCost.ToString();
    }
    
    
}
