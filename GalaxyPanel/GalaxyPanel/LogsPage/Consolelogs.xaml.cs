using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel.LogsPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Consolelogs : ContentPage
	{
		public Consolelogs()
		{
			InitializeComponent();
			GetLogs();

			ListView.RefreshCommand = new Command(() => {
				GetLogs();
				ListView.IsRefreshing = false;
			});

		}

		async void GetLogs()
		{
			try { 
			string usuario = Preferences.Get("usuario", "");
			string senha = Preferences.Get("senha", "");
			var Api = new App();
			var jsonGalaxy = await Api.Api("/hotellogs/consolelogs/" + usuario + "/" + senha + "/200");
			dynamic json = JObject.Parse(jsonGalaxy);
			var Lista = new List<Model>
			{
			};
			if (json.status != 1)
			{
				await DisplayAlert("Erro", "Erro ao obter logs..", "OK");
			}
			else
			{
				foreach (var item in json.logs)
				{
					Lista.Add(new Model { Log = item.log, Info = item.info });
				}
			}
			ListView.ItemsSource = Lista;
		}
			catch (Exception)
			{
				await DisplayAlert("Erro", "Erro ao obter logs..", "OK");
	}
}

		public class Model
		{
			public string Log { get; set; }
			public string Info { get; set; }
		}


	}
}