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
    class GrantGatedAccessCondition<T> : StringOperatorCondition<T> where T : RuleContext
    {
        protected override bool Execute(T ruleContext)
        {
            if (String.IsNullOrEmpty(Constants.GatedAccessCookeName))
                return false;

            if (HttpContext.Current.Request.Cookies[Constants.GatedAccessCookeName] == null)
                return false;

            var actualVal = HttpContext.Current.Request.Cookies[Constants.GatedAccessCookeName].Value;

            if (String.IsNullOrEmpty(actualVal))
                return false;

            return Constants.AccessGrantedCookieValue.Equals(actualVal, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}