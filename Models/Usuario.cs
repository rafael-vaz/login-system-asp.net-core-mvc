using System;
using MySqlConnector;
using System.Collections.Generic;
namespace CadastroUsuarioMVC.Models
{
    public class Usuario
    {
        
        public int id {get; set;}
        public string nome {get; set;}
        public string login {get; set;}
        public string senha {get; set;}
    }
}