using UnityEngine;

public class UIButtonsController : MonoBehaviour
{
    [SerializeField]private ClothingButton[] _clothingButtonArray;

    [SerializeField]private ClothingButton _selectedClothingButton;

    private void Awake()
    {
        _clothingButtonArray = GetComponentsInChildren<ClothingButton>();

        for (int i = 0; i < _clothingButtonArray.Length; i++)
        {
            _clothingButtonArray[i].OnSelected += SelectNewClothingButton;
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