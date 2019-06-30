using SQLite;

namespace Eat2LoseWeight.DataAccess.Entities
{
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
