using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Gestao_Clientes.Models.Enums;

namespace Gestao_Clientes.Models
{
    public class Payment
    {
        #region Properties
        // PaymentId
        [Key]
        [Display(Name = "Payment id")]
        public int PaymentId { get; set; }

        // ClientId
        [Required(ErrorMessage = "Client Id is required")]
        [Column(TypeName = "int")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        // PaymentDate
        [Required(ErrorMessage = "Payment Date is required")]
        [Display(Name = "Payment date")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        // PaymentMethod
        [Required(ErrorMessage = "Payment Method is required")]
        [Column(TypeName = "int")]
        [Display(Name = "Payment method")]
        public PaymentMethod PaymentMethod { get; set; }

        // Value
        [Required(ErrorMessage = "Value is required")]
        [Column(TypeName = "decimal(8, 2)")]
        [Range(0.01, 1000000000, ErrorMessage = "Value must be a positive value")]
        [Display(Name = "Value")]
        public decimal Value { get; set; }

        #endregion

        #region Navigation
        // Navigation
        public Client? Client { get; private set ; }

        #endregion

        #region Construtor

        public Payment()
        {

        }

        public Payment(int clientId, DateTime paymentDate, PaymentMethod paymentMethod, decimal value)
        {
            ClientId = clientId;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            Value = value;
        }

        #endregion
    }
}
