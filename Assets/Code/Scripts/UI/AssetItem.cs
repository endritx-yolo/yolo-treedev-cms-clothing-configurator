using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class AssetItem : MonoBehaviour
{
    public static event Action<InstancePlaceholderAssetDto> OnAnySelectAssetItem;

    [SerializeField] private RawImage _assetImage;
    [SerializeField] private TextMeshProUGUI _assetName;

    private InstancePlaceholderAssetDto _instancePlaceholderAssetDto;
    private Button _button;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnSelectNewItem);
    }

    public void UpdateAssetDetails(Texture2D assetTexture, string assetFileName,
        InstancePlaceholderAssetDto instancePlaceholderAssetDto)
    {
        string formattedAssetName = UIAssetNameFormatter.FormatAssetNameForUI(assetFileName);
        _assetImage.texture = assetTexture;
        _assetName.text = formattedAssetName;
        _instancePlaceholderAssetDto = instancePlaceholderAssetDto;
    }

    private void OnSelectNewItem() => OnAnySelectAssetItem?.Invoke(_instancePlaceholderAssetDto);
}