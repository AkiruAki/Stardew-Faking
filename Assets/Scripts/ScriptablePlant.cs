using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantsSpaceName;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class ScriptablePlant : ScriptableObject
{
    public PlantName namePlant;
    public Sprite seed;
    public Sprite sprout;
    public Sprite FullGrow;
    public Sprite DryPlant;
}
namespace PlantsSpaceName
{ 
    public enum PlantName
    {
        None,
        Coffee,
        Coliflower,
        Garlic
    }
    public enum GrowSeedState
    {
        None = 0,
        Seed = 1,
        Sprout = 2,
        FullGrow = 3,
        Dry = 4
    }
}