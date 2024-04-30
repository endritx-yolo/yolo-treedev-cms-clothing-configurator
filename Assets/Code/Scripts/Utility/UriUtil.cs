namespace Utility
{
    public static class UriUtil
    {
        public static string FormatUri(string route) => $"{Config.CmsUri}/{route}";
        public static string FormatUriUploads(string fileId) => $"{Config.CmsStorageUploadsUri}/{fileId}";
        public static string FormatUriApi(string route) => $"{Config.CmsUriApi}/{route}";
        public static string FormatUriApi(string route, uint id) => $"{Config.CmsUriApi}/{route}/{id}";
        public static string FormatUriApi(string route, string stringId) => $"{Config.CmsUriApi}/{route}/{stringId}";

        public static string FormatUriApi(string route, uint showroomInstanceId, uint placeholderId, uint assetId) =>
            $"{Config.CmsUriApi}/{route}/{showroomInstanceId}/{placeholderId}/{assetId}";
    }
}