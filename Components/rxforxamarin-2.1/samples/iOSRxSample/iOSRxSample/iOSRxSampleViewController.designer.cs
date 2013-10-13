// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace iOSRxSample
{
	[Register ("iOSRxSampleViewController")]
	partial class iOSRxSampleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblStatus { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIScrollView scrollPad { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblStatus != null) {
				lblStatus.Dispose ();
				lblStatus = null;
			}

			if (scrollPad != null) {
				scrollPad.Dispose ();
				scrollPad = null;
			}
		}
	}
}
