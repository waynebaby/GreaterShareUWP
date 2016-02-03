using GreaterShare.Glue;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Reflection;
using GreaterShare.Models.Sharing.ShareItems;

namespace GreaterShare.Services
{
	public class DefaultSubStorageService : ISubStorageService
	{


		DataContractJsonSerializer GetCoreSerializer<TItem>() => new DataContractJsonSerializer(typeof(TItem));

		public string SubPath { get; private set; } = "SavedShares";


		public string FileNameMask { get; set; } = "Share{0:yyMMddHHmmss}{1}.gshare";
		public List<Func<object>> FileNameMaskItemFactories { get; set; }
					   = new List<Func<object>> {
									()=>DateTime.Now,
									()=>new Random().NextDouble().ToString()
					   };
		public Task<StorageFolder> ParentFolder { get; private set; }
				//=Task.FromResult(ApplicationData.Current.LocalFolder);
				= Task.FromResult(ApplicationData.Current.TemporaryFolder);


		public string GetNewFileName()
		{
			var path =
				string.Format(
					FileNameMask,
					FileNameMaskItemFactories.Select(x => x()).ToArray()
				);
			return path;
		}

		StorageFolder currentFolder;
		async Task<StorageFolder> GetStorageCurrentFolderAsync()
		{
			if (currentFolder != null)
			{
				return currentFolder;
			}
			var item = await (await ParentFolder).TryGetItemAsync(SubPath);
			if (item != null)
			{
				if (item.IsOfType(StorageItemTypes.Folder))
				{
					return await (await ParentFolder).GetFolderAsync(SubPath);
				}
				else
				{
					await item.RenameAsync(item.Name + new Random().NextDouble().ToString() + ".bak");
				}
			}
			return currentFolder = await (await ParentFolder).CreateFolderAsync(SubPath);


		}

		public async Task<StorageFile> SaveToFileAsync<TItem>(string fileName, TItem item)
		{
			var folder = await GetStorageCurrentFolderAsync();

			var file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
			await SaveToFileAsync(file, item);

			return file;
		}

		public async Task SaveToFileAsync<TItem>(IStorageFile file, TItem item)
		{
			var stm = new MemoryStream();
			GetCoreSerializer<TItem>().WriteObject(stm, item);
			stm.Position = 0;

			using (var tstm = await file.OpenStreamForWriteAsync())
			{
				await stm.CopyToAsync(tstm);
			}
		}

		public async Task<TItem> LoadFromFileAsync<TItem>(string fileName)
		{

			var folder = await GetStorageCurrentFolderAsync();
			var file = await folder.GetFileAsync(fileName);
			return await LoadFromFileAsync<TItem>(file);

		}

		public async Task<TItem> LoadFromFileAsync<TItem>(IStorageFile file)
		{
			var stm = new MemoryStream();
			using (var tstm = await file.OpenStreamForWriteAsync())
			{
				await tstm.CopyToAsync(stm);
			}

			stm.Position = 0;
			return (TItem)GetCoreSerializer<TItem>().ReadObject(stm);
		}

		public async Task<string[]> ListFiles()
		{
			var folder = await GetStorageCurrentFolderAsync();
			return (await folder.GetFilesAsync()).Select(x => x.Name).ToArray();

		}

		public async Task<ISubStorageService[]> ListFolderServices()
		{
			var folder = await GetStorageCurrentFolderAsync();
			return (await folder.GetFoldersAsync())
				.Select(x => new DefaultSubStorageService
				{
					SubPath = x.Name,
					currentFolder = x,
					ParentFolder = ParentFolder,
				})
				.ToArray();
		}


	}
}
