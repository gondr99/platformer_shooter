using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            var enemy = PoolManager.Instance.Pop("ToastZombie") as ZombieEnemy;

            enemy.transform.position = Vector3.zero;
        }
    }
}
