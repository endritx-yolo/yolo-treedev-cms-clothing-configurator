public interface IPlayerInput
{
    public bool ListenForInputs { get; set; }
    
    void EnableInput();
    void DisableInput();
}
