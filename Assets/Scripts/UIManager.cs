using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum MenuID
    {
        Gameplay,
        Settings,
        Upgrade
    }
    
    public static UIManager Instance;
    
    [Header("Menus")]
    [SerializeField] GameObject menuGameplay;
    [SerializeField] GameObject menuSettings;
    [SerializeField] GameObject menuUpgrade;
    
    [Header("Buttons")]
    [SerializeField] private Button btnSettings;
    [SerializeField] private Button btnUpgrades;
    
    [Header("BTN Images")]
    [SerializeField] private Image imgSettings;
    [SerializeField] private Image imgUpgrades;

    [Header("Sprites")]
    [SerializeField] private Sprite sprSettingsOriginal;
    [SerializeField] private Sprite sprUpgradeOriginal;
    [SerializeField] private Sprite sprClose;

    MenuID currentMenu = MenuID.Gameplay;
    
    public void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        btnSettings.onClick.AddListener(() => BTNSettingsClicked());
        btnUpgrades.onClick.AddListener(() => BTNUpgradesClicked());
    }

    private void BTNSettingsClicked()
    {
        imgSettings.sprite = currentMenu != MenuID.Settings ? sprClose : sprSettingsOriginal;
        imgUpgrades.sprite = sprUpgradeOriginal;
        SwitchToMenu(MenuID.Settings);
    }
        
    private void BTNUpgradesClicked()
    {
        imgUpgrades.sprite = currentMenu != MenuID.Upgrade ? sprClose : sprUpgradeOriginal;
        imgSettings.sprite = sprSettingsOriginal;
        SwitchToMenu(MenuID.Upgrade);
    }

    private void SwitchToMenu(MenuID id)
    {
        if (currentMenu == id && id != MenuID.Gameplay)
        {
            id = MenuID.Gameplay;
        }
        
        DisableAllMenus();
        
        switch (id)
        {
            case MenuID.Gameplay:
                menuGameplay.SetActive(true);
                break;
            case MenuID.Settings:
                menuSettings.SetActive(true);
                break;
            case MenuID.Upgrade:
                menuUpgrade.SetActive(true);
                break;
        }
        
        currentMenu = id;
    }

    private void DisableAllMenus()
    {
        menuGameplay.SetActive(false);
        menuSettings.SetActive(false);
        menuUpgrade.SetActive(false);
    }
}
