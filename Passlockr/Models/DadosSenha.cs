using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Passlockr.Models
{
    public class DadosSenha
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }

        [Display(Name = "Descrição da senha")]
        public string Descricao { get; set; }

        [Display(Name = "E-mail ou nome de usuário")]
        public string Login { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Data de criação")]
        public DateTime DataCriacao { get; set; }

        [Display(Name = "Última edição")]
        public DateTime DataEdicao { get; set; }

        public override string ToString()
        {
            return $"{Id}, {IdUsuario}, {Descricao}, {Senha}";
        }

        public bool Valido()
        {
            bool idUsuarioValido = (IdUsuario != null) && (!IdUsuario.Equals(""));
            bool descricaoValida = (Descricao != null) && (!Descricao.Equals(""));
            bool senhaValida = (Senha != null) && (!Senha.Equals(""));
            bool loginValido = (Login != null) && (!Login.Equals(""));

            return idUsuarioValido && descricaoValida && senhaValida && loginValido;
        }
    }
}