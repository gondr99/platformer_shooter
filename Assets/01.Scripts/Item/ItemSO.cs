using UnityEngine;

public enum ItemType
{
    Coin,
    Ammo
}

[CreateAssetMenu(menuName = "SO/Item/Data")]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public AmmoType ammoType;

    public int minAmount, maxAmount;
    public Collectable prefab;

    public int GetRandomAmount() => Random.Range(minAmount, maxAmount + 1);
}
