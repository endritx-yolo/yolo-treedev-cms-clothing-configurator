using System;
using UnityEngine;

public interface IPlaceholder
{
    public PlaceholderModel PlaceholderModel { get; set; }
    public ShowroomAssetModel ShowroomAssetModel { get; set; }
    public PropTypeModel PropTypeModel { get; set; }

    public event Action<IPlaceholder, ShowroomAssetModel, bool> OnUpdatePlaceholder;
    public event Action<Transform> OnAssetAssigned;

    public uint Id { get; set; }

    public void UpdateAsset(ShowroomAssetModel showroomAssetModel, bool updateAssetInDatabase = false);
}