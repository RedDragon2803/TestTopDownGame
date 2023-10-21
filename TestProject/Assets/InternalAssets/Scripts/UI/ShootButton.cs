using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BoolVariable ShootInput;

    void Start()
    {
        ShootInput.SetValue(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ShootInput.SetValue(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ShootInput.SetValue(false);
    }
}
