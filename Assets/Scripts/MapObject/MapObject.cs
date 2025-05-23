using UnityEngine;

public class MapObject: MonoBehaviour, IInteractable  //맵에 있는 특정 오브젝트에 대한 정보를 담음
{
    public MapObjectData data;
    
    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public virtual void OnInteract(){}
}