using Microsoft.Reactive.Testing;
using NUnit.Framework;
using System;
using TurboTrainer.Core;
using System.Reactive.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TurboTrainer.Tests
{
    [TestFixture]
    public class MainViewModelTests
    {
        private TestScheduler scheduler;

        [Test]
        public void CurrentPoint_AfterConstruction_IsNull()
        {
            var vm = InitialiseViewModel();
            Assert.That(vm.CurrentPoint, Is.Null);
        }

        [Test, Timeout(500)]
        public void CurrentPoint_AfterLoadingGpx_EqualsFirstPointInGpx()
        {
            var vm = InitialiseViewModel();

	        Assert.That(ExecuteLoadGpxDataCommandAsync(vm).Result, Is.True);

            vm.CurrentPoint.AssertPoint(47.644548, -122.326897, 4.46, "2009-10-17T18:37:26Z");
        }

        [Test, Timeout(500)]
        public void CurrentPoint_AfterLoadingGpxAndWaiting_IsUpdatedAtTheCorrectTimes()
        {
            var vm = InitialiseViewModel();

            // Log property updates
            var propertyUpdates = new List<string>();
            vm.Changed.Subscribe(x => propertyUpdates.Add(x.PropertyName));

	        Assert.That(ExecuteLoadGpxDataCommandAsync(vm).Result, Is.True);

            scheduler.AdvanceBy(1);
            Assert.That(propertyUpdates.Count, Is.EqualTo(1));
            Assert.That(propertyUpdates[0], Is.EqualTo("CurrentPoint"));
            vm.CurrentPoint.AssertPoint(47.644548, -122.326897, 4.46, "2009-10-17T18:37:26Z");

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 5);
            Assert.That(propertyUpdates.Count, Is.EqualTo(2));
            Assert.That(propertyUpdates[1], Is.EqualTo("CurrentPoint"));
            vm.CurrentPoint.AssertPoint(47.644548, -122.326897, 4.94, "2009-10-17T18:37:31Z");

            scheduler.AdvanceBy(TimeSpan.TicksPerSecond * 3);
            Assert.That(propertyUpdates.Count, Is.EqualTo(3));
            Assert.That(propertyUpdates[0], Is.EqualTo("CurrentPoint"));
            vm.CurrentPoint.AssertPoint(47.644548, -122.326897, 6.87, "2009-10-17T18:37:34Z");
        }

		[Test, Timeout(500)]
		public void LoadGpxCommandExecute_WhenStreamIsNull_DoesntCrash() // This can happen if the file chooser is cancelled
        {
            var vm = InitialiseViewModel(Stream.Null);

            Assert.That(ExecuteLoadGpxDataCommandAsync(vm).Result, Is.True);
        }

        private MainViewModel InitialiseViewModel()
        {
            return InitialiseViewModel(new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.SampleGpxDocument)));
        }

        private MainViewModel InitialiseViewModel(Stream stream)
        {
            scheduler = new TestScheduler();
            return new MainViewModel(scheduler, new TestFileChooser(stream));
        }

        private static Task<bool> ExecuteLoadGpxDataCommandAsync(MainViewModel vm)
        {
            var completionSource = new TaskCompletionSource<bool>();

            vm.LoadGpxDataCommand.ThrownExceptions.Subscribe(_ => completionSource.SetResult(false));
            vm.LoadGpxDataCommand.Execute(null);
            vm.LoadGpxDataCommand.IsExecuting.FirstAsync(x => x == false).Subscribe(_ => { completionSource.SetResult(true); });

            return completionSource.Task;
        }
    }
}
