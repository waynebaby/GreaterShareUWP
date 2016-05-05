using MVVMSidekick.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GreaterShare.BackgroundServices.Models
{
    [DataContract(IsReference = true)] //if you want
    internal class PicLibFolder : BindableBase<PicLibFolder>, IPicLibFolder,IEquatable<IPicLibFolder>
    {
        public PicLibFolder()
        {
        }

        [DataMember]
        public String UriString
        {
            get { return _UriStringLocator(this).Value; }
            set { _UriStringLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String UriString Setup        
        protected Property<String> _UriString = new Property<String> { LocatorFunc = _UriStringLocator };
        static Func<BindableBase, ValueContainer<String>> _UriStringLocator = RegisterContainerLocator<String>(nameof(UriString), model => model.Initialize(nameof(UriString), ref model._UriString, ref _UriStringLocator, _UriStringDefaultValueFactory));
        static Func<String> _UriStringDefaultValueFactory = () => default(String);
        #endregion


        [DataMember]
        public int FileCount
        {
            get { return _FileCountLocator(this).Value; }
            set { _FileCountLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property int FileCount Setup        
        protected Property<int> _FileCount = new Property<int> { LocatorFunc = _FileCountLocator };
        static Func<BindableBase, ValueContainer<int>> _FileCountLocator = RegisterContainerLocator<int>(nameof(FileCount), model => model.Initialize(nameof(FileCount), ref model._FileCount, ref _FileCountLocator, _FileCountDefaultValueFactory));
        static Func<int> _FileCountDefaultValueFactory = () => default(int);
        #endregion



        [DataMember]
        public String LastFileEditTime
        {
            get { return _LastFileEditTimeLocator(this).Value; }
            set { _LastFileEditTimeLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property String LastFileEditTime Setup        
        protected Property<String> _LastFileEditTime = new Property<String> { LocatorFunc = _LastFileEditTimeLocator };
        static Func<BindableBase, ValueContainer<String>> _LastFileEditTimeLocator = RegisterContainerLocator<String>(nameof(LastFileEditTime), model => model.Initialize(nameof(LastFileEditTime), ref model._LastFileEditTime, ref _LastFileEditTimeLocator, _LastFileEditTimeDefaultValueFactory));
        static Func<String> _LastFileEditTimeDefaultValueFactory = () => default(String);
        #endregion



        [DataMember]
        public IList<IPicLibFolder> Folders
        {
            get { return _FoldersLocator(this).Value; }
            set { _FoldersLocator(this).SetValueAndTryNotify(value); }
        }
        #region Property IList<IPicLibFolder> Folders Setup        
        protected Property<IList<IPicLibFolder>> _Folders = new Property<IList<IPicLibFolder>> { LocatorFunc = _FoldersLocator };
        static Func<BindableBase, ValueContainer<IList<IPicLibFolder>>> _FoldersLocator = RegisterContainerLocator<IList<IPicLibFolder>>("Folders", model => model.Initialize("Folders", ref model._Folders, ref _FoldersLocator, _FoldersDefaultValueFactory));
        static Func<IList<IPicLibFolder>> _FoldersDefaultValueFactory = () => new List<IPicLibFolder>();

    
        public bool Equals(IPicLibFolder other)
        {
            return UriString == other.UriString && LastFileEditTime == other.LastFileEditTime && this.FileCount == other.FileCount;

        }

        #endregion


    }
}
