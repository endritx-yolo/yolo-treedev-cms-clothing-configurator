using NaughtyAttributes;
using UnityEngine;

public class PlaceholderReference : MonoBehaviour
{
    [SerializeField] private PlaceholderType _placeholderType = PlaceholderType.Texture;

    [SerializeField] private bool _isPlaceholderADifferentObject;

    [ShowIf("_isPlaceholderADifferentObject")] [SerializeField]
    private uint _placeholderId;

    private IPlaceholder _placeholder;

    #region Properties

    public IPlaceholder Placeholder
    {
        get => _placeholder;
        private set => _placeholder = value;
    }

    public PlaceholderType PlaceholderType => _placeholderType;

    public uint PlaceholderId => _placeholderId;

    #endregion

    private void Awake()
    {
        if (_isPlaceholderADifferentObject) return;
        Placeholder = GetComponentInParent<IPlaceholder>();
    }

    private void Start()
    {
        if (_isPlaceholderADifferentObject)
            _placeholder = PlaceholderManager.Instance.GetFromDictionary(_placeholderId);
    }
}