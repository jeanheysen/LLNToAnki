using LLNToAnki.Business.Ports;

namespace LLNToAnki.Infrastructure.DataStoring
{
    [System.ComponentModel.Composition.Export(typeof(IContextProvider)), System.Composition.Shared]
    public class ContextProvider : IContextProvider
    {
        private Context context;

        public IContext Context
        {
            get
            {
                if (context == null) context = new Context();
                return context;
            }
        }
    }
}
