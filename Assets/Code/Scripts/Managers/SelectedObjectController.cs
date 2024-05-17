using UnityEngine;

public class SelectedObjectController : MonoBehaviour
{
    [SerializeField] private GameObject _selectedObject;

    [SerializeField] private ClothingButton[] _clothingButtonArray;
    [SerializeField] private GameObject[] _referencedModelArray;

    private void Awake()
    {
        _clothingButtonArray = FindObjectsOfType<ClothingButton>();

        _referencedModelArray = new GameObject[_clothingButtonArray.Length];

        for (int i = 0; i < _clothingButtonArray.Length; i++)
        {
            _clothingButtonArray[i].OnSelected += DisplayModel;
            _referencedModelArray[i] = _clothingButtonArray[i].ReferencedModel;
        }

        if (_clothingButtonArray.Length == 0) return;
        int lastElementIndex = _clothingButtonArray.Length - 1;
        _clothingButtonArray[lastElementIndex].Select();
    }

    private void DisplayModel(ClothingButton clothingButton)
    {
        for (int i = 0; i < _referencedModelArray.Length; i++)
            _referencedModelArray[i].gameObject.SetActive(false);

        _selectedObject = clothingButton.ReferencedModel;
        Transform selectedModelTransform = _selectedObject.transform;
        selectedModelTransform.localPosition = Vector3.zero;
        _selectedObject.gameObject.SetActive(true);
    }
}