using LLNToAnki.Domain;

namespace LLNToAnki.Business.Ports
{
    public interface IDictionaryAbstractFactory
    {
        ITranslationDetailer Provide(Language language);
    }
}
