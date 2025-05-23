using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Object/ObjectData")]
public class MapObjectData:ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
}