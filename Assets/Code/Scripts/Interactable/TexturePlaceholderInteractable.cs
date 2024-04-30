using System;
using UnityEngine;

public class TexturePlaceholderInteractable : MonoBehaviour, IPlaceholderInteractable
{
    public static event Action<TexturePlaceholderInteractable> OnInteractWithTexturePlaceholder;
    
    public bool IsBeingInteractedWith { get; set; }
    
    public void Interact()
    {
        IsBeingInteractedWith = true;
        OnInteractWithTexturePlaceholder?.Invoke(this);
    }

    public void ResetInteractivity()
    {
        IsBeingInteractedWith = false;
    }
}
