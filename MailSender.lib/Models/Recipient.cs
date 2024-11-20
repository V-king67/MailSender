using MailSender.lib.Models.Base;
using System;
using System.ComponentModel;

namespace MailSender.lib.Models
{
    public class Recipient : Person
    {
        public new int Id
        {
            get => base.Id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Идентификатор не может быть меньше нуля");
                base.Id = value;
            }
        }

        public override string Name
        {
            get => base.Name;
            set
            {
                if (value == "QWE")
                    throw new ArgumentException("Запрещено вводить QWE", nameof(value));

                base.Name = value;
            }
        }

        public string Description { get; set; }
    }
}
