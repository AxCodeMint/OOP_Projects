using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Forms;

namespace Gestao_Clientes.Models
{
    public class ContractType
    {
        #region Properties

        // ContractTypeId
        [Key]
        [Display(Name = "Contract type Id")]
        public int ContractTypeId { get; set; }

        // Description
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        #endregion

        #region Navigation
        public ICollection<Client> Client { get; set; } = new List<Client>();

        #endregion

        #region Constructor
        public ContractType()
        {
            Description = string.Empty;
        }

        public ContractType(string description)
        {
            Description = description;
        }

        #endregion
    }
}
