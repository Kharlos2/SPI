using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGP.Models;

namespace SGP.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly SgpiContext _context;

        public UsuariosController(SgpiContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var sgpiContext = _context.Usuarios.Include(u => u.IdDocNavigation).Include(u => u.IdGeneroNavigation).Include(u => u.IdProgramaNavigation).Include(u => u.IdRolNavigation);
            return View(await sgpiContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
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

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["IdDoc"] = new SelectList(_context.TipoDocumentos, "IdDoc", "IdDoc");
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "IdGenero");
            ViewData["IdPrograma"] = new SelectList(_context.Programas, "IdPrograma", "IdPrograma");
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,PrimerApellido,SegundoApellido,Email,Password,NumeroDoc,Activo,IdDoc,IdGenero,IdPrograma,IdRol")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoc"] = new SelectList(_context.TipoDocumentos, "IdDoc", "IdDoc", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "IdGenero", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(_context.Programas, "IdPrograma", "IdPrograma", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdDoc"] = new SelectList(_context.TipoDocumentos, "IdDoc", "IdDoc", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "IdGenero", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(_context.Programas, "IdPrograma", "IdPrograma", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
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
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
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
            ViewData["IdDoc"] = new SelectList(_context.TipoDocumentos, "IdDoc", "IdDoc", usuario.IdDoc);
            ViewData["IdGenero"] = new SelectList(_context.Generos, "IdGenero", "IdGenero", usuario.IdGenero);
            ViewData["IdPrograma"] = new SelectList(_context.Programas, "IdPrograma", "IdPrograma", usuario.IdPrograma);
            ViewData["IdRol"] = new SelectList(_context.Rols, "IdRol", "IdRol", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
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
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'SgpiContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
