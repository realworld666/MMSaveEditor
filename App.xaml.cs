using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace MMSaveEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string LocalLowFolderPath;

        public static App Instance;

        public App()
        {
            Instance = this;

            Guid localLowId = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
            LocalLowFolderPath = GetKnownFolderPath(localLowId);

            Localisation.LoadData();
        }



        string GetKnownFolderPath(Guid knownFolderId)
        {
            IntPtr pszPath = IntPtr.Zero;
            try
            {
                int hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, out pszPath);
                if (hr >= 0)
                    return Marshal.PtrToStringAuto(pszPath);
                throw Marshal.GetExceptionForHR(hr);
            }
            finally
            {
                if (pszPath != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(pszPath);
            }
        }

        [DllImport("shell32.dll")]
        static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out IntPtr pszPath);
    }
}
