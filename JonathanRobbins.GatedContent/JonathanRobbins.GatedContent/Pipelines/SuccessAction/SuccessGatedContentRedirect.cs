using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Sitecore.Diagnostics;
using Sitecore.Form.Core.Pipelines.FormSubmit;
using Sitecore.Web;
using Sitecore.Web.UI.Sheer;
using Sitecore.Xml;

namespace JonathanRobbins.GatedContent.Pipelines.SuccessAction
{
    public class SuccessGatedContentRedirect : ClientPipelineArgs
    {
        private List<string> _gatedContentSaveActionIds = new List<string>();

        public void Process(SubmitSuccessArgs args)
        {
            Assert.IsNotNull((object)args, "args");

            if (args.Form != null)
            {
                if (!args.Form.SuccessRedirect && FormHasGrantGatedAccessSaveAction(args))
                {
                    string urlString = HttpContext.Current.Request.Url.AbsoluteUri;

                    WebUtil.Redirect(urlString, true);
                }
            }
        }

        public void AddGatedContentSaveActionIds(XmlNode configNode)
        {
            Assert.ArgumentNotNull(configNode, "configNode");
            string attributeValue = XmlUtil.GetAttribute("value", configNode);
            _gatedContentSaveActionIds.Add(attributeValue);
        }

        private bool FormHasGrantGatedAccessSaveAction(SubmitSuccessArgs args)
        {
            bool isGatedForm = false;

            foreach (string gatedContentSaveActionId in _gatedContentSaveActionIds)
            {
                isGatedForm = args.Form.SaveActions.ToLower().Trim().Contains(gatedContentSaveActionId.Trim().ToLower());
                if (isGatedForm)
                    break;
            }

            return isGatedForm;
        }
    }
}