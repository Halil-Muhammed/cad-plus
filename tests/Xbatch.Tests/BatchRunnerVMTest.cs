using Moq;
using NUnit.Framework;
using System.Linq;
using Xarial.CadPlus.Common.Services;
using Xarial.CadPlus.XBatch.Base;
using Xarial.CadPlus.XBatch.Base.Core;
using Xarial.CadPlus.XBatch.Base.Models;
using Xarial.CadPlus.XBatch.Base.ViewModels;
using Xarial.CadPlus.XBatch.Sw;
using Xarial.XCad.SolidWorks.Enums;

namespace Xbatch.Tests
{
    public class BatchRunnerVMTest
    {
        [Test]
        public void BatchRunnerOptionsTest()
        {
            var mock = new Mock<IBatchRunnerModel>();
            BatchJob opts = null;
            mock.Setup(m => m.CreateExecutor(It.IsAny<BatchJob>())).Callback<BatchJob>(e => opts = e).Returns(new Mock<IBatchRunJobExecutor>().Object);
            mock.Setup(m => m.InstalledVersions).Returns(new AppVersionInfo[] { new SwAppVersionInfo(SwVersion_e.Sw2019), new SwAppVersionInfo(SwVersion_e.Sw2020) });

            var modelMock = mock.Object;
            var msgSvcMock = new Mock<IMessageService>().Object;
            var vm = new BatchManagerVM(modelMock, msgSvcMock);
            vm.Document = new BatchDocumentVM("", new BatchJob(), modelMock, msgSvcMock);

            vm.Document.Input.Add("D:\\folder1");
            vm.Document.Input.Add("D:\\folder2");
            vm.Document.Filter = "*.sld*";
            vm.Document.Macros.Add("C:\\macro1.swp");
            vm.Document.Macros.Add("C:\\macro2.swp");
            vm.Document.Settings.IsTimeoutEnabled = true;
            vm.Document.Settings.Timeout = 30;
            vm.Document.Settings.OpenFileOptionSilent = true;
            vm.Document.Settings.OpenFileOptionReadOnly = true;
            vm.Document.Settings.StartupOptionBackground = true;
            vm.Document.Settings.StartupOptionSilent = false;
            vm.Document.Settings.StartupOptionSafe = false;
            vm.Document.Settings.Version = new SwAppVersionInfo(SwVersion_e.Sw2020);

            vm.Document.RunJobCommand.Execute(null);

            Assert.AreEqual("*.sld*", opts.Filter);
            Assert.IsTrue(new string[] { "C:\\macro1.swp", "C:\\macro2.swp" }.SequenceEqual(opts.Macros));
            Assert.IsTrue(new string[] { "D:\\folder1", "D:\\folder2" }.SequenceEqual(opts.Input));
            Assert.AreEqual(30, opts.Timeout);
            Assert.AreEqual(OpenFileOptions_e.Silent | OpenFileOptions_e.ReadOnly, opts.OpenFileOptions);
            Assert.AreEqual(StartupOptions_e.Background, opts.StartupOptions);
            Assert.AreEqual(new SwAppVersionInfo(SwVersion_e.Sw2020), opts.Version);
        }

        [Test]
        public void BatchRunnerOptionsTimeoutTest()
        {
            var mock = new Mock<IBatchRunnerModel>();
            BatchJob opts = null;
            mock.Setup(m => m.CreateExecutor(It.IsAny<BatchJob>())).Callback<BatchJob>(e => opts = e).Returns(new Mock<IBatchRunJobExecutor>().Object);
            mock.Setup(m => m.InstalledVersions).Returns(new AppVersionInfo[] { new SwAppVersionInfo(SwVersion_e.Sw2019), new SwAppVersionInfo(SwVersion_e.Sw2020) });

            var modelMock = mock.Object;
            var msgSvcMock = new Mock<IMessageService>().Object;
            var vm = new BatchManagerVM(modelMock, msgSvcMock);
            vm.Document = new BatchDocumentVM("", new BatchJob(), modelMock, msgSvcMock);

            vm.Document.Settings.Timeout = 300;
            vm.Document.Settings.IsTimeoutEnabled = false;
            vm.Document.Settings.IsTimeoutEnabled = true;

            vm.Document.RunJobCommand.Execute(null);

            Assert.AreEqual(300, opts.Timeout);
        }

        [Test]
        public void BatchRunnerOptionsTimeoutDisableTest()
        {
            var mock = new Mock<IBatchRunnerModel>();
            BatchJob opts = null;
            mock.Setup(m => m.CreateExecutor(It.IsAny<BatchJob>())).Callback<BatchJob>(e => opts = e).Returns(new Mock<IBatchRunJobExecutor>().Object);
            mock.Setup(m => m.InstalledVersions).Returns(new AppVersionInfo[] { new SwAppVersionInfo(SwVersion_e.Sw2019), new SwAppVersionInfo(SwVersion_e.Sw2020) });

            var modelMock = mock.Object;
            var msgSvcMock = new Mock<IMessageService>().Object;
            var vm = new BatchManagerVM(modelMock, msgSvcMock);
            vm.Document = new BatchDocumentVM("", new BatchJob(), modelMock, msgSvcMock);

            vm.Document.Settings.IsTimeoutEnabled = false;

            vm.Document.RunJobCommand.Execute(null);

            Assert.AreEqual(-1, opts.Timeout);
        }
    }
}