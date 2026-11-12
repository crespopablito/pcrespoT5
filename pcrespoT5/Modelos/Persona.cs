using SQLite;

namespace pcrespoT5.Modelos
{
    [Table("persona")]
    public class Persona
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;
    }
}
