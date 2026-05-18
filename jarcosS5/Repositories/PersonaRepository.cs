using jarcosS5.Models;
using SQLite;

namespace jarcosS5.Repositories
{
    public class PersonaRepository
    {
        string _dbPath; //Ruta
        private SQLiteConnection _conn;

        public string Status { get; set; }
        private void Init()
        {
            if (_conn != null)
                return;
            _conn = new(_dbPath);
            _conn.CreateTable<Persona>();
        }

        public PersonaRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public void AddPerson(string name)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("El nombre es requerido");

                Persona person = new() { Name = name };
                result = _conn.Insert(person);
                Status = string.Format("Dato Ingresado: {0} - {1}", result, name);
            }
            catch (Exception ex)
            {
                Status = string.Format("Error: " + ex.Message);
                throw;
            }
        }

        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                Status = string.Format("Error", ex.Message);
                throw;
            }
            return new List<Persona>();
        }
        public void Update(Persona person)
        {
            using var connection = new SQLiteConnection(_dbPath);
            connection.Update(person);
        }

        public void Delete(Persona person)
        {
            using var connection = new SQLiteConnection(_dbPath);
            connection.Delete(person);
        }
    }
}
