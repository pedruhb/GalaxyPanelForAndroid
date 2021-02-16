using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GalaxyPanel.InicioPage
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MenuSlider : TabbedPage
	{
		public MenuSlider()
		{
			InitializeComponent();
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

	}
}