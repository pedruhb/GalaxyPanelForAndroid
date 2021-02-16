using Plugin.LatestVersion;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel.InicioPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Menu : MasterDetailPage
	{
		public Menu()
		{
			InitializeComponent();
			Detail = new NavigationPage(new MenuSlider());
			HotelNameMenu.Text = Preferences.Get("hotelname", "Habbo")+" Hotel";
			VersaoMenu();
		}

		public async void VersaoMenu()
		{
			string latestVersionNumber = await CrossLatestVersion.Current.GetLatestVersionNumber();
			AppInfo.Text = "GalaxyPanel APP v" + latestVersionNumber;
		}

		private void Inicio(object sender, System.EventArgs e)
		{
			Detail.Navigation.PushAsync(new MenuSlider());
			IsPresented = false;
		}

		private void Logs(object sender, System.EventArgs e)
		{
			Detail.Navigation.PushAsync(new LogsPage.LogsMenu());
			IsPresented = false;
		}
		private void Alerta(object sender, System.EventArgs e)
		{
			Detail.Navigation.PushAsync(new AlertaPage.AlertaPage());
			IsPresented = false;
		}

		private async void Sair(object sender, System.EventArgs e)
		{
			var answer = await DisplayAlert("Sair", "Deseja sair do app?", "Sim", "Não");
			if (answer)
			{
				//System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
				System.Environment.Exit(0);
			}

		}


		private void Usuarios(object sender, System.EventArgs e)
		{
			Detail.Navigation.PushAsync(new UsuariosPage.UserMenu()); 
			IsPresented = false;
		}

		private async void Compartilhar(object sender, System.EventArgs e)
		{
			await Share.RequestAsync(new ShareTextRequest
			{
				Text = "Acesse já e faça parte da comunidade do "+ HotelNameMenu.Text+ ": "+ Preferences.Get("linkhotel", ""),
				Title = "Compartilhar Hotel"
			});
			IsPresented = false;
		}
	}
}