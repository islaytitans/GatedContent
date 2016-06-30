using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

namespace JonathanRobbins.GatedContent.Rules
{
    public class GrantGatedAccessLevelledCondition<T> : IntegerComparisonCondition<T> where T : RuleContext
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

        protected override bool Execute(T ruleContext)
        {
            string cookieName = Utility.DefineCookieName();

            if (HttpContext.Current.Request.Cookies[cookieName] == null)
                return false;

            var actualLevel = HttpContext.Current.Request.Cookies[cookieName].Value;
            if (string.IsNullOrEmpty(actualLevel))
                return false;

            int actualLevelInt;
            if (!int.TryParse(actualLevel, out actualLevelInt))
            {
                return false;
            }

            return Compare(actualLevelInt);
        }
    }
}
