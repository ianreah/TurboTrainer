using TurboTrainer.Core;
using Android.Content.Res;
using System.IO;

namespace TurboTrainer
{
	public class FileChooser : IFileChooserUi
	{
		private readonly AssetManager assets;

		public FileChooser (AssetManager assets)
		{
			this.assets = assets;
		}

		public Stream ChooseFile ()
		{
			return assets.Open("SampleGpxDocument.txt");
		}
	}
}