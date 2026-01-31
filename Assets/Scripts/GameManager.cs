using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerUpgrades upgradesStorage;

    public void Awake()
    {
        Instance = this;
    }

    public void UpdateUpgradeCost(string _key, float _value)
    {
        upgradesStorage.UpdateUpgradeCost(_key, _value);
    }

    void Update()
    {
        
    }
}
