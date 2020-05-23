using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Services;
using Umbraco.Web.Editors;
using Umbraco.Core.Logging;

namespace UmbracoDictionaryPropertyEditor.Controllers
{
    public class DictionaryTypeController : UmbracoAuthorizedJsonController
    {
        private readonly ILocalizationService _localizationService;
        private readonly ILogger _logger;

        public DictionaryTypeController(ILocalizationService localizationService,
            ILogger logger)
        {
            _localizationService = localizationService;
            _logger = logger;
        }

        /// <summary>
        /// Get a list of SelectListItem from the children of a dictionary key
        /// </summary>
        /// <param name="parentKey">Parent Dictionary Key</param>
        /// <param name="language">Two Letter ISO Language Name (e.g. en, ar, fr)</param>
        /// <returns></returns>
        public List<SelectListItem> GetDictionaryList(string parentKey, string language = null)
        {
            var result = new List<SelectListItem>();

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
                        //Get the current language
                        var currentLanguage = language ?? CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

                        var languages = _localizationService.GetAllLanguages();

                        var languageCode = languages.FirstOrDefault(x => x.CultureInfo.TwoLetterISOLanguageName.ToLower() == currentLanguage.Replace(" " , string.Empty).ToLower());

                        foreach (var item in childItems.OrderBy(x => x.ItemKey))
                        {
                            var listItem = new SelectListItem();

                            listItem.Value = item.ItemKey;

                            listItem.Text = item.Translations.FirstOrDefault(x => x.Language.IsoCode == languageCode.CultureInfo.Name).Value;

                            result.Add(listItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error<List<SelectListItem>>("GetDictionaryChildren Error: ", ex);
            }

            return result;

        }
    }
}