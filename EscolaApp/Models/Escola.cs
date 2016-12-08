using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscolaApp.Models
{
    public class Escola
    {
        public int Id { get; set; }
        public string Nome{ get; set; }
        public string UF { get; set; }
        public double CN { get; set; }
        public double CH { get; set; }
        public double LC { get; set; }
        public double Matematica{ get; set; }
        public double Redacao { get; set; }
        public double Media { get; set; }
        public override string ToString()
        {
            return $"{Id} - {Nome} - {UF} - Ciências da Natureza: {CN} - Ciências Humanas: {CH} - Linguagens: {LC} - Matemática: {Matematica} - Redação: {Redacao} - Media Geral: {Media}";
        }
    }
}
