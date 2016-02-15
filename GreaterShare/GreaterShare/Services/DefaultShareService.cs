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
using Windows.Storage.AccessCache;
using Windows.Storage;
using System.Collections.ObjectModel;
using MVVMSidekick.Reactive;
using MVVMSidekick.ViewModels;

namespace GreaterShare.Services
{
	public class DefaultShareService : IShareService
	{


		public async Task<ReceivedShareItem> GetReceivedSharedItemAsync(ShareOperation sourceOperation)
		{
			var packageView = sourceOperation.Data;
			ReceivedShareItem rval = await FetchDataFromPackageViewAsync(packageView);
			rval.QuickLinkId = sourceOperation.QuickLinkId;
			return rval;
		}

		private static async Task<ReceivedShareItem> FetchDataFromPackageViewAsync(DataPackageView packageView)
		{
			var rval = new ReceivedShareItem()
			{
				Title = packageView.Properties.Title,
				Description = packageView.Properties.Description,
				PackageFamilyName = packageView.Properties.PackageFamilyName,
				ContentSourceWebLink = packageView.Properties.ContentSourceWebLink,
				ContentSourceApplicationLink = packageView.Properties.ContentSourceApplicationLink,
				LogoBackgroundColor = packageView.Properties.LogoBackgroundColor,

			};
			if (packageView.Properties.Square30x30Logo != null)
			{
				using (var logoStream = await packageView.Properties.Square30x30Logo.OpenReadAsync())
				{
					var logo = new MemoryStream();
					await logoStream.AsStreamForRead().CopyToAsync(logo);
					logo.Position = 0;
					var str = Convert.ToBase64String(logo.ToArray());
					//rval.Square30x30LogoBase64 = Convert.ToBase64String(logo.ToArray());
					rval.Square30x30Logo = new Models.MemoryStreamBase64Item { Base64String = str };


				}
			}
			if (packageView.Properties.Thumbnail != null)
			{
				using (var thumbnailStream = await packageView.Properties.Thumbnail.OpenReadAsync())
				{
					var thumbnail = new MemoryStream();
					await thumbnailStream.AsStreamForRead().CopyToAsync(thumbnail);
					thumbnail.Position = 0;
					var str = Convert.ToBase64String(thumbnail.ToArray());
					rval.Thumbnail = new Models.MemoryStreamBase64Item { Base64String = str };
				}
			}

			if (packageView.Contains(StandardDataFormats.WebLink))
			{
				try
				{
					var link = new WebLinkShareItem
					{
						WebLink = await packageView.GetWebLinkAsync()
					};
					rval.AvialableShareItems.Add(link);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetWebLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (packageView.Contains(StandardDataFormats.ApplicationLink))
			{
				try
				{
					var sharedApplicationLink = new ApplicationLinkShareItem
					{
						ApplicationLink = await packageView.GetApplicationLinkAsync()
					};
					rval.AvialableShareItems.Add(sharedApplicationLink);

				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetApplicationLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (packageView.Contains(StandardDataFormats.Text))
			{
				try
				{
					var sharedText = new TextSharedItem { Text = await packageView.GetTextAsync() };
					rval.AvialableShareItems.Add(sharedText);
					rval.Text = await packageView.GetTextAsync();
					rval.GetValueContainer(x => x.Text)
					   	.GetNullObservable()
						.Subscribe(e => sharedText.Text = rval.Text)
						.DisposeWith(rval);
					sharedText.GetValueContainer(x => x.Text)
						.GetNullObservable()
						.Subscribe(e => rval.Text = sharedText.Text)
						.DisposeWith(rval);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetTextAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			if (packageView.Contains(StandardDataFormats.StorageItems))
			{
				try
				{
					var files = await packageView.GetStorageItemsAsync();
					var sharedStorageItem = new FilesShareItem
					{
						StorageFiles = new ObservableCollection<FileItem>()
						//StorageItems = 
					};
					foreach (StorageFile sf in files)
					{
						var guidString = Guid.NewGuid().ToString();
						StorageApplicationPermissions.FutureAccessList.AddOrReplace(guidString, sf, sf.Name);
						var ts = await sf.GetScaledImageAsThumbnailAsync(Windows.Storage.FileProperties.ThumbnailMode.VideosView);
						var tmbs = new MemoryStream();
						await ts.AsStreamForRead().CopyToAsync(tmbs);
						var file = new FileItem
						{
							AccessToken = guidString,
							ContentType = sf.ContentType,
							FileName = sf.DisplayName,
							PossiblePath = sf.Path,
							Thumbnail = new Models.MemoryStreamBase64Item(tmbs.ToArray())
						};

						sharedStorageItem.StorageFiles.Add(file);

					}
					//StorageApplicationPermissions.FutureAccessList.AddOrReplace()

					rval.AvialableShareItems.Add(sharedStorageItem);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetStorageItemsAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
			}
			//if (packageView.Contains(dataFormatName))
			//{
			//	try
			//	{
			//		this.sharedCustomData = await packageView.GetTextAsync(dataFormatName);
			//	}
			//	catch (Exception ex)
			//	{
			//		//NotifyUserBackgroundThread("Failed GetTextAsync(" + dataFormatName + ") - " + ex.Message, NotifyType.ErrorMessage);
			//	}
			//}
			if (packageView.Contains(StandardDataFormats.Html))
			{
				var sharedHtmlFormatItem = new HtmlShareItem();
				var sharedHtmlFormat = string.Empty;
				try
				{
					sharedHtmlFormat = await packageView.GetHtmlFormatAsync();
					//sharedHtmlFormatItem.HtmlFormat = sharedHtmlFormat;
					sharedHtmlFormatItem.HtmlFragment = HtmlFormatHelper.GetStaticFragment(sharedHtmlFormat);
				}
				catch (Exception ex)
				{
					//NotifyUserBackgroundThread("Failed GetHtmlFormatAsync - " + ex.Message, NotifyType.ErrorMessage);
				}
				//try
				//{
				//	var sharedResourceMap = await packageView.GetResourceMapAsync();
				//}
				//catch (Exception ex)
				//{
				//	//NotifyUserBackgroundThread("Failed GetResourceMapAsync - " + ex.Message, NotifyType.ErrorMessage);
				//}

				//if (packageView.Contains(StandardDataFormats.WebLink))
				//{
				//	try
				//	{
				//		sharedHtmlFormatItem.WebLink = await packageView.GetWebLinkAsync();
				//	}
				//	catch (Exception ex)
				//	{
				//		//NotifyUserBackgroundThread("Failed GetWebLinkAsync - " + ex.Message, NotifyType.ErrorMessage);
				//	}
				//}
				rval.AvialableShareItems.Add(sharedHtmlFormatItem);

			}
			if (packageView.Contains(StandardDataFormats.Bitmap))
			{
				try
				{
					var fi = await packageView.GetBitmapAsync();
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
			var fsitm = item.AvialableShareItems.OfType<FilesShareItem>().FirstOrDefault();
			var files = await GetStorageItemsAsync(fsitm);


			var dm = DataTransferManager.GetForCurrentView();

			TaskCompletionSource<object> dataTrasferedCompletion
				= new TaskCompletionSource<object>();


			using (Observable.FromEventPattern<TypedEventHandler<DataTransferManager, DataRequestedEventArgs>, DataRequestedEventArgs>
					(eh => dm.DataRequested += eh,
					eh => dm.DataRequested -= eh)
					//.ObserveOnDispatcher()
					.Subscribe(
						 e =>
							{

								var def = e.EventArgs.Request.GetDeferral();
								try
								{
									//dataTrasferedCompletion.TrySetResult(null);
									var r = e.EventArgs.Request;
									var package = r.Data;
									package.Properties.ContentSourceApplicationLink = item.ContentSourceApplicationLink;
									package.Properties.ContentSourceWebLink = item.ContentSourceWebLink;
									package.Properties.Description = item.Description;
									package.Properties.PackageFamilyName = item.PackageFamilyName;
									if (Square30x30Logo != null)
									{
										package.Properties.Square30x30Logo = RandomAccessStreamReference.CreateFromStream(Square30x30Logo);
										package.Properties.LogoBackgroundColor = item.LogoBackgroundColor;
									}
									if (Thumbnail != null)
									{
										package.Properties.Thumbnail = RandomAccessStreamReference.CreateFromStream(Thumbnail);
									}

									//package.SetText(item.Text?? "Reshared by " + Windows.ApplicationModel.Package.Current.DisplayName);

									package.Properties.Title = item.Title;
									var order = item.AvialableShareItems.OrderBy(x => x is TextSharedItem);
									 //make sure text share item execute last
									foreach (var subShareItem in order)
									{
										FillPackage(files, package, subShareItem);
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
				await Task.WhenAny(Task.Delay(500), dataTrasferedCompletion.Task);
			}
		}

		private static void FillPackage(StorageFile[] files, DataPackage package, object subShareItem)
		{
			if (subShareItem != null)
			{
				switch (subShareItem.GetType().Name)
				{
					case nameof(TextSharedItem):
						{
							package.SetText((subShareItem as TextSharedItem).Text);
						}
						break;
					case nameof(ApplicationLinkShareItem):
						{
							var sitm = subShareItem as ApplicationLinkShareItem;
							package.SetApplicationLink(sitm.ApplicationLink);
						}
						break;

					case nameof(DelayRenderedImageShareItem):
						{
							var sitm = subShareItem as DelayRenderedImageShareItem;
							package.SetBitmap(RandomAccessStreamReference.CreateFromStream(sitm.SelectedImage.GetRandowmAccessStream()));
						}
						break;

					case nameof(FilesShareItem):
						{
							StorageFile[] resultArray = files;
							package.SetStorageItems(resultArray);
						}
						break;
					case nameof(HtmlShareItem):
						{
							var sitm = subShareItem as HtmlShareItem;

							var fmt = HtmlFormatHelper.CreateHtmlFormat(sitm.HtmlFragment);
							package.SetHtmlFormat(fmt);
							package.SetText(sitm.HtmlFragment);

						}
						break;
					case nameof(WebLinkShareItem):
						{
							var sitm = subShareItem as WebLinkShareItem;
							package.SetWebLink(sitm.WebLink);
							package.SetText(sitm.WebLink?.ToString());

						}
						break;

					default:
						break;
				}

			}
		}

		private static async Task<StorageFile[]> GetStorageItemsAsync(object subShareItem)
		{
			var sitm = subShareItem as FilesShareItem;
			if (sitm == null)
			{
				return null;
			}
			var resultArray = await Task.WhenAll(
					sitm.StorageFiles
						.Select(flitem =>
						   StorageApplicationPermissions.FutureAccessList.GetFileAsync(flitem.AccessToken).AsTask())
						.ToArray());
			return resultArray;
		}

		public async Task<ReceivedShareItem> GetFromClipboardAsync()
		{
			var dataPackageView = Windows.ApplicationModel.DataTransfer.Clipboard.GetContent();

			var rval = await FetchDataFromPackageViewAsync(dataPackageView);
			//rval.AvialableShareItems.Add(new TextSharedItem { Text = rval.Text });
			return rval;

		}

		public async Task SetToClipboardAsync(object data)
		{
			var files = await GetStorageItemsAsync(data);
			var dataPackage = new DataPackage();
			FillPackage(files, dataPackage, data);
			Clipboard.SetContent(dataPackage);
		}
	}


}

