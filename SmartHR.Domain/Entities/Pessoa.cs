using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Domain.Entities
{
    public class Pessoa : EntityBase
    {
        /*
            "nome": "John Doe",
            "profissao": "Engenheiro de Software",
            "localizacao": "C",
            "nivel": 2
         */

        public Pessoa()
        {
            Candidaturas = new List<Candidatura>();
            Ativo = true;
        }

        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Localizacao { get; set; }
        public int Nivel { get; set; }

        public virtual IList<Candidatura> Candidaturas { get; set; }
    }
}