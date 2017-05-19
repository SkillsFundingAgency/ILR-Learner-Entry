using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace ILR
{
    public class _base : INotifyPropertyChanged
    {
        private ILR.Schema XmlSchema = new ILR.Schema();

        internal bool _isSelected = false;

        #region Constructor
#if DEBUG
        public _base()
        {
            ThrowOnInvalidPropertyName = true;
        }
#endif
        #endregion

        #region Properties

        public Boolean IsSelected
        {
            set { 
                _isSelected = value; 
                OnPropertyChanged("IsSelected"); 
            }
            get { return _isSelected; }
        }
        #endregion
  
        #region Methods
        public int GetItemSize(string ItemName)
        {
            return XmlSchema.GetMaxLength(ItemName);
        }
        public string CheckPropertyLength(object itemValue, string ClassName, string ItemName, string Tabs)
        {
            String ItemFullName = String.Format("{0}.{1}", ClassName, ItemName);
            int ItemSize = GetItemSize(ItemFullName);
            if (itemValue != null && itemValue.ToString().Length > ItemSize)
            {
                return String.Format("{0}{1} exceeds maximum length ({2} characters). Current length : {3}\r\n", Tabs, ItemName, ItemSize, itemValue.ToString().Length);
            }
            return null;
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
            if ( PropertyChanged != null)
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
