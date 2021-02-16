using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel.InicioPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
		readonly string usuario = Preferences.Get("usuario", "");
		readonly string senha = Preferences.Get("senha", "");

		public Inicio()
		{
			InitializeComponent();
			StatusDoHotel();

			ListviewInicio.RefreshCommand = new Command(() => {
				StatusDoHotel();
				ListviewInicio.IsRefreshing = false;
			});

		}


		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			var id = ((Model)ListviewInicio.SelectedItem).Id;

			switch (id)
			{
				case 1:
					Navigation.PushAsync(new UsuariosPage.Onlines());
					break;

				case 2:
					Navigation.PushAsync(new UsuariosPage.Registrados());
					break;

				case 3:
					Navigation.PushAsync(new UsuariosPage.QuartosCarregados());
					break;

				case 5:
					Navigation.PushAsync(new UsuariosPage.StaffsOnline());
					break;

				case 6:
					AlertUptime();
					break;

			}


		}

		async void AlertUptime()
		{
			try
			{
				var Api = new App();
				var jsonGet = await Api.Api("/status/" + usuario + "/" + senha + "");
				dynamic json = JObject.Parse(jsonGet);
				await DisplayAlert("Uptime do Emulador", "Tempo ligado: "+json.uptime, "OK");

			} catch (Exception e)
			{
				await DisplayAlert("Erro", "Erro ao obter uptime: "+e, "OK");
			}
		}

		async void StatusDoHotel()
		{
			var Lista = new List<Model>{};
			var Api = new App();
			var jsonGet = await Api.Api("/status/" + usuario + "/" + senha + "");
			dynamic json = JObject.Parse(jsonGet);

			if (json.status == 6)
			{

				/// Cards
				Lista.Add(new Model { Id = 1, Icone = "users", Texto1 = "Usuários onlines", Texto2 = json.onlines });
				Lista.Add(new Model { Id = 2, Icone = "users", Texto1 = "Usuários registrados", Texto2 = json.userstotal });
				Lista.Add(new Model { Id = 3, Icone = "home256", Texto1 = "Quartos carregados", Texto2 = json.quartoscarregados });
				Lista.Add(new Model { Id = 4, Icone = "star", Texto1 = "Record de onlines", Texto2 = json.recordon });
				Lista.Add(new Model { Id = 5, Icone = "userscirculo", Texto1 = "Staffs onlines", Texto2 = json.staffsonlines });

				/// Emulador
				if (json.emulador == 2)
				{
					botaoEmulador.Text = "Desligar emulador";
					botaoEmulador.BackgroundColor = Color.FromHex("#dc3545");
					Lista.Add(new Model { Id = 6, Icone = "cog", Texto1 = "Emulador ligado.", Texto2 = "" });
				}
				else
				{
					botaoEmulador.Text = "Ligar emulador";
					botaoEmulador.BackgroundColor = Color.FromHex("#31d67b");
					Lista.Add(new Model { Id = 6, Icone = "cog", Texto1 = "Emulador desligado.", Texto2 = "" });
				}

				/// Manda pra lista
				ListviewInicio.ItemsSource = Lista;
			}
			else
			{
				await DisplayAlert("Erro", "Erro ao obter dados do hotel.", "OK");
			}
		}

		async void OnButtonEmuladorPressed(object sender, EventArgs args)
		{
			try
			{
				var Api = new App();
				var jsonGet = await Api.Api("/emulador/" + usuario + "/" + senha);

				dynamic json = JObject.Parse(jsonGet);

				if (json.status == 1)
				{
					await DisplayAlert("Erro", "Erro de conexão com o GalaxyPanel.", "OK");
				}
				else if (json.status == 2)
				{
					await DisplayAlert("Erro", "Dados inválidos.", "OK");
				}
				else if (json.status == 3)
				{
					await DisplayAlert("Erro", "Login por dados desativado.", "OK");
				}
				else if (json.status == 4)
				{
					await DisplayAlert("Erro", "Hotel suspenso.", "OK");
				}
				else if (json.status == 5)
				{
					await DisplayAlert("Sucesso", "Emulador desligado!", "OK");
					botaoEmulador.Text = "Ligar emulador";
					botaoEmulador.BackgroundColor = Color.FromHex("#31d67b");
				}
				else if (json.status == 6)
				{
					await DisplayAlert("Sucesso", "Emulador ligado!", "OK");
					botaoEmulador.Text = "Desligar emulador";
					botaoEmulador.BackgroundColor = Color.FromHex("#dc3545");
				}
				else if (json.status == 7)
				{
					await DisplayAlert("Erro", "Não foi possível conectar ao hotel.", "OK");
				}
				else
				{
					await DisplayAlert("Erro", "Erro número " + json.status, "OK");
				}
			} catch (Exception e)
			{
				await DisplayAlert("Erro", "Erro: " + e, "OK");
			}
		}

		public class Model
		{
			public int Id { get; set; }
			public string Icone { get; set; }
			public string Texto1 { get; set; }
			public string Texto2 { get; set; }
		}

	}
}