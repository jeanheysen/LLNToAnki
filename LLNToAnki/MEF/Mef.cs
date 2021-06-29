using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Text;

namespace LLNToAnki.Business
{
    public static class Mef
    {
        private static CompositionContainer container;

        public static CompositionContainer Container
        {
            get
            {
                if (container == null)
                {
                    var catalog = new DirectoryCatalog(".", "LLNToAnki.*");

                    container = new CompositionContainer(catalog);
                }

                return container;
            }
        }
    }
}
