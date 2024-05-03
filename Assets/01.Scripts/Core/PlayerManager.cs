using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                _player = FindObjectOfType<Player>();
            }
            if (_player == null)
            {
                Debug.LogWarning("There is no player in scene, but still try access it");
            }
            return _player;
        }
    }
    public Transform PlayerTrm => Player.transform;
}
