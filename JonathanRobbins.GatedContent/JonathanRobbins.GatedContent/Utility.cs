using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Data.Query;
using Sitecore.Exceptions;

namespace JonathanRobbins.GatedContent
{
    public class Utility
    {
        public virtual string DefineCookieName(string level)
        {
            return Constants.AccessGrantedCookieValue + level;
        }

        public virtual int GetCookieLifeSpan()
        {
            string daysString = Sitecore.Configuration.Settings.GetSetting("CookieLifeSpanInDays");

            if (string.IsNullOrEmpty(daysString))
            {
                throw new ConfigurationException("Unable to find the CookieLifeSpanInDays setting which should be in the jonathanrobbins.GatedContent.config file. " +
                                                 "It is required to determine the lifespan of a gated content cookie");
            }

            int daysInt;
            if (int.TryParse(daysString, out daysInt))
            {
                return daysInt;
            }
            else
            {
                throw new ParseException("Unable to parse the value in the CookieLifeSpanInDays setting of the JonathanRobbins.GatedContent.config. It should be an int.");
            }
        }
    }
}
