using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;

namespace DemoSite.Configurations
{
    public class DataPathOptions
    {
        public string? FileHosting { get; set; }
    }

    public class DataPathConfig
    {
        private DataPathOptions _dataPathOptions;

        private const string FileHostingSubDir = "Uploads";
        public string FileHostingPath { get; }
        public DataPathConfig(DataPathOptions dataPathOptions, IWebHostEnvironment environment, ApplicationInfoConfig applicationInfo)
        {
            _dataPathOptions = dataPathOptions;
            if (!String.IsNullOrEmpty(dataPathOptions.FileHosting))
            {
                FileHostingPath = dataPathOptions.FileHosting;
                return;
            }
            
            // If not specified, use the default path.
            if (OperatingSystem.IsWindows())
                FileHostingPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), applicationInfo.ApplicationName, FileHostingSubDir);
            else if (OperatingSystem.IsLinux())
                FileHostingPath = Path.Join("/srv", applicationInfo.ApplicationName, FileHostingSubDir);
            else throw new NotImplementedException();
        }
    }
}
