using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "TestProject/Item")]
public class Item : ScriptableObject 
{
    public string Name;
    public int MaxStack;
    public Sprite sprite;
}
