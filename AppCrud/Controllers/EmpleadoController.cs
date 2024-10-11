using Microsoft.AspNetCore.Mvc;
using AppCrud.Data;
using AppCrud.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;

namespace AppCrud.Controllers
{
    //cada vez que se crea una accion del crud en el controlador se agrega una vista que presente 
    //la interfaz correspondiente de esta acción
    public class EmpleadoController : Controller
    { 
        //se llama a la base de datos en el nuevo controlador donde tambien se harán las operaciones crud
        //esta linea de codigo tambien permite acceder a la informacion de la bases de datos 
        private readonly AppDBContext _appDbContext;

        //HTTP GET: Redirige al usuario al formulario donde puede ingresar la información del nuevo registro(como un empleado).

        //HTTP POST: Procesa la información enviada desde ese formulario, guarda los datos en la base de datos y redirige al usuario a la lista actualizada de registros.

        public EmpleadoController(AppDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task <IActionResult> Lista()
        {
            //lista para ver los empleados que se crearon
            List<Empleado> lista = await _appDbContext.Empleados.ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public  IActionResult Nuevo() //este metodo lo redirige al formulario para poder crear el nuevo formulario 
        {         
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Nuevo(Empleado empleado)
        {

            await _appDbContext.Empleados.AddAsync(empleado);
            await _appDbContext.SaveChangesAsync(); 
            return RedirectToAction(nameof (Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id) 
        {
            Empleado empleado = await _appDbContext.Empleados.FirstAsync(e =>e.IdEmpleado == id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Editar (Empleado empleado)
        {

             _appDbContext.Empleados.Update(empleado);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            Empleado empleado = await _appDbContext.Empleados.FirstAsync(e => e.IdEmpleado == id);
            _appDbContext.Empleados.Remove(empleado);
            await _appDbContext.SaveChangesAsync(); 
            return RedirectToAction(nameof(Lista)); 
        }
    }
}
