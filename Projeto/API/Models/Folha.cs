namespace API.Models;
using System;

public class Folha
{
      public Folha()
    {
        CriadoEm = DateTime.Now;
    }
    
    public int Id {get; set;}
    public int FuncionarioId {get; set;}
    public decimal SalarioBruto {get; set;}
    public decimal Descontos {get; set;}
    public decimal SalarioLiquido {get; set;}
    public int Mes {get; set;}
    public int Ano {get; set;}
    public required Funcionario Funcionario {get; set;}
    public DateTime CriadoEm { get; set; }
}
