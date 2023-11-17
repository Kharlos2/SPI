using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGP.Models;

namespace SGP.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly SgpiContext context;

        public EstudiantesController(SgpiContext spiContext)
        {   
            context = spiContext;
        }

        // GET: Estudiantes
        public async Task<IActionResult> Index()
        {
            var sgpiContext = context.Usuarios.Include(u => u.IdDocNavigation).Include(u => u.IdGeneroNavigation).Include(u => u.IdProgramaNavigation).Include(u => u.IdRolNavigation).Where(u => u.IdRol == 2);
            return View(await sgpiContext.ToListAsync());
        }

        // GET: Estudiantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await context.Usuarios
                .Include(u => u.IdDocNavigation)
                .Include(u => u.IdGeneroNavigation)
                .Include(u => u.IdProgramaNavigation)
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }
        public async Task<ActionResult> UploadImageAsync(int id)
        {
            var pago = new Pago
            {
                IdUsuario = id // Seteas el IdUsuario basado en el 'id' recibido como parámetro
                               // Otras asignaciones de valores iniciales si es necesario
            };
            return View(pago);
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file,int id,Pago pagos)
        {

            if (file != null && file.Length > 0)
            {

                // Guardar la imagen en una carpeta local
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                // Guardar la URL de la imagen en la base de datos
                var imageUrl = "/Images/" + fileName;
                // Aquí iría la lógica para guardar 'imageUrl' en la base de datos
                pagos.IdUsuario = id;
                pagos.urlImage = imageUrl;
                pagos.Fecha = DateTime.Now;

                using (var db = new SgpiContext()) // Reemplaza YourDbContext con tu contexto de base de datos
                {
                    db.Pagos.Add(pagos); // Agregas el objeto pago al contexto de base de datos
                    db.SaveChanges(); // Guardas los cambios en la base de datos
                }

                // Puedes retornar una vista, redireccionar, o hacer cualquier otra acción necesaria
                return RedirectToAction("Index", "Estudiantes");
            }
            else
            {
                // Manejar caso en el que no se cargó ninguna imagen
                return View();
            }
        }

        // GET: Estudiantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdDoc"] = new SelectList(context.TipoDocumentos, "IdDoc", "IdDoc", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(context.Generos, "IdGenero", "IdGenero", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(context.Programas, "IdPrograma", "IdPrograma", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(context.Rols, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // POST: Estudiantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,PrimerApellido,SegundoApellido,Email,Password,NumeroDoc,Activo,IdDoc,IdGenero,IdPrograma,IdRol")] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(usuario);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoc"] = new SelectList(context.TipoDocumentos, "IdDoc", "IdDoc", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(context.Generos, "IdGenero", "IdGenero", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(context.Programas, "IdPrograma", "IdPrograma", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(context.Rols, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        private bool UsuarioExists(int id)
        {
          return (context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
