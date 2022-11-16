using MailSender.lib.Models.Base;
using System;
using System.ComponentModel;

namespace MailSender.lib.Models
{
    public class Recipient
    {
        private int id;
        public int Id 
        {
            get => id;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value), "Идентификатор не может быть меньше нуля");
                id = value;
            }
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
