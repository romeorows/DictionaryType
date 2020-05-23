using Umbraco.Core;
using Umbraco.Core.Composing;
using DictionaryType.Foundation.Core.Helpers;

namespace DictionaryType.Foundation.Core.Infrastructure
{
    public class Composer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<IDictionaryTypeHelper, DictionaryTypeHelper>();
        }
    }
}