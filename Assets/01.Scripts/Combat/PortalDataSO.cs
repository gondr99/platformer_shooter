using UnityEngine;

[CreateAssetMenu(menuName = "SO/Combat/PortalData")]
public class PortalDataSO : ScriptableObject
{
    public int minCount = 3;
    public int maxCount = 15;
    public string enemyName;
    public float launchForce, minTime, maxTime;

    public Color portalColor;

    public int GetRandomCount() => Random.Range(minCount, maxCount + 1);
}
