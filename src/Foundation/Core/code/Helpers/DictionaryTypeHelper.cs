using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Services;
using Umbraco.Core.Logging;
using System.Globalization;

namespace DictionaryType.Foundation.Core.Helpers
{
    public class DictionaryTypeHelper : IDictionaryTypeHelper
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        public DictionaryTypeHelper(ILocalizationService localizationService,
            ILogger logger)
        {
            _localizationService = localizationService;
            _logger = logger;
        }

        /// <summary>
        /// Get Dictionary<string, string> from the children of a dictionary key
        /// </summary>
        /// <param name="parentKey">Parent Dictionary Key</param>
        /// <param name="language">Two Letter ISO Language Name (e.g. en, ar, fr)</param>
        /// <returns></returns>
        public Dictionary<string, string> GetDictionaryList(string parentKey, string language = null)
        {
            var result = new Dictionary<string, string>();

            try
            {
                //Get the dictionary item
                var dictionaryParent = _localizationService.GetDictionaryItemByKey(parentKey);

                //Check if dictionary exist
                if (dictionaryParent != null)
                {
                    //Get the children of the dictionary item
                    var childItems = _localizationService.GetDictionaryItemChildren(dictionaryParent.Key);

                    if (childItems != null && childItems.Any())
                    {

                        //Get all the languages available
                        var languages = _localizationService.GetAllLanguages();

                        //Get the current language
                        var currentLanguage = language ?? CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                        //Get the current language code
                        var languageCode = languages.FirstOrDefault(x => x.CultureInfo.TwoLetterISOLanguageName.ToLower() == currentLanguage.Replace(" ", string.Empty).ToLower());

                        //Add the items into a dictionary
                        foreach (var item in childItems.OrderBy(x => x.ItemKey))
                        {
                            result.Add(item.ItemKey,
                                        item.Translations.FirstOrDefault(x => x.Language.IsoCode == languageCode.CultureInfo.Name).Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error<Dictionary<string, string>> ("GetDictionaryList Error: ", ex);
            }

            return result;

        }
    }
}