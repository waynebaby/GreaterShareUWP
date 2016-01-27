using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreaterShare.Models.Sharing.ShareItems;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using System.IO;
using Windows.ApplicationModel.DataTransfer;

namespace GreaterShare.Services
{
	public class DefaultShareService : IShareService
	{
		public async Task<ReceivedShareItem> GetReceivedSharedItemAsync(ShareOperation sourceOperation)
		{


			var rval = new ReceivedShareItem()
			{
				Title = sourceOperation.Data.Properties.Title,
				Description = sourceOperation.Data.Properties.Description,

				PackageFamilyName = sourceOperation.Data.Properties.PackageFamilyName,
				ContentSourceWebLink = sourceOperation.Data.Properties.ContentSourceWebLink,
				ContentSourceApplicationLink = sourceOperation.Data.Properties.ContentSourceApplicationLink,
				LogoBackgroundColor = sourceOperation.Data.Properties.LogoBackgroundColor,
				//Square30x30Logo = ,
				//dThumbnailStreamRef = sourceOperation.Data.Properties.Thumbnail,
				QuickLinkId = sourceOperation.QuickLinkId,
			};

			if (sourceOperation.Data.Properties.Square30x30Logo != null)
			{
				using (var logoStream = await sourceOperation.Data.Properties.Square30x30Logo.OpenReadAsync())
				{
					var logo = new MemoryStream();
					await logoStream.AsStreamForRead().CopyToAsync(logo);
					logo.Position = 0;
					rval.Square30x30Logo = logo;


				}
			}
			if (sourceOperation.Data.Properties.Thumbnail != null)
			{
				using (var thumbnailStream = await sourceOperation.Data.Properties.Thumbnail.OpenReadAsync())
				{
					var thumbnail = new MemoryStream();
					await thumbnailStream.AsStreamForRead().CopyToAsync(thumbnail);
					thumbnail.Position = 0;
					rval.ThumbnailStream = thumbnail;
				}
			}

			if (sourceOperation.Data.Contains(StandardDataFormats.WebLink))
			{
				try
				{
					var link = new WebLinkShareItem
					{
						WebLink = await sourceOperation.Data.GetWebLinkAsync()
					};
					rval.AvialableShareItems.Add(link);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetWebLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (sourceOperation.Data.Contains(StandardDataFormats.ApplicationLink))
			{
				try
				{
					var sharedApplicationLink = new ApplicationLinkShareItem
					{
						ApplicationLink = await sourceOperation.Data.GetApplicationLinkAsync()
					};
					rval.AvialableShareItems.Add(sharedApplicationLink);

				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetApplicationLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (sourceOperation.Data.Contains(StandardDataFormats.Text))
			{
				try
				{
					var sharedText = new TextSharedItem { Text = await sourceOperation.Data.GetTextAsync() };
					rval.AvialableShareItems.Add(sharedText);

				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetTextAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (sourceOperation.Data.Contains(StandardDataFormats.StorageItems))
			{
				try
				{
					var sharedStorageItem = new FilesShareItem
					{
						StorageItems = await sourceOperation.Data.GetStorageItemsAsync()
					};
					rval.AvialableShareItems.Add(sharedStorageItem);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetStorageItemsAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			//if (sourceOperation.Data.Contains(dataFormatName))
			//{
			//	try
			//	{
			//		this.sharedCustomData = await sourceOperation.Data.GetTextAsync(dataFormatName);
			//	}
			//	catch (Exception ex)
			//	{
			//		//NotifyUserBackgroundThread("Failed GetTextAsync(" + dataFormatName + ") - " + ex.Message, NotifyType.ErrorMessage);
			//	}
			//}
			if (sourceOperation.Data.Contains(StandardDataFormats.Html))
			{
				var sharedHtmlFormatItem = new HtmlShareItem();
				var sharedHtmlFormat = string.Empty;
				try
				{
					sharedHtmlFormat = await sourceOperation.Data.GetHtmlFormatAsync();
					sharedHtmlFormatItem.HtmlFormat = sharedHtmlFormat;
					sharedHtmlFormatItem.HtmlFragment = HtmlFormatHelper.GetStaticFragment(sharedHtmlFormat);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetHtmlFormatAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
				//try
				//{
				//	var sharedResourceMap = await sourceOperation.Data.GetResourceMapAsync();
				//}
				//catch (Exception ex)
				//{
				//	//NotifyUserBackgroundThread("Failed GetResourceMapAsync - " + ex.Message, NotifyType.ErrorMessage);
				//}

				if (sourceOperation.Data.Contains(StandardDataFormats.WebLink))
				{
					try
					{
						sharedHtmlFormatItem.WebLink = await sourceOperation.Data.GetWebLinkAsync();
					}
					catch (Exception ex)
					{
						//NotifyUserBackgroundThread("Failed GetWebLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
					}
				}
				rval.AvialableShareItems.Add(sharedHtmlFormatItem);

			}
			if (sourceOperation.Data.Contains(StandardDataFormats.Bitmap))
			{
				try
				{
					var fi = await sourceOperation.Data.GetBitmapAsync();
					using (var imgFileStream = await fi.OpenReadAsync())
					{
						var ms = new MemoryStream();
						await imgFileStream.AsStream().CopyToAsync(ms);
						ms.Position = 0;
						var sharedBitmapStreamRef = new DelayRenderedImageShareItem
						{
							SelectedImage = ms
						};

						rval.AvialableShareItems.Add(sharedBitmapStreamRef);

					}
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetBitmapAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}

			foreach (var item in rval.AvialableShareItems)
			{
				item.ContentSourceApplicationLink = rval.ContentSourceApplicationLink;
				item.ContentSourceWebLink = rval.ContentSourceWebLink;
				item.DefaultFailedDisplayText = rval.DefaultFailedDisplayText;
				item.Description = rval.Description;
				item.Title = rval.Title;
			}

			return rval;
		}


	}
}
