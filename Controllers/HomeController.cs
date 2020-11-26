using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CadastroUsuarioMVC.Models;
using Microsoft.AspNetCore.Http;

namespace CadastroUsuarioMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            return View();
            
        }
        
        public IActionResult Cadastro(){

            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(Usuario user){
            
            UsuarioRepository ur = new UsuarioRepository();

            ur.Insert(user);
            ViewBag.Mensagem = "Usuário cadastrado com sucesso!";
            return View();
        }

        public IActionResult Listar(){

            UsuarioRepository ur = new UsuarioRepository();

            List<Usuario> listagem = ur.Listar();

            return View(listagem);
        }

        public IActionResult Login(){

            return View();
        }
         
        [HttpPost] 
        public IActionResult Login(Usuario u){

            UsuarioRepository ur = new UsuarioRepository();
            Usuario user = ur.Consulta(u);

            if(user != null) {

                ViewBag.Mensagem = "Você está logado!";
                HttpContext.Session.SetInt32("id", user.id);
                HttpContext.Session.SetString("nome", user.nome);
                return Redirect("Cadastro");
            }

            else {

                ViewBag.Mensagem = "Falha no Login";
                return View();
            }
        }

        public IActionResult Logout(){
        
         HttpContext.Session.Clear();
         return View("Index");
        
        }



    }
}
