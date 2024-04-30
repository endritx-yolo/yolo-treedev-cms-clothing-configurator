using System;

public class InteractionManager : SceneSingleton<InteractionManager>
{
    public static event Action OnAnyPlaceholderAssetUpdate;

    private IPlaceholder _selectedPlaceholder;

    private void OnEnable()
    {
        Interaction.OnAnyStorePlaceholder += Interaction_OnAnyStorePlaceholder;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas += UIAssetItemsPresenter_OnAnyCloseMenuCanvas;
        AssetItem.OnAnySelectAssetItem += AssetItem_OnAnySelectAssetItem;
    }

    private void OnDisable()
    {
        Interaction.OnAnyStorePlaceholder -= Interaction_OnAnyStorePlaceholder;
        UIAssetItemsPresenter.OnAnyCloseMenuCanvas -= UIAssetItemsPresenter_OnAnyCloseMenuCanvas;
        AssetItem.OnAnySelectAssetItem -= AssetItem_OnAnySelectAssetItem;
    }

    private void Interaction_OnAnyStorePlaceholder(IPlaceholder placeholder) => _selectedPlaceholder = placeholder;
    private void UIAssetItemsPresenter_OnAnyCloseMenuCanvas() => _selectedPlaceholder = null;

    private void AssetItem_OnAnySelectAssetItem(InstancePlaceholderAssetDto instancePlaceholderAssetDto)
    {
        if (_selectedPlaceholder.ShowroomAssetModel.Id == instancePlaceholderAssetDto.PlaceholderModel.ShowroomAssetModel.Id)
            return;
        _selectedPlaceholder.UpdateAsset(instancePlaceholderAssetDto.PlaceholderModel.ShowroomAssetModel, true);
        _selectedPlaceholder = null;
        OnAnyPlaceholderAssetUpdate?.Invoke();
    }
}