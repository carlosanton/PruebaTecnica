using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Introduzca un nombre")]
        [MinLength(3)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Introduzca los apellidos")]
        [MinLength(3)]
        public string Apellidos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Por favor, seleccione un item de la lista")]

        public int IdSexo { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Introduzca la fecha de nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Introduzca la dirección")]
        [MinLength(10)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "Introduzca el país")]
        public string Pais { get; set; }

        [Display(Name = "Código postal")]
        [Required(ErrorMessage = "Introduzca el código postal")]
        public string CodigoPostal { get; set; }

        [ForeignKey("IdSexo")]
        public virtual Sexo Sexo { get; set; }
    }
}
