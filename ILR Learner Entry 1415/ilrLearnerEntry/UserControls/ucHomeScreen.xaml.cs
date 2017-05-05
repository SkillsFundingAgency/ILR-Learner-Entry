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
		#endregion
		#region Constructor
		public ucHomeScreen()
		{
			InitializeComponent();
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
		public int? UKPRN
		{

			get
			{
				return App.ILRMessage.LearningProvider.UKPRN;
			}
			set
			{
				App.ILRMessage.LearningProvider.UKPRN = value;
				UKPRNWasUpdated(null, null);
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
			OnPropertyChanged("Statistics");
		}


		#endregion

		#region PRIVATE Methods
		private void btnImport_Click(object sender, RoutedEventArgs e)
		{

			MessageBoxResult result = MessageBox.Show(String.Format("Importing a new file will WIPE ALL the current data.{0}{0}Any changes to learner you have made will be wiped out.{0}{0}{0} 1314 Learner Destination and Progression {0}   are NOT Migrated over.{0}{0}{0} Are you sure you want to do this ?", Environment.NewLine)
														   , "Loading Data will WIPE the current data"
														   , MessageBoxButton.YesNo
														   , MessageBoxImage.Error
														   , MessageBoxResult.No);
			if (result == MessageBoxResult.Yes)
			{
				LoadMessage = String.Empty;
				this.lbloadingMsg.Visibility = Visibility.Visible;

				try
				{
					string ImportFilename = ilrLearnerEntry.Utils.FileStuff.getFileName("XML |*.xml", "");
					if (System.IO.File.Exists(ImportFilename))  
					{
						
						this.dgStats.Visibility = Visibility.Collapsed;
						
						System.IO.FileInfo fi = new System.IO.FileInfo(ImportFilename);
						String FilenameOnly = fi.Name;
						fi = null;

						LoadMessage = String.Format("Loading File : {0} - Please Wait...", FilenameOnly);
						OnPropertyChanged("LoadMessage");

						App.ILRMessage.Import(ImportFilename);

						LoadMessage = String.Format("Loading File : {0} - Complete.", FilenameOnly);
						OnPropertyChanged("LoadMessage");
						RaiseEvent(new RoutedEventArgs(OnNewFileImportedClickEvent, String.Empty));
					}
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
					this.dgStats.Visibility = Visibility.Visible;
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
			if (String.IsNullOrEmpty(this.UKPRN.ToString()))
			{
				MessageBox.Show(String.Format("UKPRN Not Supplied. {0} Please Enter UKPRN on Home Screen", Environment.NewLine)
														   , "UKPRN Not Supplied"
														   , MessageBoxButton.OK
														   , MessageBoxImage.Error
														   , MessageBoxResult.OK);

			}
			else
			{
				using (
				var dialog = new System.Windows.Forms.FolderBrowserDialog())
				{
					dialog.ShowNewFolderButton = true;
					dialog.Description = "Select folder to Export file to.";
					dialog.SelectedPath = App.ExportFolder;
					System.Windows.Forms.DialogResult result = dialog.ShowDialog();
					if ((result == System.Windows.Forms.DialogResult.OK) || (result == System.Windows.Forms.DialogResult.OK))
					{
						if (App.ExportFolder != dialog.SelectedPath)
						{
							App.ExportFolder = dialog.SelectedPath;
						}
						App.ILRMessage.Save();
						App.ILRMessage.Export(App.ExportFolder);
					}
				}
			}
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
