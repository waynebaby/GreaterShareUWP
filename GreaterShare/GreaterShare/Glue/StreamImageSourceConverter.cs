using GreaterShare.Models;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reactive.Subjects;

namespace GreaterShare.Glue
{
	public class StreamImageSourceConverter : IValueConverter
	{
		public StreamImageSourceConverter()
		{
			subject.Where(x => x.Item1 != null && x.Item2 != null)
				.SelectMany(x =>
					  x.Item2
					  .GetRandowmAccessStreamAsync()
					  .ToObservable()
					  .Select(i => new { bmp = x.Item1, stm = i }))
				.ObserveOnDispatcher()
				.Subscribe(e =>
				{
					e.bmp.SetSource(e.stm);
				});

		}

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			var item = value as MemoryStreamBase64Item;
			if (item != null)
			{
				var bs = new BitmapImage();
				subject.OnNext(new Tuple<BitmapImage, MemoryStreamBase64Item>(bs, item));
				return bs;
			}
			return null;
		}

		Subject<Tuple<BitmapImage, MemoryStreamBase64Item>> subject = new Subject<Tuple<BitmapImage, MemoryStreamBase64Item>>();



		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}



}
