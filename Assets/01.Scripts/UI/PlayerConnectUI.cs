using System.Collections;
using UnityEngine;

public abstract class PlayerConnectUI : MonoBehaviour
{
    protected Player _player;

    protected virtual void Start()
    {
        StartCoroutine(FindPlayerCoroutine());
    }

    protected IEnumerator FindPlayerCoroutine()
    {
        yield return new WaitUntil(() => PlayerManager.Instance.Player != null);
        _player = PlayerManager.Instance.Player;

        AfterFindPlayer(); 
    }

    public abstract void AfterFindPlayer();

}
