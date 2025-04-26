using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using appweb1.Data;
using appweb1.Modelos;
using Microsoft.EntityFrameworkCore;

namespace appweb1.LogicaNegocio
{
    public class LogicaUsuario
    {
        private readonly AppDbContext _context;

        public LogicaUsuario(AppDbContext context)
        {
            _context = context;
        }

        public async Task RegistrarUsuario(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Correo == usuario.Correo))
            {
                throw new InvalidOperationException("El correo ya está registrado.");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> IniciarSesion(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                throw new ArgumentException("El correo y la contraseña son obligatorios.");
            }

            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contrasena == contrasena);
        }

        public async Task<Usuario> ObtenerUsuarioPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task ActualizarPerfil(Usuario usuarioActualizado)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == usuarioActualizado.IdUsuario);

            if (usuario == null)
            {
                throw new InvalidOperationException("El usuario no existe.");
            }

            if (await _context.Usuarios.AnyAsync(u => u.Correo == usuarioActualizado.Correo && u.IdUsuario != usuarioActualizado.IdUsuario))
            {
                throw new InvalidOperationException("El correo ya está registrado por otro usuario.");
            }

            usuario.Nombre = usuarioActualizado.Nombre;
            usuario.Apellido = usuarioActualizado.Apellido;
            usuario.Correo = usuarioActualizado.Correo;
            usuario.Contrasena = usuarioActualizado.Contrasena;
            usuario.Rol = usuarioActualizado.Rol;
            usuario.Estado = usuarioActualizado.Estado;

            await _context.SaveChangesAsync();
        }
    }
}