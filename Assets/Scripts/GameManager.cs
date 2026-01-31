using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
    }

    void Update()
    {
        
    }
}
