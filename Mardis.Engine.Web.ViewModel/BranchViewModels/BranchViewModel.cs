using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mardis.Engine.DataAccess.MardisCommon;

namespace Mardis.Engine.Web.ViewModel.BranchViewModels
{
    public class BranchViewModel
    {
        /*
         *              Datos del local
         */

        /// <summary>
        /// NOMBRE DE LOCAL
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name ="NOMBRE DE LOCAL")]
        public string Nombre { get; set; }

        /// <summary>
        /// ROTULO DE LOCAL
        /// </summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "ROTULO DE LOCAL")]
        public string Rotulo { get; set; }

        /// <summary>
        /// Provincias
        /// </summary>
        [Required]
        public List<Province> Provincia { get; set; }

        /// <summary>
        /// Tipo
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Canton
        /// </summary>
        public List<District> Canton { get; set; }

        public string Estado { get; set; }

        public List<Sector> Ciudad { get; set; }

        public string Parroquia { get; set; }

        public string Zona { get; set; }

        public string Sector { get; set; }

        public string Barrio { get; set; }

        public double Longitud{ get; set; }

        public double Latitud { get; set; }

        public string Principal { get; set; }

        public string Secundaria { get; set; }

        public string Numero { get; set; }

        public string Referencia { get; set; }

        /*
         *          Propietario del Local
         */

        public string Personeria { get; set; }

        public string Cedula { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        /*
         *              Informacion der Administrador
         */

        /// <summary>
        /// El administrador del local como propietario del negocio
        /// </summary>
        public bool Propietario { get; set; }

        /// <summary>
        /// Primer nombre del propietario
        /// </summary>
        public string PrimerNombrePropietario { get; set; }

        /// <summary>
        /// Segundo nombre del Propietario
        /// </summary>
        public string SegundoNombrePropietario { get; set; }

        public string PrimerApellidoPropietario { get; set; }

        public string SegundoApellidoPropietario { get; set; }

        public string CedulaPropietario { get; set; }

        public string TelefonoPropietario { get; set; }

        public string CelularPropietario { get; set; }

        /*
         *                  Informacion de Cuenta
         * 
         */


        public string Cuenta { get; set; }

        public string Canal { get; set; }

        public string TipoNegocio { get; set; }

        public string Cuenta2 { get; set; }

        public string Canal2 { get; set; }

        public string TipoNegocio2 { get; set; }
    }
}
