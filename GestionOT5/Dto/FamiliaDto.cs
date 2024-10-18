using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionOT5.Dto
{
    public class FamiliaDto
    {
        public int Id { get; set; }
        public string? TipoParte { get; set; }
        public string? CodigoFamilia { get; set; }
        public string? CodigoSubfamlia { get; set; }
        public string? CodigoArticulo { get; set; }
        public string? Descripcion { get; set; }
        public bool Borrado { get; set; }
    }
}
