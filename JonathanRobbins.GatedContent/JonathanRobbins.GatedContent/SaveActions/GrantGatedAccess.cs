using System;
using System.Web;
using Sitecore.Data;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Actions.Base;

namespace JonathanRobbins.GatedContent.SaveActions
{
    public class GrantGatedAccess : WffmSaveAction
    {
        private Utility _utility;
        public Utility Utility
        {
            get
            {
                if (_utility == null)
                {
                    _utility = new Utility();
                }

                return _utility;
            }
            set { _utility = value; }
        }

        public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null,
            params object[] data)
        {
            HttpCookie cookie = new HttpCookie(Constants.GatedAccessCookeName)
            {
                Value = Constants.AccessGrantedCookieValue,
                Expires = DateTime.Now.AddDays(Utility.GetCookieLifeSpan())
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}