using System;
using MySqlConnector;
using System.Collections.Generic;

namespace CadastroUsuarioMVC.Models
{
    public class UsuarioRepository
    {
        private const string enderecoConexao = "Database=cadastros; DataSource=localhost; Username=root;";
        
        public void Insert(Usuario user){
          
          MySqlConnection conexao = new MySqlConnection(enderecoConexao);

          conexao.Open();  

          string sqlInsert = "INSERT INTO usuarios (nome, login, senha) VALUES (@nome, @login, @senha)";

          MySqlCommand comando = new MySqlCommand(sqlInsert, conexao);

          comando.Parameters.AddWithValue("@nome", user.nome);
          comando.Parameters.AddWithValue("login", user.login);
          comando.Parameters.AddWithValue("@senha", user.senha);

          comando.ExecuteNonQuery();

          conexao.Close();
          
        }

        public List<Usuario> Listar(){

        MySqlConnection conexao = new MySqlConnection(enderecoConexao);

        conexao.Open();

        string sqlQuery = "SELECT * FROM usuarios ORDER BY nome";

        MySqlCommand comando = new MySqlCommand(sqlQuery, conexao);

        MySqlDataReader dados = comando.ExecuteReader();

        List<Usuario> lista = new List<Usuario>();
         
        while(dados.Read()) {
        Usuario novoUsuario = new Usuario();
      
      novoUsuario.id = dados.GetInt32("id");
      
      if(!dados.IsDBNull(dados.GetOrdinal("nome")))
      novoUsuario.nome = dados.GetString("nome");
      
      if(!dados.IsDBNull(dados.GetOrdinal("login")))
      novoUsuario.login = dados.GetString("login");
      
      if(!dados.IsDBNull(dados.GetOrdinal("senha")))
      novoUsuario.senha = dados.GetString("senha");

        lista.Add(novoUsuario);  
        }
        
        conexao.Close();
        return lista;
        }

        public Usuario Consulta(Usuario u){

          MySqlConnection conexao = new MySqlConnection(enderecoConexao);
          conexao.Open();

          string sqlQuery = "SELECT * FROM usuarios WHERE login = @login AND senha = @senha";

          MySqlCommand comando = new MySqlCommand(sqlQuery, conexao);

          comando.Parameters.AddWithValue("@login", u.login);
          comando.Parameters.AddWithValue("@senha", u.senha);

          MySqlDataReader dados = comando.ExecuteReader();
          
         Usuario user = null;

          if(dados.Read()){

            user = new Usuario();    
            
            user.id = dados.GetInt32("id");

            if(!dados.IsDBNull(dados.GetOrdinal("nome")))
            user.nome = dados.GetString("nome");
            
            if(!dados.IsDBNull(dados.GetOrdinal("login")))
            user.login = dados.GetString("login");

            if(!dados.IsDBNull(dados.GetOrdinal("senha")))
            user.senha = dados.GetString("senha");

          }

          conexao.Close();
          return user; 
        }
    }
}