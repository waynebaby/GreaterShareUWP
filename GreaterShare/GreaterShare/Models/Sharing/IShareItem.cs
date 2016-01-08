using MVVMSidekick.ViewModels;

namespace GreaterShare.Models.Sharing
{
	public interface IShareItem : IBindable
	{
		string ContentSourceApplicationLink { get; set; }
		string DefaultFailedDisplayText { get; set; }
		string Description { get; set; }
		string Title { get; set; }
	}
}