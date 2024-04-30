public interface IPlaceholderInteractable
{
    public bool IsBeingInteractedWith  { get; set; }
    
    void Interact();
    void ResetInteractivity();
}
