using LLNToAnki.Business.Ports;
using LLNToAnki.Domain;

namespace LLNToAnki.Infrastructure.URLBuilding
{
    public interface IUrlLAbstractFactory
    {
        IURLBuilder CreateUrlBuilder(Language l);
    }

    public class UrlAbstractFactory : IUrlLAbstractFactory
    {
        public IURLBuilder CreateUrlBuilder(Language l)
        {
            switch (l)
            {
                case Language.English: return new WordReferenceURLBuilder();
                case Language.Dutch: return new MijnWordenboekURLBuilder();
                default: throw new System.Exception($"{l} does not have a specified dictionnaire.");
            }
        }
    }
}
