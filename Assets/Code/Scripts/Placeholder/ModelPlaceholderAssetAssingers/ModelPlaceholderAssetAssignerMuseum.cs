public class ModelPlaceholderAssetAssignerMuseum : ModelPlaceholderAssetAssigner
{
    protected override void ModelPlaceholder_OnUpdatePlaceholder(
        IPlaceholder placeholder,
        ShowroomAssetModel showroomAssetModel,
        bool updateRecordInDatabase)
    {
        if (placeholder is ModelPlaceholder modelPlaceholder)
        {
            string fileId = showroomAssetModel.Object;
            ModelLoaderManagerMuseum modelLoaderManagerMuseum =
                new ModelLoaderManagerMuseum(modelPlaceholder, fileId, showroomAssetModel.Name, this);
            modelLoaderManagerMuseum.LoadModelFromURL(() =>
                OnModelLoaded(placeholder, showroomAssetModel, updateRecordInDatabase));
        }
    }
}