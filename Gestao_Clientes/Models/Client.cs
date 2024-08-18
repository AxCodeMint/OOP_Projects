using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Clientes.Models
{
    public class Client
    {
        #region Properties

        // ClientId
        [Key]
        [Display(Name = "Client Id")]
        public int ClientId { get; set; }

        // ContractTypeId
        [Required(ErrorMessage = "Contract Type Id is required")]
        [Column(TypeName = "int")]
        [Display(Name = "Contract type")]
        public int ContractTypeId { get; set; }

        // FidelizationTypeId
        [Required(ErrorMessage = "Fidelization Type Id is required")]
        [Column(TypeName = "int")]
        [Display(Name = "Fidelization type")]
        public int FidelizationTypeId { get; set; }

        // FirstName
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 30 characters")]
        [Column(TypeName = "nvarchar(40)")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        // LastName
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(40, ErrorMessage = "Cannot be longer than 30 characters")]
        [Column(TypeName = "nvarchar(40)")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        // FullName
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        // Street
        [Required(ErrorMessage = "Street is required")]
        [StringLength(50, ErrorMessage = "Cannot be longer than 50 characters")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Street")]
        public string Street { get; set; }

        // DoorAndFloor
        [Required(ErrorMessage = "Door and/or Floor is required")]
        [StringLength(15, ErrorMessage = "Cannot be longer than 15 characters")]
        [Column(TypeName = "nvarchar(15)")]
        [Display(Name = "Door and floor")]
        public string DoorAndFloor { get; set; }

        // City
        [Required(ErrorMessage = "City is required")]
        [StringLength(20, ErrorMessage = "Cannot be longer than 20 characters")]
        [Column(TypeName = "nvarchar(20)")]
        [Display(Name = "City")]
        public string City { get; set; }

        // PostalCode
        [Required(ErrorMessage = "Postal code is required")]
        [StringLength(8, ErrorMessage = "Cannot be longer than 8 characters")]
        [RegularExpression(@"^\d{4}-\d{3}$", ErrorMessage = "Postal code must be in the format XXXX-XXX")]
        [Column(TypeName = "nvarchar(8)")]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        // FiscalCode
        [Required(ErrorMessage = "Fiscal code is required")]
        [StringLength(9, ErrorMessage = "Cannot be longer than 9 characters")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Fiscal code must be 9 digits long")]
        [Column(TypeName = "nvarchar(9)")]
        [Display(Name = "Fiscal code")]
        public string FiscalCode { get; set; }

        // Email
        [Required(ErrorMessage = "Email is required")]
        [StringLength(256, ErrorMessage = "Cannot be longer than 50 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Column(TypeName = "nvarchar(256)")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // Phone
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(13, ErrorMessage = "Cannot be longer than 13 characters")]
        [RegularExpression(@"^(\+351)?\d{9}$", ErrorMessage = "Phone number must be 9 digits long, optionally starting with +351")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [Column(TypeName = "nvarchar(13)")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        // RegistrationDate
        [DataType(DataType.Date)]
        [Display(Name = "Registration date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        #endregion

        #region Navigation
        public ContractType? ContractType { get; private set; } 
        public FidelizationType? FidelizationType { get; private set; }
        public ICollection<Payment> Payment { get; } = new List<Payment>();
        public ICollection<ClientService> ClientService { get; }  = new List<ClientService>();

        #endregion

        #region Constructor

        public Client()
        {
            
        }

        public Client(int contractTypeId, int fidelizationTypeId, string firstName, string lastName, string street, string doorAndFloor, string city, string postalCode, string fiscalCode, string email, string phone , DateTime registrationDate)
        {
            ContractTypeId = contractTypeId;
            FidelizationTypeId = fidelizationTypeId;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            DoorAndFloor = doorAndFloor;
            City = city;
            PostalCode = postalCode;
            FiscalCode = fiscalCode;
            Email = email;
            Phone = phone;
            RegistrationDate = DateTime.Now;
        }

        #endregion
    }
}
