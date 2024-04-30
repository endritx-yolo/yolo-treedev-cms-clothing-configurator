using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlaceholderManager))]
public class ShowroomManager : SceneSingleton<ShowroomManager>
{
    public event Action<List<InstancePlaceholderAssetDto>> OnGetShowroomInstanceAssets;

    [field: SerializeField] public uint InstanceId { get; private set; }

    public ShowroomModel ShowroomModel { get; private set; }
    public ShowroomInstanceModel ShowroomInstanceModel { get; private set; }
    public UserModel UserModel { get; private set; }

    private WaitForSeconds _wait;

    protected override void Awake()
    {
        base.Awake();
        _wait = new WaitForSeconds(1f);
        ShowroomInstanceController.GetById(InstanceId,
            (showroomInstanceModel) =>
            {
                ShowroomInstanceModel = showroomInstanceModel;
                ShowroomModel = showroomInstanceModel.ShowroomModel;
                UserModel = showroomInstanceModel.UserModel;
            });
    }

    public void UpdateShowroomAssets() => StartCoroutine(TryUpdateShowroomAssets());

    private IEnumerator TryUpdateShowroomAssets()
    {
        while (true)
        {
            if (ShowroomInstanceModel != null)
            {
                ShowroomInstanceController.GetPlaceholderListById(InstanceId, LoginManager.Instance.AccessToken,
                    (responseObject) => OnGetShowroomInstanceAssets?.Invoke(responseObject));
                yield break;
            }

            yield return _wait;
        }
    }
}