using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepository _repo;

        public FuncionarioController(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("Funcionario/")]
        public IActionResult Index()
        {
            try
            {
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
        [Route("Funcionario/CadastrarFuncionario")]
        public IActionResult CadastrarFuncionario()
        {
            return View();
        }

        [HttpPost]
        [Route("Funcionario/CadastrarFuncionario")]
        public IActionResult CadastrarFuncionario([FromForm] FuncionarioView model)
        {
            try
            {
                Funcionario funcionario = new Funcionario()
                {
                    CPF = model.CPF,
                    Email = model.Email,
                    Endereco = model.Endereco,
                    Nome = model.Nome,
                    Tel = model.Tel
                };

                MemoryStream ms = new MemoryStream();
                model.Photo.CopyTo(ms);

                byte[] photo = ms.ToArray();
                string filename = model.Photo.FileName;
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
        [Route("Funcionario/EditarFuncionario")]
        public IActionResult EditarFuncionario(string CPF)
        {
            try
            {
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
        [Route("Funcionario/EditarFuncionario")]
        public IActionResult EditarFuncionario([FromForm] Funcionario funcionario)
        {
            try
            {
                bool result = _repo.EditFuncionario(funcionario);
                if (result)
                {
                    TempData["Success"] = "Funcionário editado com sucesso.";
                    return View(funcionario);
                }
                else
                {
                    throw new Exception("Falha ao editar funcionário");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return View(funcionario);
            }
        }

        [HttpGet]
        [Route("Funcionario/DeletarFuncionario")]
        public IActionResult DeletarFuncionario(string CPF)
        {
            try
            {
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