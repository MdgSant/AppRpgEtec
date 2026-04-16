using AppRpgEtec.Services.Personagens;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppRpgEtec.ViewModels.Personagens
{
    public class CadastroPersonagemViewModel : BaseViewModel
    {
        private PersonagemService pService;

        public CadastroPersonagemViewModel()
        {
            string token = Preferences.Get("UsuarioToken", string.Empty);
            pService = new PersonagemService(token);
        }
        public int Id 
        { 
            get => id; 
            set
            {
                Id = value;
                OnPropertyChanged();
            }
        }
        private string Nome { get; set; }
        private int PontosVida { get; set; }
        private int Forca { get; set; }
        private int Defesa { get; set; }
        private int Inteligencia { get; set; }
        private int Disputas { get; set; }
        private int Vitorias { get; set; }
        private int Derrotas { get; set; }
    }
}
