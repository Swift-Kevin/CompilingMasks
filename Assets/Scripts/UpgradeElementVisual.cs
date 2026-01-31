using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeElementVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI labelName;
    [SerializeField] private TextMeshProUGUI labelCost;
    [SerializeField] private Button btnUpgrade;
    
    void Start()
    {
        btnUpgrade.onClick.AddListener(UpgradeClicked);
    }

    private void UpgradeClicked()
    {
        
    }
    
    
}
