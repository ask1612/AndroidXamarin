using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


/**
 * 
 * Main Activity
 * 
 * 
 */

namespace askImageYT
{
	[Activity (Label = "askImageYT", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button buttonYT = FindViewById<Button> (Resource.Id.btnYT);
			EditText edtTextYT = FindViewById<EditText> (Resource.Id.editYT);

			buttonYT.Click += delegate {
				string channelName = Convert.ToString(edtTextYT.Text);
				if (!String.IsNullOrEmpty(channelName))
				{
					Intent imageIntent = new Intent();
					imageIntent.SetClass(this, typeof(ImageYTActivity));
					imageIntent.AddFlags(ActivityFlags.NewTask);
					imageIntent.PutExtra("channel", channelName);
					StartActivity(imageIntent);
				}			
			};

		}
	}
}


