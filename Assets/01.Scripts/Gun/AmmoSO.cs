using UnityEngine;

public enum AmmoType
{
    Common,
    ShotgunShell,
    Grenade,
    Rocket,
}

[CreateAssetMenu(menuName = "SO/Gun/Ammo")]
public class AmmoSO : ScriptableObject
{
    public AmmoType type;
    public Projectile bulletPrefab;
}
