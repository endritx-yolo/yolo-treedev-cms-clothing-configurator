using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour, IPlayerInput
{
    [Header("Character Input Values")] public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool interact;
    public bool cancel;
    public bool toggleView;

    [Header("Movement Settings")] public bool analogMovement;

    [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void OnEnable()
    {
        UIAssetItemsPresenter.OnAnyOpenMenuCanvas += DisableInput;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas += EnableInput;
    }

    private void OnDisable()
    {
        UIAssetItemsPresenter.OnAnyOpenMenuCanvas -= DisableInput;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas -= EnableInput;
    }

    [field: SerializeField] public bool ListenForInputs { get; set; } = true;

    public void OnMove(InputValue value)
    {
        if (!ListenForInputs) return;
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (!ListenForInputs) return;
        if (cursorInputForLook)
            LookInput(value.Get<Vector2>());
    }

    public void OnJump(InputValue value)
    {
        if (!ListenForInputs) return;
        JumpInput(value.isPressed);
    }

    public void OnSprint(InputValue value)
    {
        if (!ListenForInputs) return;
        SprintInput(value.isPressed);
    }

    public void OnInteract(InputValue value)
    {
        if (!ListenForInputs) return;
        InteractInput(value.isPressed);
    }

    public void OnCancel(InputValue value)
    {
        CancelInput(value.isPressed);
    }

    public void OnToggleView(InputValue value)
    {
        if (!ListenForInputs) return;
        ToggleViewInput(value.isPressed);
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void JumpInput(bool newJumpState)
    {
        jump = newJumpState;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void InteractInput(bool newInteractState)
    {
        interact = newInteractState;
    }

    public void CancelInput(bool newCancelState)
    {
        cancel = newCancelState;
    }

    public void ToggleViewInput(bool newToggleViewState)
    {
        toggleView = newToggleViewState;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!ListenForInputs) return;
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState) =>
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;

    public void EnableInput()
    {
        cursorLocked = true;
        cursorInputForLook = true;
        ListenForInputs = true;
        SetCursorState(true);
    }

    public void DisableInput()
    {
        cursorLocked = false;
        cursorInputForLook = false;
        ListenForInputs = false;
        SetCursorState(false);
        move = Vector2.zero;
        look = Vector2.zero;
        jump = false;
        sprint = false;
    }
}