using GreaterShare.Models.Sharing.ShareItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer.ShareTarget;

namespace GreaterShare.Services
{
	public interface IShareService
	{
		Task<ReceivedShareItem> GetReceivedSharedItemAsync(ShareOperation sourceOperation);

		//Task FillShareOperationAsync(ShareOperation sourceOperation, ReceivedShareItem);

	}
}
