using System.IO;

namespace CSBUnlimited.Utils.Dsv.Common
{
    internal static class CommonMethods
    {
        public static bool IsFileExistsAndNotEmpty(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
            {
                return false;
            }

            if (fileInfo.Length == 0)
            {
                return false;
            }

            if (fileInfo.Length < 10)
            {
                using (var reader = new StreamReader(filePath))
                {
                    var content = reader.ReadToEnd();
                    reader.Close();
                    return !((content?.Length ?? 0) == 0);
                }
            }

            return true;
        }
    }
}
