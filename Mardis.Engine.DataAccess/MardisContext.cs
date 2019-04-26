using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Mardis.Engine.DataAccess.MardisCommon;
using Mardis.Engine.DataAccess.MardisCore;
using Mardis.Engine.DataAccess.MardisSecurity;
using Mardis.Engine.DataAccess.MardisPedidos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Mardis.Engine.DataAccess
{
    public class MardisContext : DbContext
    {

        private readonly string _connectionString;
        protected SqlConnection Connection;
        protected SqlConnection connection => Connection ?? (Connection = GetOpenConnection());

        public MardisContext(DbContextOptions<MardisContext> options)
            : base(options)
        {
            _connectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;
            //extension.ConnectionString = connectionString;
        }

        public SqlConnection GetOpenConnection(bool mars = false)
        {
            var cs = _connectionString;
            if (mars)
            {
                var scsb = new SqlConnectionStringBuilder(cs)
                {
                    MultipleActiveResultSets = true
                };
                cs = scsb.ConnectionString;
            }
            var connection = new SqlConnection(cs);
            connection.Open();
            return connection;
        }

        public SqlConnection GetClosedConnection()
        {
            var conn = new SqlConnection(_connectionString);
            if (conn.State != ConnectionState.Closed) throw new InvalidOperationException("should be closed!");
            return conn;
        }

        /// <summary>
        /// Tabla de Usuario
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Tabla de tipos de Usuario
        /// </summary>
        public DbSet<TypeUser> TypeUsers { get; set; }

        /// <summary>
        /// Tabla de Cuentas
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Tabla de Profile
        /// </summary>
        public DbSet<Profile> Profiles { get; set; }

        /// <summary>
        /// Tabla de AuthorizationProfile
        /// </summary>
        public DbSet<AuthorizationProfile> AuthorizationProfiles { get; set; }

        /// <summary>
        /// Tabla de Menu
        /// </summary>
        public DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// Tabla de Locales
        /// </summary>
        public DbSet<Branch> Branches { get; set; }

        /// <summary>
        /// Tabla de Clientes por local
        /// </summary>
        public DbSet<BranchCustomer> BranchCustomers { get; set; }

        /// <summary>
        /// Tabla de filtro de Controles
        /// </summary>
        public DbSet<FilterController> FilterControllers { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<FilterTable> FilterTables { get; set; }

        /// <summary>
        /// Tabla de Campos de los Controles
        /// </summary>
        public DbSet<FilterField> FilterFields { get; set; }

        /// <summary>
        /// Criterios de los Filtros
        /// </summary>
        public DbSet<FilterCriteria> FilterCriterias { get; set; }

        /// <summary>
        /// Tipos de Filtros
        /// </summary>
        public DbSet<TypeFilter> TypeFilters { get; set; }

        /// <summary>
        /// Tabla de Filtros de ejecución
        /// </summary>
        public DbSet<FilterExecution> FilterExecutions { get; set; }

        /// <summary>
        /// tabla de detalle de filtros de ejecución
        /// </summary>
        public DbSet<FilterExecutionDetail> FilterExecutionDetails { get; set; }

        /// <summary>
        /// Tabla de Paises
        /// </summary>
        public DbSet<Country> Countries { get; set; }
        /// <summary>
        /// Tabla de UserCanpaign
        /// </summary>
        public DbSet<UserCanpaign> UserCanpaign { get; set; }
        /// <summary>
        /// Tabla de Provincias
        /// </summary>
        public DbSet<Province> Provinces { get; set; }


        /// <summary>
        /// Tabla de Distritos 
        /// </summary>
        public DbSet<District> Districts { get; set; }

        /// <summary>
        /// Tabla de Sectores
        /// </summary>
        public DbSet<Sector> Sectors { get; set; }


        /// <summary>
        /// Tabla de Parroquias
        /// </summary>
        public DbSet<Parish> Parishes { get; set; }

        /// <summary>
        /// Tabla de Personas
        /// </summary>
        public DbSet<Person> Persons { get; set; }
        /// <summary>
        /// Tabla de CodigoReservado
        /// </summary>
        public DbSet<CodigoReservados> CodigoReservados { get; set; }
        /// <summary>
        /// Tabla de Mapas por cuentas
        /// </summary>
        public DbSet<Map> Map { get; set; }


        /// <summary>
        /// Tabla Tipos de Personas
        /// </summary>
        public DbSet<TypePerson> TypesPerson { get; set; }

        /// <summary>
        /// Tabla de Estados de Clientes
        /// </summary>
        public DbSet<StatusCustomer> StatusCustomers { get; set; }

        /// <summary>
        /// Tipo de Clientes
        /// </summary>
        public DbSet<TypeCustomer> TypesCustomers { get; set; }

        /// <summary>
        /// Tabla de Clientes
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Tabla de Canales de Clientes 
        /// </summary>
        public DbSet<Channel> Channels { get; set; }

        /// <summary>
        /// Tabla de tipo de negocios de clientes
        /// </summary>
        public DbSet<TypeBusiness> TypeBusiness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Sequence> Sequences { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TypeService> TypeServices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<TypePoll> TypePolls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Service> Services { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<ServiceDetail> ServiceDetails { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionDetail> QuestionDetails { get; set; }

        /// <summary>
        /// Tabla de Campañas
        /// </summary>
        public DbSet<Campaign> Campaigns { get; set; }

        /// <summary>
        /// Tabla de Serivicios de Campaña
        /// </summary>
        public DbSet<CampaignServices> CampaignsServices { get; set; }

        /// <summary>
        /// Tabla de Status de Campañas
        /// </summary>
        public DbSet<StatusCampaign> StatusCampaigns { get; set; }

        /// <summary>
        /// Tabla de Tareas
        /// </summary>
        public DbSet<TaskCampaign> TaskCampaigns { get; set; }
        /// <summary>
        /// Tabla de sms
        /// </summary>
        public DbSet<Sms> Sms { get; set; }

        /// <summary>
        /// Tabla de Status de las Tareas
        /// </summary>
        public DbSet<StatusTask> StatusTasks { get; set; }

        /// <summary>
        /// Tabla de motivos de no implementacion de las tareas
        /// </summary>
        public DbSet<TaskNoImplementedReason> TaskNoImplementedReasons { get; set; }

        /// <summary>
        /// Tabla de Categorias de Producto
        /// </summary>
        public DbSet<ProductCategory> ProductCategories { get; set; }

        /// <summary>
        /// Tabla de Productos
        /// </summary>
        public DbSet<Product> Products { get; set; }

        /// <summary>
        /// Tabla de Respuestas
        /// </summary>
        public DbSet<Answer> Answers { get; set; }


        /// <summary>
        /// Tabla de Pedidos
        /// </summary>
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<PedidosItems> PedidosItems { get; set; }

        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<ChequePagos> ChequePagos { get; set; }
        public DbSet<PagosDetalles> PagosDetalles { get; set; }





        /// <summary>
        /// Tabla de detalle de respuestas
        /// </summary>
        public DbSet<AnswerDetail> AnswerDetails { get; set; }

        /// <summary>
        /// Tabla de Imagenes por local
        /// </summary>
        public DbSet<BranchImages> BranchImageses { get; set; }
        /// <summary>
        /// Tabla de Imagenes por Equipos
        /// </summary>
        public DbSet<EquipamentImages> EquipamentImages { get; set; }
        /// <summary>
        /// Tabla de estados de equipos
        /// </summary>
        public DbSet<Equipament_time> Equipament_times { get; set; }
        /// <summary>
        /// Tabla de Archivos de Carga Masiva
        /// </summary>
        public DbSet<BulkLoad> BulkLoads { get; set; }

        /// <summary>
        /// Tabla de Status de Carga Masiva
        /// </summary>
        public DbSet<BulkLoadStatus> BulksLoadStatus { get; set; }

        /// <summary>
        /// Tabla de Catalogos de Carga Masiva
        /// </summary>
        public DbSet<BulkLoadCatalog> BulkLoadCatalogs { get; set; }

        /// <summary>
        /// Tabla de Secciones por Tarea
        /// </summary>
        public DbSet<ServiceDetailTask> ServiceDetailTasks { get; set; }

        /// <summary>
        /// Tabla de Cabecera de Filtros
        /// </summary>
        public DbSet<CoreFilter> CoreFilters { get; set; }

        /// <summary>
        /// Tabla de Detalle de Filtros
        /// </summary>
        public DbSet<CoreFilterDetail> CoreFilterDetails { get; set; }


        public DbSet<Region> Regions { get; set; }


        /// <summary>
        /// Tabla de Equipament
        /// </summary>
        public DbSet<Equipament> Equipaments { get; set; }

        /// <summary>
        /// Tabla de Equipament_status
        /// </summary>
        public DbSet<Equipament_status> Equipaments_status { get; set; }

        /// <summary>
        /// Tabla de Equipament_type
        /// </summary>
        public DbSet<Equipament_type> Equipaments_type { get; set; }

        /// <summary>
        /// Tabla de DASHBOAR
        /// </summary>
        public DbSet<Dashboard> Dashboards { get; set; }


        /// <summary>
        /// Tabla de estados por usuario
        /// </summary>
        public DbSet<StatustaskUser> StatustaskUsers { get; set; }

        /// <summary>
        /// Tabla de ACTIVIDADES
        /// </summary>
        public DbSet<Activity> Activities { get; set; }


        /// <summary>
        /// Tabla de Pollster
        /// </summary>
        public DbSet<Pollster> Pollsters { get; set; }
        /// <summary>
        /// Tabla de ESTADOS DINAMICOS
        /// </summary>
        public DbSet<StatusTaskAccount> StatusTaskAccounts { get; set; }
        public IEnumerable<T> Query<T>(string query) where T : class
        {
            return connection.Query<T>(query);
        }
    }
}
