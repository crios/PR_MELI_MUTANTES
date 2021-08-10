using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades.Modelos
{
    [Table("HistoricoDNS", Schema = "dbo")]
    public class HistoricoDNS
    {

        public HistoricoDNS()
        { }

        [Key]
        public int IdHistoricoDNS { get; set; }
        public string dns { get; set; }
        public bool mutante { get; set; }
        public DateTime? fechaRegistro { get; set; }

    }
}
