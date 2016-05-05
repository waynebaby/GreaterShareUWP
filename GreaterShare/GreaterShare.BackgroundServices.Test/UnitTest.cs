using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using System.Linq;
using Windows.Storage;

namespace GreaterShare.BackgroundServices.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task SimpleGetSnapshotAsync()
        {
            var s = new GreaterShare.BackgroundServices.Service.PicLibFolderScanService();

            var sn = await s.GetSnapshotAsync();

            Assert.IsNotNull(sn);
        }


        [TestMethod]
        public async Task SimpleCompareSnapshotAsync()
        {
            var s = new GreaterShare.BackgroundServices.Service.PicLibFolderScanService();

            var sn = await s.GetSnapshotAsync();

            var lib = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);

            var folder = lib.Folders[0];

            using (var file = await (await (await folder.GetFoldersAsync()).OfType<IStorageFolder>().Last().CreateFileAsync("ok.png", CreationCollisionOption.GenerateUniqueName)).OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
            {
              await  file.FlushAsync();
            }
            var sn2= await s.GetSnapshotAsync();

            var cr= s.CompareSnapshot(sn, sn2).OfType<Models.IPicLibFolder>();
            Assert.AreEqual ( cr.Count () ,2);
        }

        [TestMethod]
        public async Task SaveLoadCompareSnapshotAsync()
        {
            var s = new GreaterShare.BackgroundServices.Service.PicLibFolderScanService();

            var sn = await s.GetSnapshotAsync();

            await s.SaveSnapshotAsync(sn);

            sn = await s.LoadSnapshotAsync();

            var lib = await Windows.Storage.StorageLibrary.GetLibraryAsync(Windows.Storage.KnownLibraryId.Pictures);

            var folder = lib.Folders[0];

            using (var file = await (await (await folder.GetFoldersAsync()).OfType<IStorageFolder>().Last().CreateFileAsync("ok.png", CreationCollisionOption.GenerateUniqueName)).OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
            {
                await file.FlushAsync();
            }
            var sn2 = await s.GetSnapshotAsync();

            var cr = s.CompareSnapshot(sn, sn2).OfType<Models.IPicLibFolder>();
            Assert.AreEqual(cr.Count(), 2);
        }



    }
}
