using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Http;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;


/**
 * 
 * ImageYTAdapter
 *
 */ 
namespace askImageYT
{
	public class ImageYTAdapter : BaseAdapter<askImageYT.YTImageData>
	{
		Activity _curContext;
		List<YTImageData> _lstYTImageData;


		/**
		 * Constructor
		 */ 

		public ImageYTAdapter (Activity curContext, List<YTImageData> lstYTImageData)
		{
			_curContext = curContext;
			_lstYTImageData = lstYTImageData;

		}
		/**
		 * GetItemID
		 */ 
		public override long GetItemId (int position)
		{
			return position;
		}
		/**
		 * GetView
		 */ 

		public override View GetView (int position, View customView, ViewGroup parent)
		{
			ImageView imageYTView;
			var itemYTImageData = _lstYTImageData [position];

			if (customView == null)
				customView = _curContext.LayoutInflater.Inflate (Resource.Layout.grid_view_lst_item, null);

			customView.FindViewById<TextView> (Resource.Id.txtMediaTitle).Text = itemYTImageData.imgTitle;
			string url = itemYTImageData.imgUrl;
			imageYTView = customView.FindViewById<ImageView> (Resource.Id.imgYouTube);
			imageYTView.SetMinimumHeight (300);
			imageYTView.SetScaleType (ImageView.ScaleType.CenterCrop);
			imageYTView.SetPadding (8, 8, 8, 8);
			Bitmap bmp = DownloadYTImage (url);
			imageYTView.SetImageBitmap (bmp);
			return customView;
		}
		/**
		 * Count
		 */
		public override int Count {
			get {
				return _lstYTImageData == null ? -1 : _lstYTImageData.Count;
			}
		}
		/**
		 * YTImageData
		 */ 
		public override YTImageData this [int position] {
			get {
				return _lstYTImageData == null ? null : _lstYTImageData [position];
			}
		}



		/**
		 * DownloadYTImage
		 */

		private Bitmap DownloadYTImage (string url)
		{
			if (string.IsNullOrWhiteSpace (url))
				throw new ArgumentException ("url must not be null, empty, or whitespace");

			Uri imageUri;
			if (!Uri.TryCreate (url, UriKind.Absolute, out imageUri))
				throw new ArgumentException ("Invalid url");

			try {
				WebRequest request = HttpWebRequest.Create (imageUri);
				request.Timeout = 10000;
				WebResponse response = request.GetResponse ();
				Stream inputStream = response.GetResponseStream ();
				return BitmapFactory.DecodeStream (inputStream);
			} catch (Exception) {
				return null;
			}
		}

	}

}




