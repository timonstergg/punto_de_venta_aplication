using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

using Venta.Application.Interfaces;
using Venta.Domain.Entities;
using Venta.Domain.Interfaces;

namespace Venta.Application.Services
{
    public class PersonaService : IPersonaService
    {
        private readonly IRepository<Persona> _repository;

        public PersonaService(IRepository<Persona> repository)
        {
            _repository = repository;
        }

        public async Task AgregarPersona(string documento, string nombre)
        {
            var persona = new Persona
            {
                Documento = documento, // Suponiendo que `Documento` es string y lo quieres desde el ID
                Nombre = nombre
            };

            await _repository.Add(persona);
            await _repository.Save();
        }

        public async Task<IEnumerable<Persona>> MostrarPersonas()
        {
            var personas = await _repository.GetAll();

            Console.WriteLine("Lista de personas:");
            foreach (var persona in personas)
            {
                Console.WriteLine($"Documento: {persona.Documento}, Nombre: {persona.Nombre}");
            }

            return personas;
        }

        public async Task ObtenerPersonaPorId(int id)
        {
            try
            {
                var persona = await _repository.GetById(id);
                if (persona != null)
                {
                    Console.WriteLine($"Persona encontrada: {persona.Nombre}");
                }
                else
                {
                    Console.WriteLine("Persona no encontrada.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task EliminarPersona(int id)
        {
            try
            {
                var persona = await _repository.GetById(id);

                if (persona == null)
                {
                    Console.WriteLine("La persona no fue encontrada.");
                    return;
                }

                await _repository.Delete(persona);
                await _repository.Save();

                Console.WriteLine($"Persona con ID {id} eliminada.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
