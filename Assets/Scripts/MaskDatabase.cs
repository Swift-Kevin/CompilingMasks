using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public enum MaskRarity
{
    Common, Uncommon, Rare, Epic
}

[Serializable]
public struct MaskEntry
{
    public string name;
    public float compilationTime;
    public float rewardPrice;
    public MaskRarity rarity;
    public Mesh model;
}
    
public class MaskDatabase : MonoBehaviour
{
    public static MaskDatabase Instance;

    public void Awake()
    {
        Instance = this;
    }
    
    [SerializeField] List<MaskEntry> maskEntries = new List<MaskEntry>();
    [SerializeField] private MaskCompiler maskCompScript;
    
    private Dictionary<MaskRarity, List<MaskEntry>> maskDB = new Dictionary<MaskRarity, List<MaskEntry>>();

    public void Init()
    {
        foreach (var entry in maskEntries)
        {
            if (!maskDB.ContainsKey(entry.rarity))
            { 
                maskDB[entry.rarity] = new List<MaskEntry>();
            }

            maskDB[entry.rarity].Add(entry);
        }
    }

    public MaskEntry GetRandomMaskFromRarity(MaskRarity rarity)
    {
        if (!maskDB.ContainsKey(rarity))
        {
            Debug.Log("Got a bad mask.");
            Debug.Log(rarity);
        
            return new MaskEntry();
        }
            
        int index = UnityEngine.Random.Range(0, maskDB[rarity].Count);
        return maskDB[rarity][index];
    }

    public MaskEntry GetRandomMask()
    {
        return GetRandomMaskFromRarity((MaskRarity)(UnityEngine.Random.Range(0, 3)));
    }
}
