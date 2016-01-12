using System.Reactive;
using System.Reactive.Linq;
using MVVMSidekick.ViewModels;
using MVVMSidekick.Views;
using MVVMSidekick.Reactive;
using MVVMSidekick.Services;
using MVVMSidekick.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GreaterShare.ViewModels
{

    [DataContract]
    public class MainPage_Model : ViewModelBase<MainPage_Model>
    {  
        public MainPage_Model()
        {
          
        
        }



		protected override Task OnBindedViewLoad(IView view)
		{
			return base.OnBindedViewLoad(view);
		}



	}

}

