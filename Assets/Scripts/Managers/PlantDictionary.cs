using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantsSpaceName;
using PlayerStuff;

public class PlantDictionary : MonoBehaviour
{
    public static PlantDictionary instance { get; private set; }

    [SerializeField]
    ScriptablePlant[] Plants;
    [SerializeField]
    Sprite wateredDirt;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public Sprite GivePlantImage(PlantName plantName, GrowSeedState State)
    {
        switch (State)
        {
            case GrowSeedState.Seed:
                return Plants[(int)plantName - 1].seed;
            case GrowSeedState.Sprout:
                return Plants[(int)plantName - 1].sprout;
            case GrowSeedState.FullGrow:
                return Plants[(int)plantName - 1].FullGrow;
            case GrowSeedState.Dry:
                return Plants[(int)plantName - 1].DryPlant;
            default:
                return null;
        }
    }

    public Sprite WateredDirt()
    {
        return wateredDirt;
    }

    public string GiveNameSeed(PlantName name)
    {
        string lookFor = "";
        switch (name)
        {
            case PlantName.Coffee:
                lookFor = "Coffee seeds";
                break;
            case PlantName.Coliflower:
                lookFor = "Cauliflower seeds";
                break;
            case PlantName.Garlic:
                lookFor = "Garlic seeds";
                break;
        }
        return lookFor;
    }

    public PlantName GiveNameSeed(ItemStats _item)
    {
        switch (_item.name)
        {
            case "Coffee seeds":
                return PlantName.Coffee;
            case "Cauliflower seeds":
                return PlantName.Coliflower;
            case "Garlic seeds":
                return PlantName.Garlic;
            default:
                return PlantName.None;
        }
    }
}