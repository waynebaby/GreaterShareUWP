using GreaterShare.BackgroundServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using System.IO;
using Windows.Storage.FileProperties;
using System.Collections;

namespace GreaterShare.BackgroundServices.Service
{
    public sealed class PicLibFolderScanService
    {
        private IStorageFolder workingFolder = ApplicationData.Current.LocalCacheFolder;
        private IAsyncOperation<StorageFile> workingFile;



        public PicLibFolderScanService()
        {
            var option = CreationCollisionOption.OpenIfExists;
            GetCacheFile(option);
        }

        private void GetCacheFile(CreationCollisionOption option)
        {
            workingFile = workingFolder.CreateFileAsync(nameof(PicLibFolderScanService) + ".cache", option);
        }

        public IAsyncOperation<IEnumerable> GetSnapshotAsync()
        {
            return InternalGetSnapshotAsync().AsAsyncOperation();
        }

        public IAsyncOperation<IEnumerable> LoadSnapshotAsync()
        {
            return InternalLoadSnapshotAsync().AsAsyncOperation();
        }

        public IAsyncAction SaveSnapshotAsync(IEnumerable folder)
        {
            return InternalSaveSnapshotAsync(folder.OfType<IPicLibFolder>().ToArray()).AsAsyncAction();
        }

        internal async Task<IEnumerable> InternalGetSnapshotAsync()
        {
            var root = await StorageLibrary.GetLibraryAsync(KnownLibraryId.Pictures);
            var snapshot = await Task.WhenAll(root.Folders.ToArray().Select(async x => await InternalGetSnapshotAsync(x)));
            return snapshot;
        }
        internal async Task<IPicLibFolder> InternalGetSnapshotAsync(IStorageFolder current)
        {
            var fs = await current.GetFilesAsync();
            var folders = await Task.WhenAll((await current.GetFoldersAsync()).Select(async x => await InternalGetSnapshotAsync(x)));

            var filesLast = (await Task.WhenAll(fs.Select(async x => await x.GetBasicPropertiesAsync())))
                          .Select(x => x.DateModified)
                          .Aggregate(DateTimeOffset.MinValue, (x, y) => x > y ? x : y);

            var foldersLast = folders.Select(x => DateTimeOffset.Parse(x.LastFileEditTime))
                   .Aggregate(DateTimeOffset.MinValue, (x, y) => x > y ? x : y);
            ;

            var picf = new PicLibFolder()
            {
                FileCount = fs.Count,
                LastFileEditTime = ((filesLast > foldersLast) ? filesLast : foldersLast).ToString(),
                UriString = current.Path,
                Folders = folders
            };

            return picf;
        }

        internal async Task<IEnumerable> InternalLoadSnapshotAsync()
        {
            var f = await workingFile;
            var sl = new System.Runtime.Serialization.DataContractSerializer(typeof(PicLibFolder[]));
            var ms = new MemoryStream();
            using (var inps = (await f.OpenReadAsync()).GetInputStreamAt(0).AsStreamForRead())
            {
                await inps.CopyToAsync(ms);
                ms.Position = 0;
            }
            var r = sl.ReadObject(ms) as PicLibFolder[];
            return r;
        }

        internal async Task InternalSaveSnapshotAsync(IPicLibFolder[] folders)
        {
            var sl = new System.Runtime.Serialization.DataContractSerializer(typeof(PicLibFolder[]));

            var ms = new MemoryStream();
            sl.WriteObject(ms, folders.OfType<PicLibFolder>().ToArray());
            ms.Position = 0;
            GetCacheFile(CreationCollisionOption.ReplaceExisting);
            var f = await workingFile;
            using (var ws = await f.OpenStreamForWriteAsync())
            {
                await ms.CopyToAsync(ws);
            }


        }



        public IEnumerable CompareSnapshot(IEnumerable oldOnes, IEnumerable newOnes)
        {
            return InternalCompareSnapshot(oldOnes.OfType<IPicLibFolder>().ToArray(), newOnes.OfType<IPicLibFolder>().ToArray()).ToArray();
        }




        Comparer<IPicLibFolder> compUrlEqual = Comparer<IPicLibFolder>.Create((x, y) =>
        x.UriString.CompareTo(y.UriString)

);
        internal IEnumerable<IPicLibFolder> InternalCompareSnapshot(IEnumerable<IPicLibFolder> oldOnes, IEnumerable<IPicLibFolder> newOnes)
        {

            if (oldOnes == null)
            {
                foreach (var item in newOnes)
                {
                    var deeper = InternalCompareSnapshot(null, item.Folders);
                    foreach (var itemdeep in deeper)
                    {
                        yield return itemdeep;
                    }
                }
            }


            var setDiff = newOnes.ToDictionary(x => x.UriString, x => x);

            foreach (var item in oldOnes)
            {
                IPicLibFolder target = null;
                if (setDiff.TryGetValue(item.UriString,out target))
                {
                    if (target.FileCount == item.FileCount&& 
                        target.Folders.Count ==item.Folders.Count &&
                        target.LastFileEditTime==item.LastFileEditTime                       
                        )
                    {
                        setDiff.Remove(target.UriString);
                    }
                }

            }
            //var setNameCommon = new SortedSet<IPicLibFolder>(newOnes, compUrlEqual);
            //setNameCommon.IntersectWith(oldOnes);

            var dictOldOnes = oldOnes.ToDictionary(x => x.UriString, x => x);
            foreach (var item in setDiff)
            {
                yield return item.Value;

                IPicLibFolder oldOne = null;
                dictOldOnes.TryGetValue(item.Value.UriString, out oldOne);

                var deeperOldOnes = oldOne?.Folders;
                var depperNewOnes = item.Value.Folders;


                var deeper = InternalCompareSnapshot(deeperOldOnes, depperNewOnes);
                foreach (var itemdeep in deeper)
                {
                    yield return itemdeep;
                }

            }

        }
    }
}
