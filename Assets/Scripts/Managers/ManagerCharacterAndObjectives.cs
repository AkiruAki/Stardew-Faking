using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using PlayerStuff;
using PlantsSpaceName;

public class ManagerCharacterAndObjectives : MonoBehaviour
{
    public static ManagerCharacterAndObjectives instance { get; private set; }

    [SerializeField]
    Inventory itemsQuickInventory;
    [SerializeField]
    PlayerStats inventoryStats;
    [SerializeField]
    CharacterScript playerCharacter;
    [SerializeField]
    QuickInventoryScript quickInventory;
    [SerializeField]
    PossibleFarm[] farms;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        playerCharacter = GameObject.Find("Character").GetComponent<CharacterScript>();
        playerCharacter.AllowController(true);
        SetSlotQuickInventory(0);
        inventoryStats = new PlayerStats();
        // Automatize later (coffee, caouliflower, garlic
        quickInventory.SetQuantityItem(0, itemsQuickInventory.itemsInventory[2].quantity);
        quickInventory.SetQuantityItem(1, itemsQuickInventory.itemsInventory[3].quantity);
        quickInventory.SetQuantityItem(2, itemsQuickInventory.itemsInventory[4].quantity);
    }

    public void SetSlotQuickInventory(int i)
    {
        quickInventory.SelectSlot(i);
        playerCharacter.ReceiveItemUsing(itemsQuickInventory.itemsInventory[i]);
    }

    public void FillBucket()
    {
        for (int i = 0; i < itemsQuickInventory.itemsInventory.Length; i++)
        {
            if (itemsQuickInventory.itemsInventory[i].type == TypeItem.Bucket)
            {
                itemsQuickInventory.itemsInventory[i].quantity = 1;
                quickInventory.FillBucket();
            }
        }
    }

    public void UseBucket()
    {
        for (int i = 0; i < itemsQuickInventory.itemsInventory.Length; i++)
        {
            if ("Bucket" == itemsQuickInventory.itemsInventory[i].name)
            {
                if (itemsQuickInventory.itemsInventory[i].quantity == 1)
                    quickInventory.EmptyBucket();
                itemsQuickInventory.itemsInventory[i].quantity = 0;
            }
        }
    }

    public void UseSeed(ItemStats item, PossibleFarm farm)
    {
        for (int i = 0; i < itemsQuickInventory.itemsInventory.Length; i++)
        {
            if (item.name == itemsQuickInventory.itemsInventory[i].name)
            {
                if (item.quantity <= 0)
                    return;
                if (farm.IsFree())
                {
                    itemsQuickInventory.itemsInventory[i].quantity--;
                    quickInventory.SetQuantityItem(0, itemsQuickInventory.itemsInventory[i].quantity);
                    farm.AddSeed(PlantDictionary.instance.GiveNameSeed(itemsQuickInventory.itemsInventory[i]));
                }
            }
        }
    }

    public void PassADay()
    {
        for (int i = 0; i < farms.Length;i++)
        {
            farms[i].Grow();
        }

        playerCharacter.AllowController(true);
    }
    
    public void AllowControls()
    {
        playerCharacter.AllowController(true);
    }

    public void BlockControlls()
    {
        playerCharacter.AllowController(false);
    }

    public void OpenInventory()
    {
        quickInventory.OpenCloseAllInventory(false);
        BlockControlls();
    }

    public void AddSeedInventory(PlantName name)
    {
        string nameItem = PlantDictionary.instance.GiveNameSeed(name);

        for (int i = 0; i < itemsQuickInventory.itemsInventory.Length; i++)
        {
            if (itemsQuickInventory.itemsInventory[i].name.Contains(nameItem))
            {
                itemsQuickInventory.itemsInventory[i].quantity++;
            }
        }
    }


    public void AddItemInventory(PlantName name)
    {
        string lookFor = PlantDictionary.instance.GiveNameSeed(name);
        for (int i = 0; i < inventoryStats.inventoryPlayer.itemsInventory.Length; i++)
        {
            if (inventoryStats.inventoryPlayer.itemsInventory[i].type != TypeItem.None &&
                inventoryStats.inventoryPlayer.itemsInventory[i].name.Contains(lookFor))
            {
                inventoryStats.inventoryPlayer.itemsInventory[i].quantity++;
                quickInventory.AddVisualInventory(i,
                    inventoryStats.inventoryPlayer.itemsInventory[i].quantity);
                return;
            }
        }

        ItemStats newItem = new ItemStats(lookFor,TypeItem.Plant,1, PlantDictionary.instance.GivePlantImage(name, GrowSeedState.FullGrow));
        for (int i = 0; i < inventoryStats.inventoryPlayer.itemsInventory.Length; i++)
        {
            if (inventoryStats.inventoryPlayer.itemsInventory[i].type == TypeItem.None)
            {
                inventoryStats.inventoryPlayer.itemsInventory[i] = newItem;
                quickInventory.AddVisualInventory(i,newItem.sprite,1);
                break;
            }
        }
    }

    public void ExchangeInventorySlots(int a, int b)
    {
        ItemStats t = inventoryStats.inventoryPlayer.itemsInventory[a];

        inventoryStats.inventoryPlayer.itemsInventory[a] = inventoryStats.inventoryPlayer.itemsInventory[b];
        inventoryStats.inventoryPlayer.itemsInventory[b] = t;
    }
}
