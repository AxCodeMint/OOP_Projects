using System;
using Microsoft.EntityFrameworkCore;
using Gestao_Clientes.Models;
using static Gestao_Clientes.Models.Enums;
using System.IO;
using Microsoft.AspNetCore.Components.Forms;

namespace Gestao_Clientes.DAL
{
    public partial class CA_RS11_P2_2_AlexandraMendes_DBContext : DbContext
    {
        public CA_RS11_P2_2_AlexandraMendes_DBContext()
        {
        }

        public CA_RS11_P2_2_AlexandraMendes_DBContext(DbContextOptions<CA_RS11_P2_2_AlexandraMendes_DBContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ClientService> ClientService { get; set; }
        public DbSet<ContractType> ContractType { get; set; }
        public DbSet<FidelizationType> FidelizationType { get; set; }
        public virtual DbSet<Payment> Payment { get; set; } = null!;
    }
}
