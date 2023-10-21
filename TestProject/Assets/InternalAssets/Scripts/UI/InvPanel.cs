using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvPanel : MonoBehaviour
{
    public Inventory inv;
    public GameObject slotPrefab;
    private List<InventorySlot> slots = new List<InventorySlot>();
    private int SlotsWithItems;
    // Start is called before the first frame update
    void Start()
    {
        SlotsWithItems = 0;
        UpdateSlotsCount();
        setAllSlots();
    }

    void OnEnable()
    {
        UpdateInventory();
    }

    public void setAllSlots()
    {
        int index = 0;
        foreach (KeyValuePair<Item, int> item_count in inv.items)
        {
            index = SlotsWithItems;
            int count = item_count.Value;
            Item item = item_count.Key;
            AddItem(index, item, count);
        }
    }

    public void ClearSlots()
    {
        SlotsWithItems = 0;
        foreach (InventorySlot slot in slots)
        {
            slot.Clear();
        }
    }

    public void setSlotInfo(int index, Item item, int count)
    {
        slots[index].SetInfo(count, item);
        SlotsWithItems++;
    }

    public void UpdateSlotsCount()
    {
        if (slots.Count < inv.maxSlots)
        {
            for (int i = slots.Count; i < inv.maxSlots; i++)
            {
                AddEmptySlot();
            }
        }
        else if (slots.Count > inv.maxSlots)
        {
            for (int i = slots.Count; i > inv.maxSlots; i--)
            {
                DeleteSlot(slots.Count - 1);
            }
        }
    }

    public void DeleteSlot(int i)
    {
        InventorySlot deleteSlot = slots[i];
        slots.Remove(deleteSlot);
        Destroy(deleteSlot.gameObject);
    }

    public void AddEmptySlot()
    {
        GameObject newSlot = Instantiate(slotPrefab, transform);
        slots.Add(newSlot.GetComponent<InventorySlot>());
    }

    public void AddItem(int index, Item item, int count)
    {
        if (index >= slots.Count)
        {
            //нет места в инвентаре, выкидывание не поместившихся предметов на землю
        }
        else
        {
            //заполнение инвентаря определенным предметом
            if (item.MaxStack < count)
            {
                count -= item.MaxStack;
                setSlotInfo(index, item, item.MaxStack);
                index++;
                AddItem(index, item, count);
            }
            else 
            {
                setSlotInfo(index, item, count);
                index++;
            }
        }
        
    }

    public void UpdateInventory()
    {
        ClearSlots();
        setAllSlots();
    }


    public void DeleteItem(int index)
    {
        inv.RemoveItem(slots[index].GetItem(), slots[index].count);
        DeleteSlot(index);
        AddEmptySlot();
    }

    public void RemoveSlotFromList(InventorySlot slot)
    {
        slots.Remove(slot);
    }
}
