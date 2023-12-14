using PlayerStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStuff
{
    public enum TypeItem
    {
        None = 0,
        Unknown = 1,
        Bucket = 2,
        Hoe = 3,
        Seed = 4,
        Plant = 5
    }
    [Serializable]
    public class ItemStats
    {
        public string name;
        public TypeItem type;
        public int quantity;
        public Sprite sprite;
        public ItemStats(string _name, TypeItem _type, int _quantity, Sprite _sprite)
        {
            name = _name;
            type = _type;
            quantity = _quantity;
            sprite = _sprite;
        }
        public ItemStats()
        {
            name = "";
            type = TypeItem.None;
            quantity = -1;
            sprite = null;
        }
    }
    [Serializable]
    public class Inventory
    {
        public ItemStats[] itemsInventory;

        public Inventory()
        {
            itemsInventory = new ItemStats[8];
            for (int i = 0; i < 8; i++)
            {
                ItemStats n = new ItemStats();
                itemsInventory[i] = n;
            }
        }
        public Inventory(int i)
        {
            itemsInventory = new ItemStats[i];
        }
    }
    [Serializable]
    public class PlayerStats
    {
        public bool isBoy;
        public Inventory inventoryPlayer;

        public PlayerStats()
        {
            isBoy = false;
            inventoryPlayer = new Inventory();
        }

        public PlayerStats(bool _isBoy)
        {
            isBoy = _isBoy;
            inventoryPlayer = new Inventory();
        }
    }
}

public class ManagerSaving : MonoBehaviour
{
    public static ManagerSaving instance { get; private set; }

    PlayerStats statsCharacter;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AddToInventory() {

    }
}
