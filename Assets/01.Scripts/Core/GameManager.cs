using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Portal _portalPrefab;

    private void Update()
    {
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            Vector3 pos = new Vector3(2, 2);
            Portal portal = Instantiate(_portalPrefab, pos, Quaternion.identity);

            portal.OpenPortal(pos);
        }
    }
}
