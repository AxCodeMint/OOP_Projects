using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Clientes.Models
{
    public class Service
    {
        #region Properties

        // ServiceId
        [Key]
        [Display(Name = "Service id")]
        public int ServiceId { get; set; }

        // Description
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        
       [Required(ErrorMessage = "Unit Price is required")]
       [Column(TypeName = "decimal(8, 2)")]
       [Range(0.01, 1000000000, ErrorMessage = "Unit Price must be a positive value")]
       [Display(Name = "Unit price")]
        public decimal UnitPrice { get; set; }

        #endregion

        #region Navigation

        public ICollection<ClientService> ClientService { get; set; } = new List<ClientService>();

        #endregion

        #region Constructor

        public Service()
        {

        }

        public Service(string description, decimal unitPrice)
        {
            Description = description;
            UnitPrice = unitPrice;
        }

        #endregion
    }
}
