using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace ilrLearnerEntry.Utils
{
	internal class LLDDListDisplayItem : INotifyPropertyChanged
	{
		internal int? _code = null;
		internal string _displayText = string.Empty;
		internal bool _isSelected = false;
		internal bool _isPrimary = false;

		#region Constructor
#if DEBUG
		public LLDDListDisplayItem()
		{
			ThrowOnInvalidPropertyName = true;
		}
#endif
		#endregion

		#region Properties
		public int? Code
		{
			set { _code = value; OnPropertyChanged("Code"); }
			get { return _code; }
		}
		public String DisplayText
		{
			set { _displayText = value; OnPropertyChanged("DisplayText"); }
			get { return _displayText; }
		}
		public Boolean IsSelected
		{
			set { _isSelected = value; OnPropertyChanged("IsSelected"); }
			get { return _isSelected; }
		}
		public Boolean IsPrimary
		{
			set { _isPrimary = value; OnPropertyChanged("IsPrimary"); }
			get { return _isPrimary; }
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
