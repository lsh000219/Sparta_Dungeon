using UnityEngine;

public class MapObject: MonoBehaviour, IInteractable
{
    public MapObjectData data;
    
    
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str;
    }

    public virtual void OnInteract(){}
}