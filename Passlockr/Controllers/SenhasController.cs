using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Passlockr.Models;

namespace Passlockr.Controllers
{
    [Authorize]
    public class SenhasController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private const string ChaveCriptografia = "F1DFD2077ACF8F2F94E42CDEDC3E8BED";

        public SenhasController()
        {
            _dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }

        // GET: Senhas
        public ActionResult Index()
        {
            string idUsuarioAtual = User.Identity.GetUserId();
            var senhasUsuario = _dbContext.DadoSenha.Where(s => s.IdUsuario.Equals(idUsuarioAtual)).ToList();
            return View(senhasUsuario);
        }

        // GET: Senhas/Criar
        public ActionResult Criar()
        {
            return View();
        }


        // POST: Senhas/Adicionar
        [HttpPost]
        public ActionResult Adicionar(DadosSenha dadosSenha)
        {
            dadosSenha.IdUsuario = User.Identity.GetUserId();
            dadosSenha.DataCriacao = DateTime.Now;
            dadosSenha.DataEdicao = DateTime.Now;
            dadosSenha.Senha = Encriptar(dadosSenha.Senha);
            ValidarLogin(ref dadosSenha);
            _dbContext.DadoSenha.Add(dadosSenha);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index), "Senhas");
        }

        // POST: Senhas/Remover
        [HttpPost]
        public ActionResult Remover(int id)
        {
            if (_dbContext != null)
            {
                var dadosDeSenha = _dbContext.DadoSenha.SingleOrDefault(c => c.Id == id);

                if (dadosDeSenha != null)
                {
                    _dbContext.DadoSenha.Remove(dadosDeSenha);
                    _dbContext.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index), "Senhas");
        }

        public ActionResult ConfirmarRemocao(int id)
        {
            var dadosDeSenha = _dbContext.DadoSenha.SingleOrDefault(c => c.Id == id);

            if (dadosDeSenha != null)
            {
                return View(dadosDeSenha);
            }

            return RedirectToAction(nameof(Index), "Senhas");
        }

        // GET: Senhas/Editar
        public ActionResult Editar(int id)
        {
            var dadosDeSenha = _dbContext?.DadoSenha.SingleOrDefault(c => c.Id == id);

            if (dadosDeSenha == null)
                return HttpNotFound();

            dadosDeSenha.Senha = Desencriptar(dadosDeSenha.Senha);
            return View(dadosDeSenha);
        }

        // POST: Senhas/Atualizar
        [HttpPost]
        public ActionResult Atualizar(DadosSenha dadosAtualizados)
        {
            if ((_dbContext != null) && (dadosAtualizados.Valido()))
            {
                if (dadosAtualizados.Id == 0)
                    _dbContext.DadoSenha.Add(dadosAtualizados);
                else
                {
                    var dadosOriginais = _dbContext.DadoSenha.Single(c => c.Id == dadosAtualizados.Id);
                    dadosOriginais.Descricao = dadosAtualizados.Descricao;
                    dadosOriginais.Senha = Encriptar(dadosAtualizados.Senha);
                    dadosOriginais.DataEdicao = DateTime.Now;
                    ValidarLogin(ref dadosOriginais);
                }

                _dbContext.SaveChanges();
            }

            return RedirectToAction(nameof(Index), "Senhas");
        }

        public ActionResult Detalhes(int id)
        {
            var dadosDeSenha = _dbContext.DadoSenha.SingleOrDefault(c => c.Id == id);

            if (dadosDeSenha == null)
                return HttpNotFound();

            return View(dadosDeSenha);
        }

        public ActionResult Obter(int id)
        {
            var dadosDeSenha = _dbContext.DadoSenha.SingleOrDefault(c => c.Id == id);

            if ((dadosDeSenha != null) && (dadosDeSenha.Valido()))
            {
                return File(
                    Encoding.UTF8.GetBytes($"Login: {dadosDeSenha.Login}\nSenha: {Desencriptar(dadosDeSenha.Senha)}"),
                    "text/plain",
                    $"Passlockr - {dadosDeSenha.Descricao}.txt");
            }

            return RedirectToAction(nameof(Index), "Senhas");
        }

        private string Encriptar(string senha)
        {
            byte[] senhaEncriptada = null;
            var aes = Aes.Create();

            if (aes == null)
                return null;

            aes.Key = Encoding.UTF8.GetBytes(ChaveCriptografia);
            aes.IV = new byte[16];
            var encriptador = aes.CreateEncryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encriptador, CryptoStreamMode.Write))
                {
                    using (var streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(senha);
                    }

                    senhaEncriptada = memoryStream.ToArray();
                }
            }

            return Convert.ToBase64String(senhaEncriptada);
        }

        private string Desencriptar(string senhaEncriptada)
        {
            Aes aes = Aes.Create();

            if (aes == null)
                return null;

            aes.Key = Encoding.UTF8.GetBytes(ChaveCriptografia);
            aes.IV = new byte[16];
            var desencriptador = aes.CreateDecryptor(aes.Key, aes.IV);

            using (var memoryStream = new MemoryStream(Convert.FromBase64String(senhaEncriptada)))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, desencriptador, CryptoStreamMode.Read))
                {
                    using (var streamReader = new StreamReader(cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }

        private void ValidarLogin(ref DadosSenha dadosSenha)
        {
            if ((dadosSenha.Login == null) || (dadosSenha.Login.Equals("")))
            {
                dadosSenha.Login = "Não possui";
            }
        }
    }
}