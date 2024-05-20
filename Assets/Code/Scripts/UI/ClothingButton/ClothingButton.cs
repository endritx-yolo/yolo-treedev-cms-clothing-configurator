using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClothingButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public event Action<ClothingButton> OnSelected;

    private GameObject _referencedModel;

    private ITweener[] _tweenerArray;

    [SerializeField] private bool _isSelected;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    
    private Texture2D _texture2D;

    #region Properties

    public bool IsSelected
    {
        get => _isSelected;
        set => _isSelected = value;
    }

    public GameObject ReferencedModel
    {
        get => _referencedModel;
        set => _referencedModel = value;
    }

    public Texture2D Texture2D
    {
        get => _texture2D;
        set
        {
            _texture2D = value;
            _rawImage.texture = Texture2D;
        }
    }

    public TextMeshProUGUI NameText
    {
        get => _nameText;
        set => _nameText = value;
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

    public void OnPointerClick(PointerEventData eventData) => Select();

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsSelected) return;
        RevertButtonState();
    }

    public void Select()
    {
        IsSelected = true;
        OnSelected?.Invoke(this);
        for (int i = 0; i < _tweenerArray.Length; i++)
            if (_tweenerArray[i] is ISelectableTweener selectableTweener)
                selectableTweener.ExecuteSelected();
    }

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