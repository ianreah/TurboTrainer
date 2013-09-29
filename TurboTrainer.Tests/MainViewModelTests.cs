using Microsoft.Reactive.Testing;
using NUnit.Framework;
using System;
using TurboTrainer.Core;
using System.Reactive.Linq;
using System.Collections.Generic;

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

            ExecuteAndWaitForLoadGpxDataCommand(vm);

            vm.CurrentPoint.AssertPoint(47.644548, -122.326897, 4.46, "2009-10-17T18:37:26Z");
        }

        [Test, Timeout(500)]
        public void CurrentPoint_AfterLoadingGpxAndWaiting_IsUpdatedAtTheCorrectTimes()
        {
            var vm = InitialiseViewModel();

            // Log property updates
            var propertyUpdates = new List<string>();
            vm.Changed.Subscribe(x => propertyUpdates.Add(x.PropertyName));

            ExecuteAndWaitForLoadGpxDataCommand(vm);

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

        private MainViewModel InitialiseViewModel()
        {
            scheduler = new TestScheduler();
            return new MainViewModel(scheduler, new TestFileChooser());
        }

        private static void ExecuteAndWaitForLoadGpxDataCommand(MainViewModel vm)
        {
            vm.LoadGpxDataCommand.Execute(null);
            vm.LoadGpxDataCommand.IsExecuting.FirstAsync(x => x == false).Wait();
        }
    }
}
