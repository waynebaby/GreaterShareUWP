using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Input.Inking;

namespace GreaterShare.Services
{
	public interface IDrawingService
	{
		byte[] DrawStrokeOnSolidColorBackground(IReadOnlyList<InkStroke> strokes, int width, int height, Color color );
		byte[] DrawStrokeOnImageBackground(IReadOnlyList<InkStroke> strokes, byte[] backgroundImageBuffer);
	}
}
