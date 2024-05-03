using jgarcia5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace jgarcia5
{
    public class PersonRepository
    {
        String _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage  { get; set; }

        private void Init() {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string dbPath) {
            _dbPath = dbPath;
        }
        public void AddNewPerson(string name) {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Es necesario ingresar un nombre");
                Persona person = new() { Name = name };
                result = conn.Insert(person);
                StatusMessage = string.Format("Se inserto correctamente ", result, name);
             }
            catch (Exception ex )
            {
                StatusMessage = string.Format("Error no se inserto ", name, ex.Message);
            }

        }
        public List<Persona> getAllPeople(){
            try
        {
                Init();
                return conn.Table<Persona>().ToList();
        }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al listar", ex.Message);
            }
            return new List<Persona>();
    }
        public void ActualizarPersona(Persona person)
        {
            try
            {
                conn.Update(person);
                StatusMessage = string.Format(" Se actualizo correctamente");
            }
            catch (Exception ex)
            {
               StatusMessage = string.Format("No se actualizo correctamente", ex.Message);
            }
        }

        public void EliminarPersona(Persona person)
        {
            try
            {
                conn.Delete(person);
                StatusMessage = string.Format("se elimino correctamente");
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("No se elimino correctamente", ex.Message);
            }
        }
    }
}