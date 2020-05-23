using DictionaryType.Foundation.Core.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Web.Editors;

namespace DictionaryType.Feature.BackOffice.Controllers
{
    public class DictionaryTypeController : UmbracoAuthorizedJsonController
    {
        private readonly IDictionaryTypeHelper _dictionaryHelper;

        public DictionaryTypeController(IDictionaryTypeHelper dictionaryHelper)
        {
            _dictionaryHelper = dictionaryHelper;
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

            //Get the dictionary list
            var dictionaryList = _dictionaryHelper.GetDictionaryList(parentKey, language);

            if (dictionaryList != null && dictionaryList.Any())
            {
                result.AddRange(dictionaryList.Select(x => new SelectListItem() { Value = x.Key, Text = x.Value }));
               
            }
           
            return result;

        }
    }
}