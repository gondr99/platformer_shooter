using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct DropInfo
{
    public ItemSO item;
    public float dropRate;
}

[CreateAssetMenu(menuName = "SO/Item/DropTable")]
public class DropTableSO : ScriptableObject
{
    public List<DropInfo> tables;
}
