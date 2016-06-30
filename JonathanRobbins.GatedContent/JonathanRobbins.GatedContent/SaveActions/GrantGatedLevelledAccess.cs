using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Data;
using Sitecore.Publishing.Pipelines.PublishItem;
using Sitecore.WFFM.Abstractions.Actions;
using Sitecore.WFFM.Actions.Base;

namespace JonathanRobbins.GatedContent.SaveActions
{
    public class GrantGatedLevelledAccess : WffmSaveAction
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

        public override void Execute(ID formId, AdaptedResultList adaptedFields, ActionCallContext actionCallContext = null, params object[] data)
        {
            string level = DetermineLevel(actionCallContext);

            HttpCookie cookie = new HttpCookie(Utility.DefineLevelledCookieName())
            {
                Value = level,
                Expires = DateTime.Now.AddDays(Utility.GetCookieLifeSpan())
            };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        private string DetermineLevel(ActionCallContext actionCallContext)
        {
            string level = string.Empty;

            if (actionCallContext != null && actionCallContext.Parameters.Any())
            {
                var levelParam = actionCallContext.Parameters.FirstOrDefault(
                        p => p.Key.Equals("level", StringComparison.InvariantCultureIgnoreCase));

                if (!string.IsNullOrEmpty(levelParam.Value.ToString()))
                {
                    level = levelParam.Value.ToString();
                }
            }

            return level;
        }
    }
}
