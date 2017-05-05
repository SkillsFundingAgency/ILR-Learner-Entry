using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

using System.Reflection;
//using System.Reflection.Emit;


namespace ilrLearnerEntry
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Mutex mutex = new Mutex(false, "ilrLearnerEntry_L1FF99F8C-E564-47DF-A8D8-A3E9BC9A188A_1415");
        private static bool NoCheckOnStart = false;

        #region Public Methods
        public App()
        {
            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;
            ResourceDictionary myresourcedictionary_Generic = new ResourceDictionary();
            myresourcedictionary_Generic.Source = new Uri("/ilrLearnerEntry;component/StandardResourceDictionary.xaml", UriKind.RelativeOrAbsolute);
            this.Resources.MergedDictionaries.Add(myresourcedictionary_Generic);

            string Path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            System.IO.FileInfo fi = new System.IO.FileInfo(Path);
            DataFolder = fi.DirectoryName;
            ExportFolder = fi.DirectoryName;


            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if ((arg.ToUpper() == "NOCHECKONSTART") || (arg.ToUpper() == "/NOCHECKONSTART"))
                {
                    NoCheckOnStart = true;
                    break;
                }
            }
            if (!NoCheckOnStart)
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(0.01), false))
                {
                    MessageBox.Show(String.Format("You re already running a version of this application.{0} This version will close.", Environment.NewLine)
                                        , "Alreay Running a version of Application "
                                        , MessageBoxButton.OK
                                        , MessageBoxImage.Asterisk
                                        , MessageBoxResult.OK);
                    // Already running.
                    Application.Current.Shutdown();
                    return;
                }
            }
            GC.Collect();

            Application.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            ILRMessage = new ILR.Message(System.IO.Path.Combine(DataFolder, "internal.ilr"));           

        }

        #endregion

        #region Public Properties
        public static Version ApplciationVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return version;
            }
        }
        public static String ApplicationName
        {
            //get
            //{
            //    return "ILR Learner Entry";
            //}
            get
            {
                var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title.Length > 0)
                        return titleAttribute.Title;
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }

        }
        public static ILR.Message ILRMessage
        { get; set; }

        public static String DataFolder
        { get; set; }
        public static String ExportFolder
        { get; set; }

        #endregion
    }
}
