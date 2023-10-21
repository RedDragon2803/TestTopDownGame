using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAble : MonoBehaviour
{
    public Item item;
    public Inventory playerInv;
    public int count = 1;
    public GameEvent PickUpEvent;
    
    //Время после выпадения предмета до возможности его подбора
    public float timeToUpable = 1f;
    private bool isUpable = false;

    private void Start() 
    {
        Invoke("SetUpable", timeToUpable);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isUpable)
        {
            if (playerInv.AddItem(item, count))
            {
                PickUpEvent.Raise();
                Destroy(gameObject);
            }
            else
            {
                isUpable = false;
                Invoke("SetUpable", timeToUpable);
            }
        }
    }

    private void SetUpable()
    {
        isUpable = true;
    }
}
