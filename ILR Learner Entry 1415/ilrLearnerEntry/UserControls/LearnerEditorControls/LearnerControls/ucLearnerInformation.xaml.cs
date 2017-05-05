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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.LearnerControls
{
    /// <summary>
    /// Interaction logic for ucLearnerInformation.xaml
    /// </summary>
    public partial class ucLearnerInformation : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private Learner _learner;
        #endregion

        #region Constructor
        public ucLearnerInformation()
        {
            InitializeComponent();
        }       
        #endregion

        #region Public Properties

        public Learner CurrentItem
        {
            get { return _learner; }
            set
            {
                if (value != null)
                {
                    _learner = value;
                    this.DataContext = this;
                    OnPropertyChanged("Address");
                    OnPropertyChanged("PostCode");
                    OnPropertyChanged("Telephone");
                    OnPropertyChanged("Email");
                    OnPropertyChanged("RestrictedIndList");
                    OnPropertyChanged("EthnicityList");
                    OnPropertyChanged("PriorAttainmentList");
                    OnPropertyChanged("CurrentItem");
                }
                else
                {
                    this.DataContext = null;
                }
            }
        }

        public DataTable RestrictedIndList { set; get; }
        public DataTable EthnicityList { set; get; }
        public DataTable PriorAttainmentList { set; get; }
        public DataTable DestList { set; get; }
        public PostAdd Address
        {
            get
            {
                try
                {
                    return _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                    {
                        return (lcl.LocType.Value.ToString() == "1"
                                && lcl.ContType.Value.ToString() == "2"
                                );
                    }
                     ).PostAdd;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
            set
            {
                _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                {
                    return (lcl.LocType.Value.ToString().ToLower().Contains("1")
                                || (lcl.PostAdd != null)
                            );
                }
                    ).PostAdd = value;
            }
        }
        public String PostCode
        {
            //Current 
            get
            {

                try
                {
                    return _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                    {
                        return (lcl.LocType.Value.ToString() == "2"
                                && lcl.ContType.Value.ToString() == "2"
                                );
                    }
                     ).PostCode;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return string.Empty;
                }
            }
            set
            {
                _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                {
                    return (lcl.LocType.Value.ToString().ToLower().Contains("1")
                                || (lcl.PostAdd != null)
                            );
                }
                    ).PostCode = value;
            }
        }
        public String Telephone
        {
            get
            {
                try
                {
                    return _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                    {
                        return (lcl.LocType.Value.ToString() == "3"
                                && lcl.ContType.Value.ToString() == "2"
                                );
                    }
                     ).TelNumber;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return string.Empty;
                }
            }
            set
            {
                _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                {
                    return (lcl.LocType.Value.ToString().ToLower().Contains("1")
                                || (lcl.PostAdd != null)
                            );
                }
                    ).PostCode = value;
            }
        }
        public String Email
        {
            get
            {
                try
                {

                    return _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                    {
                        return (lcl.LocType.Value.ToString() == "4"
                                && lcl.ContType.Value.ToString() == "2"
                                );
                    }
                     ).Email;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return string.Empty;
                }
            }
            set
            {
                _learner.LearnerContactList.Find(delegate(LearnerContact lcl)
                {
                    return (lcl.LocType.Value.ToString().ToLower().Contains("1")
                                || (lcl.PostAdd != null)
                            );
                }
                    ).PostCode = value;
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
