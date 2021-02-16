using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GalaxyPanel
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class Login : ContentPage
	{

		public Login()
		{
			InitializeComponent();
			user.Text = Preferences.Get("usuario", "");
			pass.Text = Preferences.Get("senha", "");
		}

		protected override bool OnBackButtonPressed()
		{
			ShowExitDialog();
			return true;
		}
		private async void ShowExitDialog()
		{
			var answer = await DisplayAlert("Sair", "Deseja sair do app?", "Sim", "Não");
			if (answer)
			{
				System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
			}
		}

		protected void GoGalaxy(object sender, EventArgs e)
		{
			Device.OpenUri(new Uri("https://cliente.galaxyservers.com.br/cart.php"));
		}

		async void OnButtonLoginPressed(object sender, EventArgs args)
		{
			actInd.IsVisible = true;
			actInd.IsRunning = true;
			botao.IsVisible = false;

			string usuario = user.Text;
			string senha = pass.Text;

			if (usuario == "" || usuario == null)
			{
				await DisplayAlert("Erro ao realizar login", "Usuário vazio.", "OK");
			}
			else if (senha == "" || senha == null)
			{
				await DisplayAlert("Erro ao realizar login", "Senha vazia.", "OK");
			}
			else
			{

				var Api = new App();
				var jsonGet = await Api.Api("/login/" + usuario + "/" + senha + "");
				dynamic json = JObject.Parse(jsonGet);

				if (json.status == "1" || json.status == 1)
				{
					string hotelname = json.hotelname;
					string linkhotel = json.linkhotel;
					Preferences.Set("usuario", usuario);
					Preferences.Set("senha", senha);
					Preferences.Set("hotelname", hotelname);
					Preferences.Set("linkhotel", linkhotel);
					await Navigation.PushAsync(new InicioPage.Menu());
				}
				else if (json.status == "2" || json.status == 2)
				{
					await DisplayAlert("Erro ao realizar login", "Usuário inexistente.", "OK");
				}
				else if (json.status == "3" || json.status == 3)
				{
					await DisplayAlert("Erro ao realizar login", "Senha inválida.", "OK");
				}
				else if (json.status == "4" || json.status == 4)
				{
					await DisplayAlert("Erro ao realizar login", "Dados inválidos.", "OK");
				}
				else if (json.status == "5" || json.status == 5)
				{
					await DisplayAlert("Erro ao realizar login", "Tente novamente mais tarde.", "OK");
				}
				else if (json.status == "6" || json.status == 6)
				{
					await DisplayAlert("Erro ao realizar login", "Hotel acessível apenas via pin ou sms.", "OK");
				}
				else if (json.status == "7" || json.status == 7)
				{
					await DisplayAlert("Erro ao realizar login", "Hotel suspenso.", "OK");
				}
				else
				{
					await DisplayAlert("Erro", "Erro " + json.status, "OK");
				}
			}

			actInd.IsVisible = false;
			actInd.IsRunning = false;
			botao.IsVisible = true;

		}
	}
}


