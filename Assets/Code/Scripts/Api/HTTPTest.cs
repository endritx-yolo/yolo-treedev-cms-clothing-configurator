using System.Text;
using UnityEngine;

public class HTTPTest : MonoBehaviour
{
    private void Start()
    {
        #region Showroom

        /*ShowroomController.GetBySlug("showroom-watches", (showroomModel) => { Debug.Log($"{showroomModel.Id}"); });

        ShowroomController.GetList((showroomModelList) =>
        {
            for (int i = 0; i < showroomModelList.ShowroomModelList.Count; i++)
                Debug.Log($"{showroomModelList.ShowroomModelList[i].Id}");
        });*/

        #endregion

        #region Showroom Instance

        /*ShowroomInstanceController.GetById(12, (showroomInstanceModel) =>
        {
            Debug.Log(showroomInstanceModel.UserModel.Name);
        });

        ShowroomInstanceController.GetList((showroomInstanceListDto) =>
        {
            for (int i = 0; i < showroomInstanceListDto.ShowroomInstanceModelList.Count; i++)
                Debug.Log($"{showroomInstanceListDto.ShowroomInstanceModelList[i].ShowroomModel.Name}");
        });*/

        #endregion

        #region PropTypes

        /*PropTypeController.GetById(9, (x) => { Debug.Log(x.Name); });
        
        PropTypeController.GetList((x) =>
        {
            for (int i = 0; i < x.PropTypeModelList.Count; i++)
                Debug.Log($"{x.PropTypeModelList[i].Name}");
        });*/

        #endregion

        #region Categories

        /*CategoryController.GetById(11, (x) =>
        {
            Debug.Log(x.Name);
        });
        
        CategoryController.GetList((x) =>
        {
            for (int i = 0; i < x.CategoryModelList.Count; i++)
            {
                Debug.Log(x.CategoryModelList[i].Id + " " + x.CategoryModelList[i].Name);
            }
        });*/

        #endregion

        #region Placeholder

        /*PlaceholderController.GetById(17, (x) =>
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"<color=red>Id: {x.Id}\n");
            stringBuilder.Append($"Name: {x.Name}\n");
            stringBuilder.Append($"Description: {x.Description}\n");
            stringBuilder.Append($"Key: {x.Key}</color>\n");
            stringBuilder.Append($"<color=blue>Showroom Desc: {x.ShowroomModel.Description}</color>\n");
            stringBuilder.Append($"<color=green>Asset: {x.ShowroomAssetModel.Name}</color>\n");
            stringBuilder.Append($"<color=yellow>Prop Type: {x.PropTypeModel.Name}</color>\n");
            Debug.Log(stringBuilder);
        });

        
        PlaceholderController.GetList((x) =>
        {
            for (int i = 0; i < x.PlaceholderModelList.Count; i++)
            {
                Debug.Log(x.PlaceholderModelList[i].Id + " " + x.PlaceholderModelList[i].Name);
            }
        });*/

        #endregion

        #region ShowroomAsset

        /*ShowroomAssetController.GetById(19, (x) => { Debug.Log(x.Name); });

        ShowroomAssetController.GetList((x) =>
        {
            for (int i = 0; i < x.ShowroomAssetModelList.Count; i++)
            {
                Debug.Log(x.ShowroomAssetModelList[i].Id + " " + x.ShowroomAssetModelList[i].Name);
            }
        });*/

        #endregion

        #region Get asset of prop

        /*AuthenticationController.Authenticate((bearerToken) =>
        {
            ShowroomInstanceController.GetPlaceholderListById(21, bearerToken.AccessToken, (x) =>
            {
                
            });
        });*/
        
        #endregion
    }
}