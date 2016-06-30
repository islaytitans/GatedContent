using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Actions.Base;

namespace JonathanRobbins.GatedContent.SaveActions
{
    public class GrantGatedAccess : WffmSaveAction
    {
        public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null,
            params object[] data)
        {
            HttpCookie cookie = new HttpCookie(Constants.GatedAccessCookeName)
            {
                Value = Constants.AccessGrantedCookieValue,
                Expires = DateTime.Today.AddYears(10),
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}