using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHR.Domain.Entities
{
    public class Vaga : EntityBase
    {
        public Vaga()
        {
            Candidaturas = new List<Candidatura>();
            Ativo = true;
        }
        /*
            "empresa": "Teste",
            "titulo": "Vaga teste",
            "descricao": "Criar os mais diferentes tipos de teste",
            "localizacao": "A",
            "nivel": 3
         */

        public string Empresa { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Localizacao { get; set; }
        public int Nivel { get; set; }
        public virtual IList<Candidatura> Candidaturas { get; set; }
    }
}

