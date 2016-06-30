using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;
using Sitecore.Diagnostics;
using Sitecore.Publishing.Pipelines.PublishItem;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Actions.Base;

namespace JonathanRobbins.GatedContent.SaveActions
{
    public class GrantGatedLevelledAccess : WffmSaveAction
    {
        public string Level { get; set; }

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

        public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null, params object[] data)
        {
            if (string.IsNullOrEmpty(Level))
            {
                Log.Error("The level of Gated Access has not been set on the Save Action - " 
                    + ActionID.ToString() + ". Please ensure it is set correct i.e. <Level>1</Level> in the parameters field", this);
                Level = string.Empty;
            }

            HttpCookie cookie = new HttpCookie(Utility.DefineCookieName())
            {
                Value = Level,
                Expires = DateTime.Now.AddDays(Utility.GetCookieLifeSpan())
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
