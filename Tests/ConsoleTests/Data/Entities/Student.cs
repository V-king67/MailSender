using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTests.Data.Entities
{
    public class Student : NamedEntity
    {
        //[Key]
        //public int PrimaryKey { get; set; }
        [Required, MaxLength(120)]
        public string Surname {  get; set; }

        [MaxLength(120)]
        public string Patronymic {  get; set; }

        public virtual Group Group { get; set; } //При добавлении virtual становится навигационным свойством,
                                                 //т.е. Entity Framework переопределит getter свойства для формирования запроса к другой таблице в бд
    }
}
