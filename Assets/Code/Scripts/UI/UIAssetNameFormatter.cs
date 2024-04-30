namespace Utility
{
    public static class UIAssetNameFormatter
    {
        private static int _maxCharLength = 20;
        
        public static string FormatAssetNameForUI(string assetFileName)
        {
            assetFileName = assetFileName.Trim();
            string formattedFileName = string.Empty;
            int fileNameLength = assetFileName.Length;
        
            if (fileNameLength > _maxCharLength)
            {
                string postFix = string.Empty;
                string[] fileNameArray = assetFileName.Split(".");
                postFix = "..." + fileNameArray[^1];
                int postFixLength = postFix.Length;
                int remainingFileNameCharacterLength = _maxCharLength - postFixLength;
                formattedFileName = assetFileName.Substring(0, remainingFileNameCharacterLength) + postFix;
            }
            else
            {
                formattedFileName = assetFileName;
            }

            return formattedFileName;
        }
    }
}
