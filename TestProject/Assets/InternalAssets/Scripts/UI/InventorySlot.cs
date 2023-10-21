using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public Button button;
    public GameObject countText;
    public GameObject itemImage;
    public Inventory inv;
    private Item SlotItem;

    public int count { get; set; }

    public void SetInfo (int count, Item item)
    {
        this.count = count;
        text.text = count.ToString();
        image.sprite = item.sprite;
        SlotItem = item;
        button.interactable = true;
        itemImage.SetActive(true);

        if (count > 1) 
            countText.SetActive(true);
        else 
            countText.SetActive(false);
    }

    public void Clear()
    {
        button.interactable = false;
        countText.SetActive(false);
        itemImage.SetActive(false);
        SlotItem = null;
    }

    public Item GetItem()
    {
        return SlotItem;
    }

    //удалить даную €чейку и вставить пустую в конец инвентар€
    public void DeleteItem()
    {
        InvPanel invPanel = transform.parent.GetComponent<InvPanel>();
        inv.RemoveItem(SlotItem, count);
        invPanel.AddEmptySlot();
        invPanel.RemoveSlotFromList(this);
        Destroy(gameObject);
    }

}
