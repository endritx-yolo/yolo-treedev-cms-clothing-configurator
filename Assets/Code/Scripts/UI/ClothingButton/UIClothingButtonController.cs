using NaughtyAttributes;
using UnityEngine;

public class UIButtonsController : MonoBehaviour
{
    [BoxGroup("")] [SerializeField] private RectTransform _gridRectTransform;
    [BoxGroup("")] [SerializeField] private GameObject _clothingButton;

    [SerializeField] private ClothingButton[] _clothingButtonArray;

    [SerializeField] private ClothingButton _selectedClothingButton;

    private ObjectInteract _objectInteract;

    private void Awake()
    {
        _objectInteract = FindObjectOfType<ObjectInteract>();
        CreateButtons();
    }

    private void CreateButtons()
    {
        if (_objectInteract == null) return;

        for (int i = 0; i < _objectInteract.LoadedModels.Count; i++)
            Instantiate(_clothingButton, _gridRectTransform);

        _clothingButtonArray = GetComponentsInChildren<ClothingButton>();

        for (int i = 0; i < _clothingButtonArray.Length; i++)
        {
            _clothingButtonArray[i].OnSelected += SelectNewClothingButton;
            _clothingButtonArray[i].ReferencedModel = _objectInteract.LoadedModels[i];
            _clothingButtonArray[i].Texture2D = _objectInteract.ClothingModelImporter[i].ClothingTexture;
            _clothingButtonArray[i].NameText.text = _objectInteract.ClothingModelImporter[i].Name;
        }
    }

    private void SelectNewClothingButton(ClothingButton clothingButton)
    {
        _selectedClothingButton = clothingButton;

        for (int i = 0; i < _clothingButtonArray.Length; i++)
        {
            if (_clothingButtonArray[i].Equals(_selectedClothingButton)) continue;
            _clothingButtonArray[i].Deselect();
        }
    }
}