using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appweb1.LogicaNegocio;
using appweb1.Modelos;
using appweb1.ViewModels;
using Microsoft.AspNetCore.Http;

namespace appweb1.Controllers
{
    public class ControladorUsuario : Controller
    {
        private readonly LogicaUsuario _logicaUsuario;

        public ControladorUsuario(LogicaUsuario logicaUsuario)
        {
            _logicaUsuario = logicaUsuario;
        }

        // GET: /ControladorUsuario/Registrar
        public IActionResult Registrar()
        {
            return View(new RegistrarUsuarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(RegistrarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var usuario = new Usuario
                {
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Correo = model.Correo,
                    Contrasena = model.Contrasena, // En producción, deberías hashear la contraseña
                    Rol = model.Rol,
                    Estado = model.Estado
                };

                await _logicaUsuario.RegistrarUsuario(usuario);
                return RedirectToAction("Index", "Home");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // GET: /ControladorUsuario/Login
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var usuario = await _logicaUsuario.IniciarSesion(model.Correo, model.Contrasena);
                if (usuario == null)
                {
                    ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
                    return View(model);
                }

                // Guardar el ID del usuario en la sesión
                HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);
                HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);

                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        // GET: /ControladorUsuario/Perfil
        public async Task<IActionResult> Perfil()
        {
            var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
            if (!usuarioId.HasValue)
            {
                return RedirectToAction("Login");
            }

            var usuario = await _logicaUsuario.ObtenerUsuarioPorId(usuarioId.Value);
            if (usuario == null)
            {
                return RedirectToAction("Login");
            }

            var model = new RegistrarUsuarioViewModel
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Correo = usuario.Correo,
                Contrasena = usuario.Contrasena,
                Rol = usuario.Rol,
                Estado = usuario.Estado
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActualizarPerfil(RegistrarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Perfil", model);
            }

            try
            {
                var usuarioId = HttpContext.Session.GetInt32("UsuarioId");
                if (!usuarioId.HasValue)
                {
                    return RedirectToAction("Login");
                }

                var usuario = new Usuario
                {
                    IdUsuario = usuarioId.Value,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Correo = model.Correo,
                    Contrasena = model.Contrasena,
                    Rol = model.Rol,
                    Estado = model.Estado
                };

                await _logicaUsuario.ActualizarPerfil(usuario);
                return RedirectToAction("Perfil");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Perfil", model);
            }
        }

        // GET: /ControladorUsuario/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}