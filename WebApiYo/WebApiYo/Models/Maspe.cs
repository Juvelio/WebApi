using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiYo.Models
{
    public class Maspe
    {
        public int MASPE_CARNE { get; set; }
        public string MASPE_DNI { get; set; }
        public string TGRAD_DES { get; set; }
        public string MASPE_PATER { get; set; }
        public string MASPE_MATER { get; set; }
        public string MASPE_NOMB { get; set; }


        public int Fila { get; set; }
        public string MASPE_FASCEN { get; set; }
        public string TSITUA_DESL { get; set; }
        public int Total { get; set; }
        public int Filtro { get; set; }
    }
}
