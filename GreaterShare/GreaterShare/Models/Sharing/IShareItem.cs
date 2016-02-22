//using System;

//namespace GreaterShare.Models.Sharing
//{
public interface IShareItem
{
	bool IsSelected { get; set; }

	void WireEvent();

	bool IsEventWired { get; set; }
}
//}