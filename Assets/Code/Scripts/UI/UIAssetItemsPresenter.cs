using System.Collections.Generic;
using Lean.Pool;
using Michsky.MUIP;
using NaughtyAttributes;
using UnityEngine;
using System;

public class UIAssetItemsPresenter : MonoBehaviour
{
    public static event Action OnAnyOpenMenuCanvas;
    public static event Action OnAnyCloseMenuCanvas;

    [SerializeField] private AssetItem _assetItem;
    [SerializeField] private RectTransform _parentRectTransform;

    [BoxGroup("Menu Canvas"), SerializeField]
    private CanvasGroup _mainCanvasGroup;

    [BoxGroup("Loading Graphics"), SerializeField]
    private GameObject _loadingGraphics;

    [BoxGroup("List CanvasGroup"), SerializeField]
    private CanvasGroup _listCanvasGroup;

    [BoxGroup("Close Button"), SerializeField]
    private ButtonManager _closeButton;

    private List<GameObject> _assetItemList = new List<GameObject>();

    private bool _isInMenuCanvas;

    public float MainCanvasGroupAlpha
    {
        get => _mainCanvasGroup.alpha;
        private set
        {
            _mainCanvasGroup.alpha = value;
            _mainCanvasGroup.blocksRaycasts = _mainCanvasGroup.alpha > 0f;
        }
    }

    private void Awake()
    {
        MainCanvasGroupAlpha = 0f;
        _listCanvasGroup.alpha = 0f;
    }

    private void OnEnable()
    {
        Interaction.OnAnyInteractWithPlaceholder += Interaction_OnAnyInteractWithPlaceholder;
        InteractionManager.OnAnyPlaceholderAssetUpdate += InteractionManager_OnAnyPlaceholderAssetUpdate;
        Interaction.OnAnyCancelInput += Interaction_OnAnyCancelInput;
        _closeButton.onClick.AddListener(CloseButton_OnClick);
    }

    private void OnDisable()
    {
        Interaction.OnAnyInteractWithPlaceholder -= Interaction_OnAnyInteractWithPlaceholder;
        InteractionManager.OnAnyPlaceholderAssetUpdate -= InteractionManager_OnAnyPlaceholderAssetUpdate;
        Interaction.OnAnyCancelInput -= Interaction_OnAnyCancelInput;
    }

    private void Interaction_OnAnyInteractWithPlaceholder(PlaceholderType placeholderType)
    {
        if (_isInMenuCanvas) return;
        _isInMenuCanvas = true;
        MainCanvasGroupAlpha = 1f;
        string accessToken = LoginManager.Instance.AccessToken;
        switch (placeholderType)
        {
            case PlaceholderType.Texture:
                ShowroomInstanceController.GetPublicTextureList(ShowroomManager.Instance.InstanceId, accessToken,
                    OnLoadPublicTextures);
                break;
            case PlaceholderType.Video:
                ShowroomInstanceController.GetPublicVideoList(ShowroomManager.Instance.InstanceId, accessToken,
                    OnLoadPublicTextures);
                break;
            case PlaceholderType.Model:
                ShowroomInstanceController.GetPublicModelList(ShowroomManager.Instance.InstanceId, accessToken,
                    OnLoadPublicTextures);
                break;
        }

        OnAnyOpenMenuCanvas?.Invoke();
    }

    private void InteractionManager_OnAnyPlaceholderAssetUpdate() => CloseMenu();
    private void CloseButton_OnClick() => CloseMenu();
    private void Interaction_OnAnyCancelInput() => CloseMenu();

    private void CloseMenu()
    {
        MainCanvasGroupAlpha = 0f;
        DespawnAssetItems();
        _isInMenuCanvas = false;
        OnAnyCloseMenuCanvas?.Invoke();
        _loadingGraphics.gameObject.SetActive(true);
    }

    private void OnLoadPublicTextures(List<InstancePlaceholderAssetDto> placeholderList)
    {
        if (_assetItemList.Count > 0) DespawnAssetItems();
        _loadingGraphics.gameObject.SetActive(true);
        _listCanvasGroup.alpha = 0f;
        for (int i = 0; i < placeholderList.Count; i++)
        {
            InstancePlaceholderAssetDto tempPlaceholderInstance = placeholderList[i];
            /*string iconName = tempPlaceholderInstance.ShowroomAssetModel.ImageName;
            string fileName = tempPlaceholderInstance.ShowroomAssetModel.FileName;*/
            string iconName = tempPlaceholderInstance.PlaceholderModel.ShowroomAssetModel.ImageName;
            string fileName = tempPlaceholderInstance.PlaceholderModel.ShowroomAssetModel.FileName;

            TextureLoaderManager textureLoaderManager =
                new TextureLoaderManager(fileName, iconName, tempPlaceholderInstance);
            textureLoaderManager.LoadTextureFromURL(OnLoadTexture);
        }

        _loadingGraphics.gameObject.SetActive(false);
        _listCanvasGroup.alpha = 1f;
    }

    private void OnLoadTexture(Texture2D loadedTexture, string imageName,
        InstancePlaceholderAssetDto tempPlaceholderInstance)
    {
        AssetItem assetItem = LeanPool.Spawn(_assetItem, _parentRectTransform);
        assetItem.UpdateAssetDetails(loadedTexture, imageName, tempPlaceholderInstance);
        _assetItemList.Add(assetItem.gameObject);
    }

    private void DespawnAssetItems()
    {
        for (int i = 0; i < _assetItemList.Count; i++)
            LeanPool.Despawn(_assetItemList[i]);

        _assetItemList.Clear();
    }
}