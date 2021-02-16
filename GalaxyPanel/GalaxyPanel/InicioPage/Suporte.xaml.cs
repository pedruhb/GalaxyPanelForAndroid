using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel.InicioPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Suporte : ContentPage
	{

		public string source = "https://painel.galaxyservers.com.br/app/chat/"+ Preferences.Get("usuario", "") + "/" + Preferences.Get("senha", "");
		public Suporte()
		{
			InitializeComponent();
			webview.Source = source;
		}

		protected override bool OnBackButtonPressed()
		{
			if (webview.CanGoBack)
			{
				webview.GoBack();
			} else
			{
				webview.Source = source;
			}
			return true;
		}

	}
}