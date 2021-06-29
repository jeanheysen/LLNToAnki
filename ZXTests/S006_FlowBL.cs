using LLNToAnki.Business;
using LLNToAnki.Business.Logic;
using NUnit.Framework;

namespace ZXTests
{
    public class S006_FlowBL
    {
        [Test]
        public void T001_flowBLSolvedWithDependencyInjectionIsNotNull()
        {
            var flowBL = Mef.Container.GetExportedValue<IFlowBL>();

            Assert.IsNotNull(flowBL);
        }
    }
}
