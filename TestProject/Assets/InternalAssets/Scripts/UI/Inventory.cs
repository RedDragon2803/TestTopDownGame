using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Inventory", menuName = "TestProject/Inventory")]
public class Inventory : ScriptableObject, IDataPersistence
{
    public int maxSlots;
    public Dictionary<Item, int> items = new Dictionary<Item, int>();
    public SerializedDictionary<string, Item> namesOfItems;

    public bool AddItem(Item item, int count)
    {
        if (!items.ContainsKey(item))
        {
            if (items.Count < maxSlots)
            {
                items.Add(item, count);
                return true;
            }
            else
            {
                Debug.Log("Inventory full");
                return false;
            }
        }
        else
        {
            items[item] += count;
            return true;
        }
    }


    public bool RemoveItem(Item item, int count)
    {
        if (items.ContainsKey(item))
        {
            items[item] -= count;
            if (items[item] <= 0) items.Remove(item);
            return true;
        }

        Debug.Log("Item " + item.Name + " not found in inventory");
        return false;
    }

    public void LoadData(GameData data)
    {
        items.Clear();
        foreach (var item in data.playerInventory)
        {
            string name = item.Key;
            int count = item.Value;
            AddItem(namesOfItems[name], count);
        }
    }

    public void SaveData(GameData data)
    {
        data.playerInventory.Clear();
        foreach (var item in items)
        {
            data.playerInventory.Add(item.Key.Name, item.Value);
        }
    }
}
