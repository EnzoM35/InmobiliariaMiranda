using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmobiliariaULP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaULP.Controllers
{

    public class PropietariosController : Controller
    {
        private readonly RepositorioPropietario repositorio;
        
        public PropietariosController()
        {
           this.repositorio = new RepositorioPropietario();
        }


        // GET: Propietarios
        public ActionResult Index()
        {
             try
            {
                var lista = repositorio.ListarTodos();
                ViewBag.Id = TempData["Id"];
                // TempData es para pasar datos entre acciones
                // ViewBag/Data es para pasar datos del controlador a la vista
                // Si viene alguno valor por el tempdata, lo paso al viewdata/viewbag
                if (TempData.ContainsKey("Mensaje"))
                    ViewBag.Mensaje = TempData["Mensaje"];
                return View(lista);
            }
			catch (Exception)
            {// Poner breakpoints para detectar errores
				throw;
			}
        }

        // GET: Propietarios/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Propietarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Propietario propietario)
        {
            try
            {
                if (ModelState.IsValid)// Pregunta si el modelo es válido
                {
                    repositorio.Alta(propietario);
                    TempData["Id"] = propietario.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(propietario);
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // GET: Propietarios/Edit/5
        public ActionResult Edit(int id)
        {
             try
            {
                var entidad = repositorio.ListarPorId(id);
                return View(entidad);//pasa el modelo a la vista
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // POST: Propietarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
             // Si en lugar de IFormCollection ponemos Propietario, el enlace de datos lo hace el sistema
            Propietario p = null;
            try
            {
                p = repositorio.ListarPorId(id);
                // En caso de ser necesario usar: 
                //
                //Convert.ToInt32(collection["CAMPO"]);
                //Convert.ToDecimal(collection["CAMPO"]);
                //Convert.ToDateTime(collection["CAMPO"]);
                //int.Parse(collection["CAMPO"]);
                //decimal.Parse(collection["CAMPO"]);
                //DateTime.Parse(collection["CAMPO"]);
                ////////////////////////////////////////
                p.Nombre = collection["Nombre"];
                p.Apellido = collection["Apellido"];
                p.Dni = collection["Dni"];
                p.Email = collection["Email"];
                p.Telefono = collection["Telefono"];
                repositorio.Modificacion(p);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // GET: Propietarios/Delete/5
        public ActionResult Delete(int id)
        {
            	try
			{
                var entidad = repositorio.ListarPorId(id);
                return View(entidad);
            }
            catch (Exception ex)
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // POST: Propietarios/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
              try
            {
                repositorio.Baja(id);
                TempData["Mensaje"] = "Eliminación realizada correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {//poner breakpoints para detectar errores
                throw;
            }
        }
    }
}