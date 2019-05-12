using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FuncionarioController : Controller
    {
        // Cria varíavel global para conexão com repositório
        private readonly IFuncionarioRepository _repo;

        public FuncionarioController(IFuncionarioRepository repo)
        {
            // Inicializa repositório no Constructor
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Recupera lista de funcionários
                List<Funcionario> funcionarios = _repo.GetAllFuncionarios();
                return View(funcionarios);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return View();
            }
        }

        [HttpGet]
        public IActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarFuncionario([FromForm] FuncionarioView model)
        {
            try
            {
                // Recupera dados do formulário e cria novo model para salvar no BD
                Funcionario funcionario = new Funcionario()
                {
                    CPF = model.CPF,
                    Email = model.Email,
                    Endereco = model.Endereco,
                    Nome = model.Nome,
                    Tel = model.Tel
                };

                byte[] photo = null;
                string filename = null;

                // Recupera array de bytes da foto caso exista
                if (model.Photo != null)
                {
                    MemoryStream ms = new MemoryStream();
                    model.Photo.CopyTo(ms);

                    photo = ms.ToArray();
                    filename = model.Photo.FileName;
                }

                // Salva funcionário no BD
                _repo.RegisterFuncionario(funcionario, photo, filename);


                TempData["Success"] = "Funcionário cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult EditarFuncionario(string CPF)
        {
            try
            {
                // Recupera funcionário de acordo com CPF informado
                FuncionarioView funcionario = _repo.GetFuncionario(CPF);
                return View(funcionario);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EditarFuncionario([FromForm] FuncionarioView model)
        {
            try
            {
                // Recupera dados do formulário e cria novo model para salvar no BD
                Funcionario funcionario = new Funcionario()
                {
                    CPF = model.CPF,
                    Email = model.Email,
                    Endereco = model.Endereco,
                    Nome = model.Nome,
                    Tel = model.Tel
                };

                byte[] photo = null;
                string filename = null;

                // Cria array de bytes da foto caso exista
                if (model.Photo != null)
                {
                    MemoryStream ms = new MemoryStream();
                    model.Photo.CopyTo(ms);

                    photo = ms.ToArray();
                    filename = model.Photo.FileName;
                }

                // Atualiza valores do funcionário no BD
                bool result = _repo.EditFuncionario(funcionario, photo, filename);

                if (result)
                {
                    TempData["Success"] = "Funcionário editado com sucesso.";
                    return View(model);
                }
                else
                {
                    throw new Exception("Falha ao editar funcionário");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult DeletarFuncionario(string CPF)
        {
            try
            {
                // Remove funcionário no BD
                bool result = _repo.DelFuncionario(CPF);
                if (result)
                {
                    TempData["Success"] = "Funcionário removido com sucesso";
                }
                else
                {
                    throw new Exception("Falha ao remover funcionário");
                }
            } 
            catch (Exception ex)
            {
                TempData["Error"] = ex;
            }

            return RedirectToAction("Index");
        }
    }
}