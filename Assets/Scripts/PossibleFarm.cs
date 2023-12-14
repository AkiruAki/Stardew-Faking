using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlantsSpaceName;
using PlayerStuff;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PossibleFarm : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer plantInChild, wateredDirt;
    [SerializeField]
    GrowSeedState seedState;

    [SerializeField]
    PlantName plantInfo;
    bool watered;
    int daysPassedFullGrow = 0;

    public void Watering()
    {
        watered = true;
        wateredDirt.sprite = PlantDictionary.instance.WateredDirt();
        ManagerCharacterAndObjectives.instance.UseBucket();
    }

    void DryEarth()
    {
        watered = false;
        wateredDirt.sprite = null;
    }

    public void RemovePlant()
    {
        switch (seedState)
        {
            case GrowSeedState.None:
                break;
            case GrowSeedState.Seed:
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
            case GrowSeedState.Sprout:
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
            case GrowSeedState.FullGrow:
                // Add to inventory
                ManagerSaving.instance.AddToInventory();

                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;

        }
    }

    public bool IsFree()
    {
        if (seedState == GrowSeedState.None)
            return true;
        return false;
    }

    public void AddSeed(PlantName name)
    {
        seedState = GrowSeedState.Seed;
        plantInfo = name;
        plantInChild.sprite = PlantDictionary.instance.GivePlantImage(plantInfo, seedState);
    }

    public void Grow()
    {
        switch (seedState)
        {
            case GrowSeedState.None:
                break;
            case GrowSeedState.Seed:
                if (watered)
                {
                    plantInChild.sprite = PlantDictionary.instance.GivePlantImage(plantInfo, GrowSeedState.Sprout);
                    seedState = GrowSeedState.Sprout;
                }
                break;
            case GrowSeedState.Sprout:
                if (watered)
                {
                    plantInChild.sprite = PlantDictionary.instance.GivePlantImage(plantInfo, GrowSeedState.FullGrow);
                    seedState = GrowSeedState.FullGrow;
                }
                else
                {
                    plantInChild.sprite = PlantDictionary.instance.GivePlantImage(plantInfo, GrowSeedState.Dry);
                    seedState = GrowSeedState.Dry;
                }
                break;
            case GrowSeedState.FullGrow:
                daysPassedFullGrow++;
                if (daysPassedFullGrow >= 3)
                {
                    plantInChild.sprite = null;
                    daysPassedFullGrow = 0;
                    seedState = GrowSeedState.None;
                }
                break;

        }
        DryEarth();
    }

    public void ChopPlant()
    {
        switch (seedState)
        {
            case GrowSeedState.Seed:
                ManagerCharacterAndObjectives.instance.AddSeedInventory(plantInfo);
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
            case GrowSeedState.Sprout:
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
            case GrowSeedState.FullGrow:
                ManagerCharacterAndObjectives.instance.AddItemInventory(plantInfo);
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
            case GrowSeedState.Dry:
                seedState = GrowSeedState.None;
                plantInfo = PlantName.None;
                plantInChild.sprite = null;
                break;
        }
    }
}
