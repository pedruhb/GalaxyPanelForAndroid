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

namespace GalaxyPanel.UsuariosPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class QuartosCarregados : ContentPage
	{
		public QuartosCarregados()
		{
			InitializeComponent();
			GetQuartos();

			ListView.RefreshCommand = new Command(() => {
				GetQuartos();
				ListView.IsRefreshing = false;
			});

		}

		async void GetQuartos()
		{
			try
			{
			string usuario = Preferences.Get("usuario", "");
			string senha = Preferences.Get("senha", "");
			var Api = new App();
			var jsonGalaxy = await Api.Api("/quartos/" + usuario + "/" + senha + "");
			dynamic json = JObject.Parse(jsonGalaxy);
			var Lista = new List<Model>
			{
			};
			if (json.status != 1)
			{
				await DisplayAlert("Erro", "Erro ao obter quartos..", "OK");
			}
			else
			{
						foreach (var item in json.usuarios)
						{
							Lista.Add(new Model { Quarto = item.quarto, Info = item.info });
						}
			}
			ListView.ItemsSource = Lista;
		}
			catch (Exception)
			{
				await DisplayAlert("Erro", "Erro ao obter quartos..", "OK");
	}
}

		public class Model
		{
			public string Quarto { get; set; }
			public string Info { get; set; }
		}


	}
}