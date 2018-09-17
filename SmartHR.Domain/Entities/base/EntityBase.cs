using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Domain.Entities
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public DateTime? DataModificacao { get; set; }
        public bool Ativo { get; set; }
    }
}