using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace GreaterShare.Services
{
	public interface IImageConvertService
	{
		Task ConverterBitmapToTargetStreamAsync(IRandomAccessStream bitmapSourceStream, IRandomAccessStream saveTargetStream);
	}
}