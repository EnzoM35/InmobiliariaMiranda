using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InmobiliariaULP.Models;

namespace InmobiliariaULP.Controllers
{
    
    public class InquilinosController : Controller
    {

        private readonly RepositorioInquilino repositorio;
        
        public InquilinosController(){
            this.repositorio = new RepositorioInquilino();
        }
        // GET: Inquilinos
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

        // GET: Inquilinos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Inquilinos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino inquilino)
        {
            try
            {
                if (ModelState.IsValid)// Pregunta si el modelo es válido
                {
                    repositorio.Alta(inquilino);
                    TempData["Id"] = inquilino.Id;
                    return RedirectToAction(nameof(Index));
                }
                else
                    return View(inquilino);
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // GET: Inquilinos/Edit/5
        public ActionResult Edit(int id)
        {
               try
            {
                var entidad = repositorio.ListarPorId(id);
                return View(entidad);//pasa el modelo a la vista
            }
            catch (Exception)
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // POST: Inquilinos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
                  // Si en lugar de IFormCollection ponemos Propietario, el enlace de datos lo hace el sistema
            Inquilino i = null;
            try
            {
                i = repositorio.ListarPorId(id);
                // En caso de ser necesario usar: 
                //
                //Convert.ToInt32(collection["CAMPO"]);
                //Convert.ToDecimal(collection["CAMPO"]);
                //Convert.ToDateTime(collection["CAMPO"]);
                //int.Parse(collection["CAMPO"]);
                //decimal.Parse(collection["CAMPO"]);
                //DateTime.Parse(collection["CAMPO"]);
                ////////////////////////////////////////
                i.Nombre = collection["Nombre"];
                i.Apellido = collection["Apellido"];
                i.Dni = collection["Dni"];
                i.Email = collection["Email"];
                i.Telefono = collection["Telefono"];
                repositorio.Modificacion(i);
                TempData["Mensaje"] = "Datos guardados correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // GET: Inquilinos/Delete/5
        public ActionResult Delete(int id)
        {
            	try
			{
                var entidad = repositorio.ListarPorId(id);
                return View(entidad);
            }
            catch (Exception )
            {//poner breakpoints para detectar errores
                throw;
            }
        }

        // POST: Inquilinos/Delete/5
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