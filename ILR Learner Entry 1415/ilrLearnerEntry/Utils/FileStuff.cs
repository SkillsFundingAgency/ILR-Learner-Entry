using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ilrLearnerEntry.Utils
{
    public static class FileStuff
    {

        #region File Stuff
        public static string getFileName(String Filter, String InitialDirectory)
        {
            string fileLocation = string.Empty;
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.InitialDirectory = InitialDirectory;
                openFileDialog.Multiselect = false;
                openFileDialog.AutoUpgradeEnabled = true;
                openFileDialog.Filter = Filter;

                System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    fileLocation = openFileDialog.FileName;
                }
                return fileLocation;
            }
        }

        public static void OpenFolderInExplorer(String folder)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
            {
                FileName = folder,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        #endregion

    }
}
