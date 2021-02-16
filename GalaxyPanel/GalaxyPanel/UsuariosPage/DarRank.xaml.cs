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
	public partial class DarRank : ContentPage
	{
		public DarRank()
		{
			InitializeComponent();
			GetRanks();
		}

		public class Model
		{
			public string Usuario { get; set; }
			public string Info { get; set; }
		}

		async void GetRanks()
		{
			try
			{
				string usuario = Preferences.Get("usuario", "");
				string senha = Preferences.Get("senha", "");
				var Api = new App();
				var jsonGalaxy = await Api.Api("/getranks/" + usuario + "/" + senha + "");
				dynamic json = JObject.Parse(jsonGalaxy);

				if (json.status != 1)
				{
					await DisplayAlert("Erro", "Erro ao obter ranks..", "OK");
				}
				else
				{
					var Lista = new List<Model>
					{
					};

					foreach (var item in json.ranks)
					{
						Lista.Add(new Model { Usuario = item.id, Info = item.nome });
					}

					Picker.ItemsSource = Lista;
				}

			}
			catch (Exception)
			{
				await DisplayAlert("Erro", "Erro ao obter ranks..", "OK");
			}
		}


		async void OnButtonPressed(object sender, EventArgs args)
		{
			var el = (Model)Picker.SelectedItem;

			if (user.Text == null || user.Text == "")
			{
				await DisplayAlert("Erro", "Usuário inválido ou em branco.", "OK");
			}
			else if (Picker.SelectedIndex == -1 || el == null)
			{
				await DisplayAlert("Erro", "Rank inválido ou em branco.", "OK");
			}
			else
			{

				try
				{
					var rank = Convert.ToInt32(el.Usuario);
					string userlg = user.Text.Replace("/", "").Replace("&", "").Replace(":", "");
					string usuario = Preferences.Get("usuario", "");
					string senha = Preferences.Get("senha", "");
					var Api = new App();
					var jsonGet = await Api.Api("/rank/" + usuario + "/" + senha + "/" + userlg + "/" + rank );
					dynamic json = JObject.Parse(jsonGet);

					if (json.status == 1 || json.status == "1")
					{
						await DisplayAlert("Sucesso!", ""+ json.mensagem+" ", "OK");
					}
					else if (json.status == 9)
					{
						await DisplayAlert("Erro", "O usuário não existe!", "OK");
					}
					else if (json.status == 10)
					{
						await DisplayAlert("Erro", "Rank inválido", "OK");
					}
					else
					{
						await DisplayAlert("Erro", "Erro, tente novamente mais tarde!", "OK");
					}
				}
				catch (Exception e)
				{
					await DisplayAlert("Erro", "Erro: "+e, "OK");
				}
			}
		}
		
	}
}