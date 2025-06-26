using System;
using System.Globalization;

namespace Questao1
{
    class ContaBancaria
    {
        public int Numero { get; set; }
        public string Titular { get; set; }
        public double Saldo { get; set; }

        public ContaBancaria(int numero, string titular)
        {
            Numero = numero;
            Titular = titular;
        }
        
        public ContaBancaria(int numero, string titular, double depositoinicial)
        {
            Numero = numero;
            Titular = titular;
            Saldo = depositoinicial;
        }

        public void Deposito(double deposito)
        {
            Saldo += deposito;
        }
        
        public void Saque(double saque)
        {
            Saldo -= 3.50;
            Saldo -= saque;
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {Titular}, Saldo: $ {Saldo.ToString("0.00", CultureInfo.InvariantCulture)}";
        }
    }
}
