using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlusBot.Modules
{
	public class ImageManipulation
	{
		public static async Task<MemoryStream> ProcessImageAsync(string name1, string name2)
		{
			string firstText = name1;
			string secondText = name2;

			PointF firstLocation = new PointF(100f, 90f);
			PointF secondLocation = new PointF(510f, 60f);

			string imageFilePath = @"images\kicking.bmp";
			Bitmap bitmap = (Bitmap)Image.FromFile(imageFilePath);//load the image file

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				using (Font arialFont = new Font("Arial", 10))
				{
					graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation);
					graphics.DrawString(secondText, arialFont, Brushes.Black, secondLocation);
				}
			}


			MemoryStream strm = new MemoryStream();
			bitmap.Save(strm, System.Drawing.Imaging.ImageFormat.Png);

			await Task.Yield();
			return strm;
		}
	}
}
