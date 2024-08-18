using Gestao_Clientes.DAL;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Linq;

namespace Gestao_Clientes.Models
{
    internal sealed class SeedData
    {
        internal void SeedDataInDatabase(CA_RS11_P2_2_AlexandraMendes_DBContext context)
        {
            // Verificar e adicionar ContractType
            AddContractType(context, "Fixed monthly fee");
            AddContractType(context, "One-time payment per service");

            // Verificar e adicionar FidelizationType
            AddFidelizationType(context, "Non-Membership", 0, 0, 2);
            AddFidelizationType(context, "Basic Membership", 4, 15, 0);

            // Verificar e adicionar Services
            AddService(context, "Personal Training", 50m);
            AddService(context, "Group Fitness Classes", 15m);
            AddService(context, "Open functional training zone", 20m);
            AddService(context, "Open specialized weightlifting zone", 25m);
            AddService(context, "Nutritional Counseling", 15m);
            AddService(context, "Massage Therapy", 15m);
            AddService(context, "All services pack", 15m);
            AddService(context, "Two services pack", 20m);

            // Verificar e adicionar Clients
            AddClient(context,
                context.ContractType.Where(c => c.Description == "Fixed monthly fee").Select(c => c.ContractTypeId).FirstOrDefault(),
                context.FidelizationType.Where(ft => ft.Description == "Basic Membership" && ft.Duration == 4 && ft.Discount == 15 && ft.ServicesMaximum == 0).Select(ft => ft.FidelizationTypeId).FirstOrDefault(),
                "Madalena", "Tavares", "Rua D.Joao V", "5", "Lisboa", "1000-001", "123456789", "madalena90_soares@gmail.com", "+351919158988", new DateTime(2024, 05, 12));

            AddClient(context,
                context.ContractType.Where(c => c.Description == "Fixed monthly fee").Select(c => c.ContractTypeId).FirstOrDefault(),
                context.FidelizationType.Where(ft => ft.Description == "Basic Membership" && ft.Duration == 4 && ft.Discount == 15 && ft.ServicesMaximum == 0).Select(ft => ft.FidelizationTypeId).FirstOrDefault(),
                "Inês", "Soares", "Rua D.Manuel", "45", "Coimbra", "3000-001", "192837465", "ij_soares@gmail.com", "+351919158985", new DateTime(2024, 04, 01));


            // Verificar e adicionar ClientService
            AddClientService(context,
                context.Client.Where(c => c.Email == "madalena90_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
                context.Service.Where(s => s.Description == "All services pack").Select(s => s.ServiceId).FirstOrDefault());
            AddClientService(context,
                context.Client.Where(c => c.Email == "ij_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
                context.Service.Where(s => s.Description == "Personal Training").Select(s => s.ServiceId).FirstOrDefault());


            // Verificar e adicionar Payments
            AddPayment(context,
                context.Client.Where(c => c.Email == "ij_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
                new DateTime(2024, 4, 30), Enums.PaymentMethod.Cash, 20m);
            AddPayment(context,
                context.Client.Where(c => c.Email == "madalena90_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
                new DateTime(2024, 5, 30), Enums.PaymentMethod.BankTransfer, 17m);
            AddPayment(context,
                context.Client.Where(c => c.Email == "madalena90_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
               new DateTime(2024, 6, 30), Enums.PaymentMethod.Cash, 17m);
            AddPayment(context,
                context.Client.Where(c => c.Email == "madalena90_soares@gmail.com").Select(c => c.ClientId).FirstOrDefault(),
                new DateTime(2024, 7, 30), Enums.PaymentMethod.MobilePaymentService, 17m);
        }

        private void AddContractType(CA_RS11_P2_2_AlexandraMendes_DBContext context, string description)
        {
            if (!context.ContractType.Any(ct => ct.Description == description))
            {
                var contractType = new ContractType
                (
                    description
                );

                context.ContractType.Add(contractType);
                context.SaveChanges();
            }
        }

        private void AddFidelizationType(CA_RS11_P2_2_AlexandraMendes_DBContext context, string description, int duration, int discount, int servicesMaximum)
        {
            if (!context.FidelizationType.Any(ft => ft.Description == description && ft.Duration == duration && ft.Discount == discount && ft.ServicesMaximum == servicesMaximum))
            {
                var fidelizationType = new FidelizationType
                (
                    description,
                    duration,
                    discount,
                    servicesMaximum
                );

                context.FidelizationType.Add(fidelizationType);
                context.SaveChanges();
            }
        }

        private void AddService(CA_RS11_P2_2_AlexandraMendes_DBContext context, string description, decimal unitPrice)
        {
            if (!context.Service.Any(s => s.Description == description))
            {
                var service = new Service
                (
                    description,
                    unitPrice
                );

                context.Service.Add(service);
                context.SaveChanges();
            }
        }

        private void AddClient(CA_RS11_P2_2_AlexandraMendes_DBContext context, int contractTypeId, int fidelizationTypeId, string firstName, string lastName, string street, string doorAndFloor, string city, string postalCode, string fiscalCode, string email, string phone, DateTime registrationDate)
        {
            if (!context.Client.Any(c => c.Email == email))
            {
                var client = new Client
                (
                    contractTypeId,
                    fidelizationTypeId,
                    firstName,
                    lastName,
                    street,
                    doorAndFloor,
                    city,
                    postalCode,
                    fiscalCode,
                    email,
                    phone,
                    registrationDate
                );

                context.Client.Add(client);
                context.SaveChanges();
            }
        }

        private void AddClientService(CA_RS11_P2_2_AlexandraMendes_DBContext context, int clientId, int serviceId)
        {
            if (!context.ClientService.Any(cs => cs.ClientId == clientId && cs.ServiceId == serviceId))
            {
                var clientService = new ClientService
                (
                    clientId,
                    serviceId
                );

                context.ClientService.Add(clientService);
                context.SaveChanges();
            }
        }

        private void AddPayment(CA_RS11_P2_2_AlexandraMendes_DBContext context, int clientId, DateTime paymentDate, Enums.PaymentMethod paymentMethod, decimal value)
        {
            if (!context.Payment.Any(p => p.ClientId == clientId && p.PaymentDate == paymentDate && p.Value == value))
            {
                var payment = new Payment
                (
                    clientId,
                    paymentDate,
                    paymentMethod,
                    value
                );

                context.Payment.Add(payment);
                context.SaveChanges();
            }
        }
    }

}
