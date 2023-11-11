using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SGP.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Build.Framework;

namespace SGP.Controllers
{
    public class AdministradorController : Controller
    {
        private SgpiContext context;

        SgpiContext db = new SgpiContext();

        public AdministradorController(SgpiContext spiContext)
        {
            context = spiContext;
        }


        public IActionResult CrearUsuario()
        {
            ViewBag.TipoDocumento = context.TipoDocumentos.ToList();
            ViewBag.Genero = context.Generos.ToList();
            ViewBag.Programa = context.Programas.ToList();
            ViewBag.Rol = context.Rols.ToList();

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult GuardarUsuario(Usuario usuario)
        {
            context.Add(usuario);
            context.SaveChanges();
            ViewBag.TipoDocumento = context.TipoDocumentos.ToList();
            ViewBag.Genero = context.Generos.ToList();
            ViewBag.Programa = context.Programas.ToList();
            ViewBag.Rol = context.Rols.ToList();
            return View(usuario);
        }
        public async Task<IActionResult> Index(String buscar)
        {  
            var usuarios = from usuario in context.Usuarios select usuario;

            if (!String.IsNullOrEmpty(buscar))
            {
                usuarios = usuarios.Where(s => s.Nombre!.Contains(buscar));
            }

            var sgpiContext = context.Usuarios.Include(u => u.IdDocNavigation).Include(u => u.IdGeneroNavigation)
                .Include(u => u.IdProgramaNavigation).Include(u => u.IdRolNavigation);
            return View(await usuarios.ToListAsync());
        }

        //Borrar
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (context.Usuarios == null)
            {
                return Problem("Entity set 'SgpiContext.Usuarios'  is null.");
            }
            var usuario = await context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                context.Usuarios.Remove(usuario);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //get editar
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
            ViewData["IdDoc"] = new SelectList(context.TipoDocumentos, "IdDoc", "Descripcion", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(context.Generos, "IdGenero", "Descripcion", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(context.Programas, "IdPrograma", "Descripcion", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(context.Rols, "IdRol", "Descripcion", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
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
            ViewData["IdDoc"] = new SelectList(context.TipoDocumentos, "IdDoc", "Descripcion", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(context.Generos, "IdGenero", "Descripcion", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(context.Programas, "IdPrograma", "Descripcion", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(context.Rols, "IdRol", "Descripcion", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Details/5
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
        private bool UsuarioExists(int id)
        {
            return (context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }


        public IActionResult BuscarUsuario()
        {
            Usuario us = new Usuario();
            ViewBag.Documento = context.TipoDocumentos.ToList();
            return View(us);
        }

        [HttpPost]
        public IActionResult BuscarUsuario(Usuario usuario)
        {
            var us = context.Usuarios
                .Where(u => u.NumeroDoc == usuario.NumeroDoc
                && u.IdDoc == usuario.IdDoc).FirstOrDefault();

            if (us != null)
            {

                ViewBag.Genero = context.Pagos.ToList();
                ViewBag.Documento = context.TipoDocumentos.ToList();
                ViewBag.Rol = context.Rols.ToList();
                ViewBag.Programa = context.Programas.ToList();
                return View(us);
            }

            else
            {
                ViewBag.documentos = context.TipoDocumentos.ToList();
                return View();
            }
        }
    }
}
