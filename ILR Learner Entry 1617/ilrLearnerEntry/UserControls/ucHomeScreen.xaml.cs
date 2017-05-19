using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;


namespace ilrLearnerEntry.UserControls
{
    /// <summary>
    /// Interaction logic for ucHomeScreen.xaml
    /// </summary>
    public partial class ucHomeScreen : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private String _windowTitle = string.Empty;
        private String _loadMessage = string.Empty;
        private String _statsMessage = string.Empty;
        
        private Boolean IsProcessing { get; set; }
        private string ImportFilename { get; set; }
        private string FilenameOnly { get; set; }
        private BackgroundWorker _workerStats;
        #endregion

        #region Constructor
        public ucHomeScreen()
        {
            InitializeComponent();
            IsProcessing = false;
            this.DataContext = this;
            OnPropertyChanged("Statistics");
            OnPropertyChanged("UKPRN");
        }
        #endregion

        #region routed events NewFileImported

        public static readonly RoutedEvent OnNewFileImportedClickEvent = EventManager.RegisterRoutedEvent("OnNewFileImportedClickEventXXVVVV", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(string));

        // Expose this event for this control's container
        public event RoutedEventHandler OnNewFileImported
        {
            add { AddHandler(OnNewFileImportedClickEvent, value); }
            remove { RemoveHandler(OnNewFileImportedClickEvent, value); }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            RaiseEvent(new RoutedEventArgs(OnNewFileImportedClickEvent, String.Empty));
        }
        #endregion

        #region routed events - UKPRN

        public static readonly RoutedEvent OnUkprnUpdateEvent = EventManager.RegisterRoutedEvent("OnUkprnUpdatedEventNam", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(string));

        // Expose this event for this control's container
        public event RoutedEventHandler OnUkprnUpdated
        {
            add { AddHandler(OnUkprnUpdateEvent, value); }
            remove { RemoveHandler(OnUkprnUpdateEvent, value); }
        }
        private void UKPRNWasUpdated(object sender, RoutedEventArgs e)
        {
            OnPropertyChanged("UKPRN");
            RaiseEvent(new RoutedEventArgs(OnUkprnUpdateEvent, String.Empty));
        }
        #endregion

