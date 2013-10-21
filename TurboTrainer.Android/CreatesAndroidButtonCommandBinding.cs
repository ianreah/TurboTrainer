using ReactiveUI;
using System;
using System.Windows.Input;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Android.Widget;

namespace TurboTrainer
{
	/// <summary>
	/// A very basic command binding creator specifically for Android.Widget.Button
	/// 
	/// I'm assuming it'll be implemented as part of ReactiveUI for the Android platform
	/// eventually...and potentially in a more generic and robust way (assuming Mr Betts
	/// finds a good way to deal with the inconsistent Android event naming...
	/// ...https://twitter.com/paulcbetts/status/391241925132427264)
	/// </summary>
	public class CreatesAndroidButtonCommandBinding : ICreatesCommandBinding
	{
		public int GetAffinityForObject(Type type, bool hasEventTarget)
		{
			return typeof(Button).IsAssignableFrom(type) ? 2 : 0;
		}

		public IDisposable BindCommandToObject(ICommand command, object target, IObservable<object> commandParameter)
		{
			return BindCommandToObject<EventArgs>(command, target, commandParameter, "Click");
		}

		public IDisposable BindCommandToObject<TEventArgs>(ICommand command, object target, IObservable<object> commandParameter, string eventName)
		{
			var button = (Button)target;

			var disposables = new CompositeDisposable();

			disposables.Add(Observable.FromEventPattern(button, eventName).Subscribe(_ => command.Execute(null)));
			disposables.Add(Observable.FromEventPattern(command, "CanExecuteChanged").Subscribe(x => button.Enabled = command.CanExecute(null)));

			return disposables;
		}
	}
}


