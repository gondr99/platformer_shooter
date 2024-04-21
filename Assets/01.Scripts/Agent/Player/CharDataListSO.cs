using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

[Serializable]
public struct Character
{
    public GunDataSO gun;
    public SpriteLibraryAsset sprites;
}

[CreateAssetMenu(menuName ="SO/CharacterData")]
public class CharDataListSO : ScriptableObject
{
    public List<Character> dataList;
}
