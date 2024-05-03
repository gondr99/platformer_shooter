using UnityEngine;

[CreateAssetMenu(menuName ="SO/Gun/GunData")]
public class GunDataSO : ScriptableObject
{
    public Sprite gunSprite;
    public string gunName;
    public Vector3 gunSpritePosition;
    public Vector3 firePosTrmPosition;

    public bool infiniteAmmo;

    public int damage;
    public int maxAmmo;
    public float reloadTime;
    public float cooldown;

    public float knockBackPower;
    public AmmoSO ammoData;
}