        #region Public Properties
        public string WindowTitle
        {
            get { return App.ApplicationName; }
        }
        public string LogFileName
        {
            get
            {
                if ((App.LogFileName != null) && (App.LogFileName != string.Empty))
                {
                    System.IO.FileInfo fi = new System.IO.FileInfo(App.LogFileName);
                    return String.Format(" LogFile : {0}", fi.Name);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public String ApplicationVersion
        {
            get
            {
                string vReturn = string.Empty;
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                if (version != null)
                {
#if DEBUG
                    vReturn = version.ToString();
#else
                    vReturn = version.Major.ToString() + "." + version.Minor.ToString() + "." + version.Build.ToString();
#endif
                }
                else
                {
                    vReturn = "Unable to get Version.";
                }

                return vReturn;
            }
        }
        public String LoadMessage
        {
            get
            {
                return _loadMessage;
            }
            set
            {
                _loadMessage = value;
                OnPropertyChanged("LoadMessage");
            }
        }
        public String StatsMessage
        {
            get
            {
                return _statsMessage;
            }
            set
            {
                _statsMessage = value;
                OnPropertyChanged("StatsMessage");
                OnPropertyChanged("StatsMessageVisibility");
            }
        }
        public Visibility StatsMessageVisibility
        {
            get
            {
                return _statsMessage == string.Empty ? Visibility.Collapsed : Visibility.Visible;
            }
          
        }
        public int? UKPRN
        {

            get
            {
                return App.ILRMessage.LearningProvider.UKPRN;
            }
            set
            {
				int number;
				bool result = Int32.TryParse(System.Convert.ToString(value), out number);
				if (result) { App.ILRMessage.LearningProvider.UKPRN = value; }
				else { App.ILRMessage.LearningProvider.UKPRN = null; }
			}
        }
        public System.Data.DataTable Statistics
        {
            get { return App.ILRMessage.Statistics; }
        }
        #endregion

        #region Public Methods
        public void RefreshData()
        {
            // Not sure DoEvents is needed anymore?
            App.DoEvents();
            SetupBackgroundWorkerStats();
        }
        #endregion

        #region PRIVATE Methods
        private void SetupBackgroundWorkerStats()
        {
            if (_workerStats == null)
            {
                _workerStats = new BackgroundWorker();
            }
            if (!_workerStats.IsBusy)
            {
                StatsMessage = String.Format("  Refreshing Stats. Please Wait...");

                App.Log("Home Screen", "ImportWorker", "Setup Worker");
                _workerStats.DoWork += new DoWorkEventHandler(workerStats_DoWork);
                _workerStats.RunWorkerCompleted += new RunWorkerCompletedEventHandler(workerStats_RunWorkerCompleted);
                _workerStats.WorkerReportsProgress = false;
                _workerStats.WorkerSupportsCancellation = true;
                _workerStats.RunWorkerAsync();
            }
            else
            {
                App.Log("Home Screen", "ImportWorker", "Worker is Busy. ");
            }
        }
        private void workerStats_DoWork(object sender, DoWorkEventArgs e)
        {
            this.IsProcessing = true;

            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                App.Log("Home Screen", "WorkerStats", "ReFreshStats.");
                App.ILRMessage.ReFreshStats();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                e.Cancel = _workerStats.CancellationPending;
                this.IsProcessing = false;
            }
        }
        private void workerStats_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                App.Log("Home Screen", "ImportWorke r- Progress Update", "Update Here");
                // Not used just here incase!!
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void workerStats_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            App.Log("Home Screen", "WorkerStats - Complete", "");
            try
            {
                if (e.Cancelled == true)
                {
                    App.Log("Home Screen", "WorkerStats - Complete", "was Cancelled");
                    //  throw (new Exception("Cancelled Background"));
                }
                else if (e.Error != null)
                {
                    App.Log("Home Screen", "WorkerStats - Complete", "Had Error : " + e.Error.Message);
                    throw (new Exception(e.Error.Message));
                }
                else // Completed without error !!
                {
                    StatsMessage = String.Empty;
                    OnPropertyChanged("Statistics");
                    App.Log("Home Screen", "WorkerStats - Complete", "Completed OK.");
                }
            }
            catch (Exception ex)
            {
                App.Log("HomeScreen", "WorkerStats", "Error : " + String.Format("Error while  processing : {0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                if (_workerStats != null)
                {
                    if (_workerStats.IsBusy)
                    {
                        workerStats_CancelIfProcessing();
                    }
                    _workerStats.DoWork -= new DoWorkEventHandler(workerStats_DoWork);
                    _workerStats.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(workerStats_RunWorkerCompleted);
                }
                _workerStats = null;
                this.IsProcessing = false;
                StatsMessage = String.Empty;
                App.Log("Home Screen", "WorkerStats - Complete", "UI Update - End");
                App.DoEvents();
            }
        }
        private void workerStats_CancelIfProcessing()
        {
            App.Log("Home Screen", "WorkerStats - Cancel", "");
            if (_workerStats != null)
            {
                if (_workerStats.WorkerSupportsCancellation == true && _workerStats.IsBusy)
                {
                    if (!(_workerStats.CancellationPending))
                    {
                        App.Log("Home Screen", "WorkerStats - Cancel", "CancellationPending");
                        _workerStats.CancelAsync();
                    }
                }
            }
            else
            {
                this.IsProcessing = false;
            }
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            App.Log("Home Screen", "Import", "Button Click");
            MessageBoxResult result = MessageBox.Show(String.Format("Importing a new file will WIPE ALL the current data.{0}{0}Any changes to learner you have made WILL be wiped out.{0}{0}{0} Are you sure you want to do this ?", Environment.NewLine)
                                                           , "Loading Data will WIPE the current data"
                                                           , MessageBoxButton.YesNo
                                                           , MessageBoxImage.Error
                                                           , MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                App.Log("Home Screen", "Import", "Accepted Warning Message");
                LoadMessage = String.Empty;
                this.lbloadingMsg.Visibility = Visibility.Visible;
                try
                {
                    ImportFilename = ilrLearnerEntry.Utils.FileStuff.getFileName("XML |*.xml", "");
                    App.Log("Home Screen", "Import", "Selected File");
                    if (System.IO.File.Exists(ImportFilename))
                    {
                        LoadMessage = String.Format("  Loading File : {0} {1}    Please Wait...{1} {1}    This may take a few minutes depending on size of file.", FilenameOnly, Environment.NewLine);
                        OnPropertyChanged("LoadMessage");
                        
                        App.Log("Home Screen", "Import", "File Exists");
                        HideControlWhileLoadingFile(Visibility.Collapsed);
                        App.DoEvents();

                        System.IO.FileInfo fi = new System.IO.FileInfo(this.ImportFilename);
                        this.FilenameOnly = fi.Name;
                        fi = null;

                        App.DoEvents();
                        
                        App.ILRMessage.Import(ImportFilename);
                        LoadMessage = String.Format("Loading File : {0}  {1} {1}Completed.", FilenameOnly, Environment.NewLine);
                        OnPropertyChanged("LoadMessage");
                        OnPropertyChanged("UKPRN");
                        App.DoEvents();
                        RaiseEvent(new RoutedEventArgs(OnNewFileImportedClickEvent, String.Empty));
                    }
                    else
                    {   LoadMessage = String.Empty;
                        OnPropertyChanged("LoadMessage");
                        App.DoEvents();
                    }
                    OnPropertyChanged("LoadMessage");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Error while loading your file.{0}{0}{1}", Environment.NewLine, ex.Message)
                                                            , "Error loading File"
                                                            , MessageBoxButton.OK
                                                            , MessageBoxImage.Error);
                }
                finally
                {
                    HideControlWhileLoadingFile(Visibility.Visible);
                    UKPRNWasUpdated(null, null);
                }
            }
        }
        private void BackupMessagebeforeWeStart()
        {
        }
        private void ResotreMessage()
        {

        }
        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            LoadMessage = String.Format("Export Started : {0}", this.UKPRN.ToString());
            if (String.IsNullOrEmpty(this.UKPRN.ToString()))
            {
                MessageBox.Show(String.Format("UKPRN Not Supplied. {0} Please Enter UKPRN on Home Screen", Environment.NewLine)
                                                           , "UKPRN Not Supplied"
                                                           , MessageBoxButton.OK
                                                           , MessageBoxImage.Error
                                                           , MessageBoxResult.OK);

                LoadMessage = String.Empty;
            }
            else if (App.ILRMessage.LearnerExportCount == 0)
            {
                MessageBox.Show(String.Format("There are No learner marked for export and in a completed State.", Environment.NewLine)
                                                           , "Nothing to Export"
                                                           , MessageBoxButton.OK
                                                           , MessageBoxImage.Error
                                                           , MessageBoxResult.OK);

                LoadMessage = String.Empty;
            }
            else
            {
                this.lbloadingMsg.Visibility = Visibility.Visible;
                App.Log("Home Screen", "Export", "");
                using (
                var dialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    dialog.ShowNewFolderButton = true;
                    dialog.Description = "Select folder to Export file to.";
                    dialog.SelectedPath = App.ExportFolder;
                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if ((result == System.Windows.Forms.DialogResult.OK) || (result == System.Windows.Forms.DialogResult.OK))
                    {
                        App.Log("Home Screen", "Export", "Start");
                        if (App.ExportFolder != dialog.SelectedPath)
                        {
                            App.ExportFolder = dialog.SelectedPath;
                        }
                        LoadMessage = String.Format("Export Started : {0}.", this.UKPRN.ToString());
                        App.DoEvents();
                        OnPropertyChanged("LoadMessage");
                        App.ILRMessage.Save();
                        LoadMessage = String.Format("Export Started : {0}..", this.UKPRN.ToString());
                        App.DoEvents();

                        App.ILRMessage.Export(App.ExportFolder,App.ApplicationVersion.ToString());
                        LoadMessage = String.Format("Export Started : {0}...", this.UKPRN.ToString());
                       
                        App.Log("Home Screen", "Export", "Complete");
                        StatsMessage = String.Format("Export Complete..");
                        App.DoEvents();
                        OnPropertyChanged("LoadMessage");
                    }
                }
            }
            this.lbloadingMsg.Visibility = Visibility.Collapsed;

        }
        private void btnOpenExportFolder_Click(object sender, RoutedEventArgs e)
        {        
            ilrLearnerEntry.Utils.FileStuff.OpenFolderInExplorer(App.ExportFolder);
        }
        private void UserControl_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            RefreshData();
            LoadMessage = String.Empty;
            this.lbloadingMsg.Visibility = Visibility.Collapsed;
        }
        private void HideControlWhileLoadingFile(Visibility ShowOrHide)
        {
            this.dgStats.Visibility = ShowOrHide;
            this.ImportExportGrid.Visibility = ShowOrHide;
            this.UKPRNGrid.Visibility = ShowOrHide;
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// INotifyPropertyChanged requires a property called PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fires the event for the property when it changes.
        /// </summary>
        protected virtual void OnPropertyChanged(string propertyName)
        {
#if DEBUG
            VerifyPropertyName(propertyName);
#endif
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                {
                    throw new Exception(msg);
                }
                else
                {
                    Debug.Fail(msg);
                }
            }
        }

        protected bool ThrowOnInvalidPropertyName { get; set; }

        #endregion
    }
}
