using UnityEngine;

public interface IPoolable
{
    public string ItemName { get; }
    public GameObject GetGameObject();
    public void ResetItem();
}
