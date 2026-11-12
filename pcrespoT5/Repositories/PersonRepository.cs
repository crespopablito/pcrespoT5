using pcrespoT5.Modelos;
using SQLite;

namespace pcrespoT5.Repositories
{
    public class PersonRepository
    {
        private readonly string _dbPath;
        private SQLiteConnection? _conn;

        public string StatusMessage { get; set; } = string.Empty;

        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (_conn is not null)
                return;

            _conn = new SQLiteConnection(_dbPath);
            _conn.CreateTable<Persona>();
        }

        // INSERTAR
        public void AddNewPerson(string name)
        {
            try
            {
                Init();

                if (string.IsNullOrWhiteSpace(name))
                    throw new Exception("Nombre es requerido");

                Persona persona = new() { Name = name.Trim() };
                _conn!.Insert(persona);
                StatusMessage = "Dato ingresado";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }
        }

        // SELECCIONAR
        public List<Persona> GetAllPerson()
        {
            try
            {
                Init();
                return _conn!.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = "Error: " + ex.Message;
            }

            return new List<Persona>();
        }

        // ELIMINAR
        public void DeletePerson(int id)
        {
            try
            {
                Init();

                int result = _conn!.Delete<Persona>(id);

                StatusMessage = result > 0
                    ? "Persona eliminada correctamente"
                    : "No se encontró la persona";
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al eliminar: " + ex.Message;
            }
        }

        // ACTUALIZAR
        public bool UpdatePerson(int id, string nuevoNombre)
        {
            try
            {
                Init();

                if (string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    StatusMessage = "El nombre es obligatorio";
                    return false;
                }

                nuevoNombre = nuevoNombre.Trim();

                if (nuevoNombre.Length > 25)
                {
                    StatusMessage = "El nombre no puede superar los 25 caracteres";
                    return false;
                }

                Persona? persona = _conn!.Find<Persona>(id);

                if (persona == null)
                {
                    StatusMessage = "Persona no encontrada";
                    return false;
                }

                persona.Name = nuevoNombre;

                int resultado = _conn.Update(persona);

                StatusMessage = resultado > 0
                    ? "Persona actualizada correctamente"
                    : "No se pudo actualizar la persona";

                return resultado > 0;
            }
            catch (Exception ex)
            {
                StatusMessage = "Error al actualizar: " + ex.Message;
                return false;
            }
        }
    }
}
