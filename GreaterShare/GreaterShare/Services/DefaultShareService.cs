using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreaterShare.Models.Sharing.ShareItems;
using Windows.ApplicationModel.DataTransfer.ShareTarget;
using System.IO;
using Windows.ApplicationModel.DataTransfer;
using System.Reactive.Linq;
using Windows.Foundation;
using Windows.Storage.Streams;

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
					var str = Convert.ToBase64String(logo.ToArray());
					//rval.Square30x30LogoBase64 = Convert.ToBase64String(logo.ToArray());
					rval.Square30x30Logo = new Models.MemoryStreamBase64Item { Base64String = str };


				}
			}
			if (sourceOperation.Data.Properties.Thumbnail != null)
			{
				using (var thumbnailStream = await sourceOperation.Data.Properties.Thumbnail.OpenReadAsync())
				{
					var thumbnail = new MemoryStream();
					await thumbnailStream.AsStreamForRead().CopyToAsync(thumbnail);
					thumbnail.Position = 0;
					var str = Convert.ToBase64String(thumbnail.ToArray());
					rval.Thumbnail = new Models.MemoryStreamBase64Item { Base64String = str };
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
							SelectedImage = new Models.MemoryStreamBase64Item(ms.ToArray())
						};

						rval.AvialableShareItems.Add(sharedBitmapStreamRef);

					}
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetBitmapAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}

			//foreach (var item in rval.AvialableShareItems)
			//{
			//	//item.ContentSourceApplicationLink = rval.ContentSourceApplicationLink;
			//	//item.ContentSourceWebLink = rval.ContentSourceWebLink;
			//	//item.DefaultFailedDisplayText = rval.DefaultFailedDisplayText;
			//	//item.Description = rval.Description;
			//	//item.Title = rval.Title;
			//}

			return rval;
		}




		public async Task ShareItemAsync(ReceivedShareItem item)
		{
			//PrepareData
			if (item == null)
			{
				return;
			}

			var sqt = item?.Square30x30Logo?.GetRandowmAccessStreamAsync();
			var Square30x30Logo = sqt == null ? null : await sqt;
			var rbt = item?.Thumbnail?.GetRandowmAccessStreamAsync();
			var Thumbnail = rbt == null ? null : await rbt; ;

			//
			var dm = DataTransferManager.GetForCurrentView();

			TaskCompletionSource<object> dataTrasferedCompletion
				= new TaskCompletionSource<object>();


			using (Observable.FromEventPattern<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, DataRequestedEventArgs>
					(eh => dm.DataRequested += eh,
					eh => dm.DataRequested -= eh)
					.Subscribe(
				e =>
				{

					var def = e.EventArgs.Request.GetDeferral();
					try
					{


						//dataTrasferedCompletion.TrySetResult(null);
						var r = e.EventArgs.Request;
						r.Data.Properties.ContentSourceApplicationLink = item.ContentSourceApplicationLink;
						r.Data.Properties.ContentSourceWebLink = item.ContentSourceWebLink;
						r.Data.Properties.Description = item.Description;
						r.Data.Properties.PackageFamilyName = item.PackageFamilyName;
						if (Square30x30Logo != null)
						{
							r.Data.Properties.Square30x30Logo = RandomAccessStreamReference.CreateFromStream(Square30x30Logo);
						}
						if (Thumbnail != null)
						{
							r.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromStream(Thumbnail);

						}
						r.Data.Properties.Title = item.Title;
						foreach (var subShareItem in item.AvialableShareItems)
						{
							if (subShareItem != null)
							{
								switch (subShareItem.GetType().Name)
								{
									case nameof(ApplicationLinkShareItem):
										{
											var sitm = subShareItem as ApplicationLinkShareItem;
											r.Data.SetApplicationLink(sitm.ApplicationLink);
										}
										break;
									case nameof(CustomDataShareItem):
										{
											//var sitm = subShareItem as CustomDataShareItem;
											//r.Data.SetData
										}
										break;
									case nameof(DelayRenderedImageShareItem):
										{
											var sitm = subShareItem as DelayRenderedImageShareItem;
											r.Data.SetBitmap(RandomAccessStreamReference.CreateFromStream(sitm.SelectedImage.GetRandowmAccessStream()));
										}
										break;
									case nameof(ErrorMessageShareItem):
										{
											var sitm = subShareItem as ErrorMessageShareItem;
											r.FailWithDisplayText(sitm.CustomErrorText);
										}
										break;
									case nameof(FilesShareItem):
										{
											var sitm = subShareItem as FilesShareItem;
											r.Data.SetStorageItems(sitm.StorageItems);
										}
										break;
									case nameof(HtmlShareItem):
										{
											var sitm = subShareItem as HtmlShareItem;
											//r.Data.SetHtmlFormat(sitm.HtmlFormat);
											var fmt = HtmlFormatHelper.CreateHtmlFormat(sitm.HtmlFragment);
											r.Data.SetHtmlFormat(fmt);
										}
										break;
									//case nameof(ImagesShareItem):
									//	{
									//		var sitm = subShareItem as ImagesShareItem;
									//		r.Data.SetStorageItems(sitm.StorageItems);
									//	}
									//	break;
									case nameof(TextSharedItem):
										{
											var sitm = subShareItem as TextSharedItem;
											r.Data.SetText(sitm.Text);
										}
										break;
									case nameof(WebLinkShareItem):
										{
											var sitm = subShareItem as WebLinkShareItem;
											r.Data.SetWebLink(sitm.WebLink);
										}
										break;

									default:
										break;
								}

							}

						}
					}
					catch (Exception ex)
					{
						e.EventArgs.Request.FailWithDisplayText(ex.Message);

					}
					finally
					{
						def.Complete();
					}

				}
				))
			{
				DataTransferManager.ShowShareUI();
				await Task.WhenAny(Task.Delay(5000), dataTrasferedCompletion.Task);

			}
		}


	}


}

