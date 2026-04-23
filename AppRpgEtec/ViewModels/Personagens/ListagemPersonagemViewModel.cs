using AppRpgEtec.Models;
using AppRpgEtec.Services.Personagens;
using AppRpgEtec.Views.Armas;
using AppRpgEtec.Views.Personagens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class ListagemPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;
        public ICommand DirecionarArmasCommand { get; set; }
        public ObservableCollection<Personagem> Personagens { get; set; }
        public ICommand NovoPersonagemCommand { get; }

        public ListagemPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
            Personagens = new ObservableCollection<Personagem>();

            _ = ObterPersonagens();
            InicializarCommands();

            NovoPersonagemCommand = new Command(async () => { await ExibirCadastroPersonagem(); });
        }

        public void InicializarCommands()
        {
            DirecionarArmasCommand = new Command(async () => await DirecionarParaArmas());
        }

        public async Task ObterPersonagens()
        {
            try //Junto com o Catch evitará que erros fechem o aplicativo
            {
                Personagens = await pService.GetPersonagensAsync();
                OnPropertyChanged(nameof(Personagens)); //Informará a View que houve carregamento
            }
            catch (Exception ex)
            {
                //Captará o erro para exibir em tela
                await Application.Current.MainPage
                    .DisplayAlert("Ops", ex.Message + " Detalhes: " + ex.InnerException, "ok");
            }
        }
        public async Task DirecionarParaArmas()//Método para exibição da view de armas
        {
            Application.Current.MainPage = new ListagemArmasView();
        }

        public async Task ExibirCadastroPersonagem()
        {
            try
            {
                await Shell.Current.GoToAsync("cadPersonagemView");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }
    }
}
