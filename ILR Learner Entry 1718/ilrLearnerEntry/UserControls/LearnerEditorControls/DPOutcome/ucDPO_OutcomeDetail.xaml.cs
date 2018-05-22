﻿using System;
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

namespace ilrLearnerEntry.UserControls.LearnerEditorControls.DPOutcomeControls
{
    /// <summary>
    /// Interaction logic for ucDPO_OutcomeDetail.xaml
    /// </summary>
    public partial class ucDPO_OutcomeDetail : UserControl, INotifyPropertyChanged
    {
        #region Private Variables
        private ILR.DPOutcome _outcome;
        private DataTable _dt;
        private string _outCode;

        #endregion

        #region Constructor
        public ucDPO_OutcomeDetail()
        {
            InitializeComponent();
        }
        #endregion

        #region Public Properties

        public ILR.DPOutcome CurrentItem
        {
            get { return _outcome; }
            set
            {
                if (value != null)
                {
                    this.DataContext = this;
                    if (_outcome != value)
                    {
                        _outcome = value;
                        OutCode = value.OutCode.HasValue ? value.OutCode.ToString() : "";
                    }
                }
                else
                {
                    this.DataContext = null;
                }
                OnPropertyChanged("OutcomeTypeList");
                OnPropertyChanged("CurrentItem");
            }
        }

        public string OutCode
        {
            get { return _outCode; }
            set
            {
                _outCode = value;
                if (String.IsNullOrEmpty(value))
                {
                    CurrentItem.OutCode = null;
                }
                else
                {
                    int number;
                    bool result = Int32.TryParse(System.Convert.ToString(value), out number);
                    if (result)
                    {
                        CurrentItem.OutCode = number;
                    }
                }
                OnPropertyChanged("OutCode");
            }
        }

        public DataTable OutcomeTypeList
        {
            set
            {
                _dt = value;
                OnPropertyChanged("OutcomeTypeList");
            }
            get { return _dt; }
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

