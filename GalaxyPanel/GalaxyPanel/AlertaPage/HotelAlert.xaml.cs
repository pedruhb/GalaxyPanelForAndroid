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

namespace GalaxyPanel.AlertaPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class HotelAlert : ContentPage
	{
		public HotelAlert()
		{
			InitializeComponent();
		}

		async void OnButtonSendAlertPressed(object sender, EventArgs args)
		{
			if (mensagem.Text == null || mensagem.Text == "")
			{
				await DisplayAlert("Erro", "Mensagem inválida ou em branco.", "OK");
			}
			else
			{
				try
				{
					string msg = mensagem.Text.Replace("/", "").Replace("&", "");
					string usuario = Preferences.Get("usuario", "");
					string senha = Preferences.Get("senha", "");
					var Api = new App();
					var jsonGet = await Api.Api("/alert/hotelalert/hotelalert/" + usuario + "/" + senha + "/" + msg);
					dynamic json = JObject.Parse(jsonGet);
					if (json.status == 8)
					{
						await DisplayAlert("Sucesso", "Alerta enviado com sucesso!", "OK");
					}
					else if (json.status == 9)
					{
						await DisplayAlert("Erro", "O emulador está desligado!", "OK");
					}
					else
					{
						await DisplayAlert("Erro", "Erro ao enviar alerta!", "OK");
					}
				}
				catch (Exception)
				{
					await DisplayAlert("Erro", "Erro ao enviar alerta!", "OK");
				}
			}
		}
	}
}