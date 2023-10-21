using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseClickedOutside : MonoBehaviour, IDeselectHandler
{
    void Awake() 
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
    // Start is called before the first frame update
    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
