
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

using ILR;

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearningDelControls.LdFramControls
{
	/// <summary>
	/// Interaction logic for LearnerSupportFundingItem.xaml
	/// </summary>
	public partial class ucLdFamItem : UserControl, INotifyPropertyChanged
	{
		private LearningDeliveryFAM _learningDeliverFAM;
        
        #region Constructor
		public ucLdFamItem()
		{
			InitializeComponent();
			this.DataContext = this;
		}
		#endregion

		#region Public Properties
		public LearningDeliveryFAM CurrentItem
		{
			get { return _learningDeliverFAM; }
			set
			{
				if (value != null)
				{
					_learningDeliverFAM = value;
					this.DataContext = this;
					OnPropertyChanged("CurrentItem");
					OnPropertyChanged("IsTypeVisable");
					OnPropertyChanged("IsCodeReadOnly");
					OnPropertyChanged("IsCodeVisable");
					OnPropertyChanged("IsFromVisable");
					OnPropertyChanged("IsToVisable");
				}
				else
				{
					this.DataContext = null;
				}
			}
		}

		public Boolean IsTypeEnables
		{
			get
			{
				bool bRet = true;
				return bRet;
			}
		}
		public Visibility IsTypeVisable
		{
			get
			{
				Visibility v = Visibility.Visible;
				if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
				{

				}
				else
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "LSF":
							v = Visibility.Visible;
							break;
						case "HEM":
						case "LDM":
							v = Visibility.Collapsed;
							break;
						default:
							v = Visibility.Visible;
							break;
					}
				}
				return v;
			}
		}
		public Visibility IsCodeVisable
		{
			get
			{
				Visibility v = Visibility.Visible;
				if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
				{

				}
				else
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
							v = Visibility.Visible;
							break;
						case "HEM":
						case "LDM":
						case "LSF":
							v = Visibility.Collapsed;
							break;
						default:
							v = Visibility.Visible;
							break;
					}
				}
				return v;
			}
		}
		
        public bool IsCodeReadOnly
        {
            get
            {
                bool bRet = false;
                if ((_learningDeliverFAM == null) || (_learningDeliverFAM.LearnDelFAMType == null))
                {

                }
                else
                {
                    switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
                    {
                        case "ALB":
                        case "ACT":
                            bRet = false;
                            break;
                        case "LSF":
                            bRet = true;
                            break;
                        default:
                            bRet = true;
                            break;
                    }
                }
                return bRet;
            }
        }

        public Visibility IsFromVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				if (_learningDeliverFAM != null)
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "HEM":
						case "LDM":
                        case "ACT":
                        case "LSF":
							v = Visibility.Visible;
							break;
						default:
							v = Visibility.Collapsed;
							break;
					}
				}
				return v;
			}
		}
		public Visibility IsToVisable
		{
			get
			{
				Visibility v = Visibility.Collapsed;
				if (_learningDeliverFAM != null)
				{
					switch (_learningDeliverFAM.LearnDelFAMType.ToUpper())
					{
						case "ALB":
						case "HEM":
						case "LDM":
                        case "ACT":
                        case "LSF":
							v = Visibility.Visible;
							break;
						default:
							v = Visibility.Collapsed;
							break;
					}
				}
				return v;
			}
		}
		#endregion

		#region Public Methods
		#endregion

		#region PRIVATE Methods
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
