using UnityEngine;

public class Coin : Collectable, IPoolable
{
    public string ItemName => "Coin";

    public override void Collect()
    {
        if (_alreadyCollected) return;

        _alreadyCollected = true;
        int amount = _itemData.GetRandomAmount();
        PlayerManager.Instance.AddCoin(amount);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ResetItem()
    {
        _alreadyCollected = false;
    }
}
