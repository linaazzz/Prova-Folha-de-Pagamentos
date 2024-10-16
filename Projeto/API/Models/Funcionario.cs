namespace API.Models;
using System;

    public class Funcionario
    {
        public Funcionario()
    {
        CriadoEm = DateTime.Now;
    }
        public int Id {get; set;}
        public string? Nome {get; set;}
        public string? CPF {get; set;}
        public decimal SalarioBase {get; set;}
        public DateTime CriadoEm { get; set; }
    }
