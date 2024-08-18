using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Clientes.Models
{
    public class FidelizationType
    {
        #region Properties

        // FidelizationTypeId
        [Key]
        [Display(Name = "Fidelization Type Id")]
        public int FidelizationTypeId { get; set; }

        // Description
        [Required(ErrorMessage = "Description is required")]
        [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        // Duration
        [Column(TypeName = "int")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 month")]
        [Display(Name = "Duration (in months)")]
        public int Duration { get; set; }

        // Discount
        [Column(TypeName = "int")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        [Display(Name = "Discount (%)")]
        public int Discount { get; set; }

        // ServicesMaximum
        [Column(TypeName = "int")]
        [Range(0, int.MaxValue, ErrorMessage = "Services Maximum must be a positive number(int)")]
        [Display(Name = "Services maximum")]
        public int ServicesMaximum { get; set; }

        #endregion

        #region Navigation
        public ICollection<Client> Client { get; set; } = new List<Client>();

        #endregion

        #region Constructor

        public FidelizationType()
        {
            Description = string.Empty;
        }

        public FidelizationType(string description, int duration, int discount = 0, int servicesMaximum = 0)
        {
            Description = description;
            Duration = duration;
            Discount = discount;
            ServicesMaximum = servicesMaximum;
        }

        #endregion
    }
}
