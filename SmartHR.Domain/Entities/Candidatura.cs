using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using SmartHR.Domain.ViewModels;

namespace SmartHR.Domain.Entities
{
    public class Candidatura
    {
        /*
            "id_vaga": 1,
            "id_pessoa": 2
         */

         public Candidatura()
         {
             
         }

        public Candidatura(CandidaturaVM model)
        {
            VagaID = model.id_vaga;
            PessoaID = model.id_pessoa;
        }

        public int VagaID { get; set; }
        public int PessoaID { get; set; }

        public int Score { get; set; }

        public virtual Vaga Vaga { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public DateTime DataCriacao { get; set; }
    }
}

