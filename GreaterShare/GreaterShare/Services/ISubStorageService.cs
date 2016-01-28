using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace GreaterShare.Services
{
	public interface ISubStorageService
	{
		string GetNewFileName();
		Task<StorageFile> SaveToFileAsync<TItem>(string fileName, TItem item);
		Task SaveToFileAsync<TItem>(IStorageFile file, TItem item);
		Task<TItem> LoadFromFileAsync<TItem>(string fileName);
		Task<TItem> LoadFromFileAsync<TItem>(IStorageFile file);
		Task<string[]> ListFiles();
		Task<ISubStorageService[]> ListFolderServices();
		string SubPath { get; }			 
	}
}
