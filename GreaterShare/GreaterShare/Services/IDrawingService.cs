using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input.Inking;

namespace GreaterShare.Services
{
	public interface IDrawingService
	{
		byte[] DrawStrokeOnBackground(IReadOnlyList<InkStroke> strokes, int width, int height, byte[] backgroundImageBuffer);

	}
}
