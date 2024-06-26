using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;
    
    public Sprite Icon;

    public int Price;

    public int Value;
}
