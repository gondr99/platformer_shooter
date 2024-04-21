using UnityEngine;

public static class TransformExtenstions
{
    public static void GGM(this Transform trm, float x, float y)
    {
        Debug.Log(trm.position + new Vector3(x, y));
    }
}
