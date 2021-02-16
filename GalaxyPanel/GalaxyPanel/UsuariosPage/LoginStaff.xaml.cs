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
	public partial class LoginStaff : ContentPage
	{
		public LoginStaff()
		{
			InitializeComponent();
		}

		async void OnButtonPressed(object sender, EventArgs args)
		{
			if (user.Text == null || user.Text == "")
			{
				await DisplayAlert("Erro", "Usuário inválido ou em branco.", "OK");
			} 
   else if (pass.Text == null || pass.Text == "")
			{
				await DisplayAlert("Erro", "Senha inválida ou em branco.", "OK");
			}
			else
			{

				try
				{
					string userlg = user.Text.Replace("/", "").Replace("&", "").Replace(":", "");
					string senhalg = pass.Text.Replace("/", "").Replace("&", "").Replace(":", "");
					string usuario = Preferences.Get("usuario", "");
					string senha = Preferences.Get("senha", "");
					var Api = new App();
					var jsonGet = await Api.Api("/loginstaff/" + usuario + "/" + senha + "/" + userlg + "/" + senhalg);
					dynamic json = JObject.Parse(jsonGet);

					if (json.status == 1 || json.status == "1")
					{
						await DisplayAlert("Sucesso!", "Pin definido como sucesso!", "OK");
					}
					else if (json.status == 8)
					{
						await DisplayAlert("Erro", "O usuário não tem rank!", "OK");
					}
					else if (json.status == 9)
					{
						await DisplayAlert("Erro", "O usuário não existe!", "OK");
					}
					else if (json.status == 10)
					{
						await DisplayAlert("Erro", "Senha fácil demais!", "OK");
					}
					else
					{
						await DisplayAlert("Erro", "Erro, tente novamente mais tarde!", "OK");
					}
				}
				catch (Exception err)
				{
					await DisplayAlert("Erro", err.ToString(), "OK");
				}
			}
		}
	}
}