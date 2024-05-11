using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Dtwo.API.View;
using Dtwo.Plugins.MultiAccount.View.Pages;

namespace Dtwo.Plugins.MultiAccount.View
{
	public class MultiAccount_ViewPlugin : ViewPlugin
	{
		public MultiAccount_ViewPlugin(Plugins.PluginInfos infos, Assembly assembly) : base(infos, assembly)
		{
			
		}

		public override Dictionary<string, Page> Pages => new Dictionary<string, Page>()
		{
			{ "MultiAccount",
				new Page(typeof(MainWindow),
				"MultiAccount", "Multi compte",
				true, true,
				new List<Tab>()
				{
					new Tab("Home", typeof(Components.Startup)),
					new Tab("Options", typeof(Components.Options))
				})
			},
			//{ "MultiAccountWindow", new Page(typeof(FloatingWindow), "MultiAccountWindow", "MultiAccountWindow", false) },
			//{ "MultiAccountWindow_Options", new Page(typeof(FloatingWindowAccountOptions), "MultiAccountWindow_Options", "MultiAccountWindow_Options", false) }
		};
	}
}
