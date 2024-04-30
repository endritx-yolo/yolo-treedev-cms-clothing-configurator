using System.Collections.Generic;

public class PlaceholderManager : SceneSingleton<PlaceholderManager>
{
    private Dictionary<uint, IPlaceholder> _placeholderDictionary = new Dictionary<uint, IPlaceholder>();

    private void OnEnable() => ShowroomManager.Instance.OnGetShowroomInstanceAssets +=
        ShowroomManager_OnGetShowroomInstanceAssets;

    private void OnDisable() => ShowroomManager.Instance.OnGetShowroomInstanceAssets -=
        ShowroomManager_OnGetShowroomInstanceAssets;

    private void ShowroomManager_OnGetShowroomInstanceAssets(List<InstancePlaceholderAssetDto> instancePlaceholderAsset)
    {
        for (int i = 0; i < instancePlaceholderAsset.Count; i++)
        {
            PlaceholderModel placeholderModel = instancePlaceholderAsset[i].PlaceholderModel;
            ShowroomAssetModel showroomAssetModel = instancePlaceholderAsset[i].ShowroomAssetModel;
            IPlaceholder placeholder = GetFromDictionary(instancePlaceholderAsset[i].PlaceholderModel.Id);

            if (showroomAssetModel == null)
                showroomAssetModel = placeholderModel.ShowroomAssetModel;

            if (placeholder != null)
            {
                placeholder.PlaceholderModel = placeholderModel;
                placeholder.UpdateAsset(showroomAssetModel);
            }
        }
    }

    public void AddToDictionary(IPlaceholder placeholder) => _placeholderDictionary.TryAdd(placeholder.Id, placeholder);

    public IPlaceholder GetFromDictionary(uint id)
    {
        if (_placeholderDictionary.TryGetValue(id, out IPlaceholder placeholder))
            return placeholder;

        return null;
    }
}