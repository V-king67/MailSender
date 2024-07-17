using System.Collections.Generic;

namespace ConsoleTests.Data.Entities
{
    public class Group : NamedEntity
    {
        public virtual ICollection<Student> Students { get; set; }
    }

}
