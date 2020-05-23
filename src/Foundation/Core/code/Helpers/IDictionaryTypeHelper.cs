using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DictionaryType.Foundation.Core.Helpers
{
    public interface IDictionaryTypeHelper
    {
        Dictionary<string, string> GetDictionaryList(string parentKey, string language = null);
    }
}