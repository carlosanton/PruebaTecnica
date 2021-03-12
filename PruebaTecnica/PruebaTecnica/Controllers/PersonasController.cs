using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PruebaTecnica.Models;
using System;
using System.Linq;

namespace PruebaTecnica.Controllers
{
    public class PersonasController : Controller
    {
        private DbContexto _contexto;

        public PersonasController(DbContexto contexto)
        {
            _contexto = contexto;

            if(_contexto.Sexo.ToList().Count == 0)
            {
                Sexo masculino = new Sexo
                {
                    Tipo = "Masculino"
                };
                Sexo femenino = new Sexo
                {
                    Tipo = "Femenino"
                };
                _contexto.Sexo.Add(masculino);
                _contexto.Sexo.Add(femenino);
                _contexto.SaveChanges();
            }
        }

        public IActionResult Index()
        {
            var personas = _contexto.Persona.ToList();
            ViewBag.ListaSexos = _contexto.Sexo.ToList();
            return View(personas);
        }

        public IActionResult Create(int Id = 0)
        {
            var persona = _contexto.Persona.Where(x => x.Id == Id).FirstOrDefault();

            if (persona == null)
            {
                persona = new Persona();
            }

            ViewBag.ListaSexos = _contexto.Sexo.ToList().Select(i => new SelectListItem()
            {
                Text = i.Tipo,
                Value = i.Id.ToString()
            });

            return View(persona);
        }

        public IActionResult Delete(int Id)
        {
            var persona = _contexto.Persona.Where(x => x.Id == Id).FirstOrDefault();

            if(persona != null)
            {
                _contexto.Persona.Remove(persona);
                _contexto.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Create(Persona persona)
        {
            var a = Request.Form["IdSexo"];
            try
            {
                if(persona.Id > 0)
                {
                    // Editamos
                    var personaDDBB = _contexto.Persona.Where(x => x.Id == persona.Id).FirstOrDefault();

                    personaDDBB.Nombre = persona.Nombre;
                    personaDDBB.Apellidos = persona.Apellidos;
                    personaDDBB.CodigoPostal = persona.CodigoPostal;
                    personaDDBB.Direccion = persona.Direccion;
                    personaDDBB.Pais = persona.Pais;
                    personaDDBB.IdSexo = persona.IdSexo;
                    personaDDBB.FechaNacimiento = persona.FechaNacimiento;
                }
                else
                {
                    // Nuevo
                    _contexto.Persona.Add(persona);
                }

                _contexto.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }
    }
}
