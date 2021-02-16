using System;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new Login());
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public async Task<string> Api(string call)
		{
			try
			{
				var client = new WebClient();
				string data = await client.DownloadStringTaskAsync("https://painel.galaxyservers.com.br/app/" + call);
				client.Dispose();
				return data;
			}
			catch (Exception erro)
			{
				return "Erro: " + erro;
			}
		}
	}
}
