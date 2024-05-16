using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClothingButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public event Action<ClothingButton> OnSelected;
    
    private ITweener[] _tweenerArray;

    private bool _isSelected;

    #region Properties

    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }

    #endregion

    private void Awake()
    {
        _tweenerArray = GetComponentsInChildren<ITweener>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsSelected) return;
        for (int i = 0; i < _tweenerArray.Length; i++)
            _tweenerArray[i].Execute();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelected = true;
        OnSelected?.Invoke(this);
        for (int i = 0; i < _tweenerArray.Length; i++)
        {
            if (_tweenerArray[i] is ISelectableTweener selectableTweener)
                selectableTweener.ExecuteSelected();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelected) return;
        RevertButtonState();
    }

    [Button("Deselect")]
    public void Deselect()
    {
        IsSelected = false;
        RevertButtonState();
    }

    private void RevertButtonState()
    {
        for (int i = 0; i < _tweenerArray.Length; i++)
        {
            if (_tweenerArray[i] is IReverseTweener reverseTweener)
                reverseTweener.Revert();
        }
    }
}