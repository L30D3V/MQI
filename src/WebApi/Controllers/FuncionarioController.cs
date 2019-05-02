using System;
using System.Collections.Generic;
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
        public IActionResult CadastrarFuncionario([FromForm] Funcionario funcionario)
        {
            try
            {
                _repo.RegisterFuncionario(funcionario);

                TempData["Success"] = "Funcionário cadastrado com sucesso";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex;
                return View(funcionario);
            }
        }

        [HttpGet]
        [Route("Funcionario/EditarFuncionario")]
        public IActionResult EditarFuncionario(string CPF)
        {
            try
            {
                Funcionario funcionario = _repo.GetFuncionario(CPF);
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