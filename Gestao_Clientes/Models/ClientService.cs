using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao_Clientes.Models
{
    public class ClientService
    {
        #region Properties

        // CientServiceId
        [Key]
        [Display(Name = "Client Service Id")]
        public int ClientServiceId { get; set; }

        // ClientId
        [Required(ErrorMessage = "Client Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Client Id must be a positive number(int).")]
        [Column(TypeName = "int")]
        [Display(Name = "Client")]
        public int ClientId { get; set; }

        // ServiceId
        [Required(ErrorMessage = "Service Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Service Id must be a positive number(int).")]
        [Column(TypeName = "int")]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }

        #endregion

        #region Navigation

        public Client? Client { get; private set; }
        public Service? Service { get; private set; }

        #endregion

        #region Constructor
        public ClientService()
        {

        }

        public ClientService(int clientId, int serviceId)
        {
            ClientId = clientId;
            ServiceId = serviceId;
        }
        #endregion
    }
}