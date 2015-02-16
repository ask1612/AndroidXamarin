
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using Java.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;


/**
 * 
 *ImageYTActivity
 *
 */
namespace askImageYT
{
	[Activity (Label = "askImageYTActivity")]			
	public class ImageYTActivity : Activity
	{
		public GridView _gridView;
		string _url;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Set our view from the "grid_view_lst" layout resource
			SetContentView (Resource.Layout.grid_view_lst);	
			_gridView = FindViewById<GridView> (Resource.Id.gridView);
			var channel = Intent.Extras.GetString ("channel");
			_url =String.Format( "http://gdata.youtube.com/feeds/api/videos?vq={0}" +
				"&alt=json&orderby=rating&start-index=11&max-results=10",channel);
			WebClient web = new WebClient();
			web.DownloadStringCompleted += (s, e) =>
			{
				var text = e.Result; // get the downloaded data
				RunOnUiThread(() =>
					{
						List<YTImageData> lstYTImageData = new  List<YTImageData> ();
						string json=text;
						JObject jsnObj = JObject.Parse(json);
						var jCollection = jsnObj["feed"]["entry"].Children();
						var data = from p in jCollection
							select p;
						foreach (JObject p in data)
						{
							YTImageData dataN = new YTImageData();
							dataN.imgUrl = (string)p["media$group"]["media$thumbnail"][1]["url"];
							dataN.imgTitle=(string)p["media$group"]["media$title"]["$t"];
							lstYTImageData.Add(dataN);
						}
						_gridView.Adapter = new ImageYTAdapter (this,lstYTImageData);
					});
			};
			var uri = new Uri(_url); // Html home page
			web.Encoding = Encoding.UTF8;
			web.DownloadStringAsync(uri);
		}
	}
}
