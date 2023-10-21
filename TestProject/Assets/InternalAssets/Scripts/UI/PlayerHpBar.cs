using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBar : MonoBehaviour
{
    public FloatVariable HP;
    public FloatVariable maxHP;
    public Slider slider;

    void Start()
    {
        slider.maxValue = maxHP.Value;
    }
    // Update is called once per frame
    void Update()
    {
        slider.value = HP.Value;
    }
}
