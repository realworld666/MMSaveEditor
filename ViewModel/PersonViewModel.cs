using System;
using GalaSoft.MvvmLight;
using Newtonsoft.Json.Linq;

namespace MMSaveEditor.ViewModel
{
	/// <summary>
	/// This class contains properties that the main View can data bind to.
	/// <para>
	/// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
	/// </para>
	/// <para>
	/// You can also use Blend to data bind with the tool's support.
	/// </para>
	/// <para>
	/// See http://www.galasoft.ch/mvvm
	/// </para>
	/// </summary>
	public class PersonViewModel : ViewModelBase
	{
		private JObject _personData;

		/// <summary>
		/// Initializes a new instance of the MainViewModel class.
		/// </summary>
		public PersonViewModel()
		{
			////if (IsInDesignMode)
			////{
			////    // Code runs in Blend --> create design time data.
			////}
			////else
			////{
			////    // Code runs "for real"
			////}
		}

		public void SetModel( JObject personData )
		{
			_personData = personData;
			RaisePropertyChanged( String.Empty );
		}

		public string FirstName
		{
			get
			{
				return _personData?.GetValue( "mFirstName" ).ToString();
			}
			set
			{

			}
		}
		public string LastName
		{
			get
			{
				return _personData?.GetValue( "mLastName" ).ToString();
			}
			set
			{

			}
		}
	}
}