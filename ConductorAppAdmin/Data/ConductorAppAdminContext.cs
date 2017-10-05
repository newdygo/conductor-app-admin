namespace ConductorAppAdmin.Models
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Contexto do banco de dados.
    /// </summary>
    public class ConductorAppAdminContext : DbContext
    {
        #region Construtctors

        /// <summary>
        /// Inicializa a class do contexto
        /// </summary>
        /// <param name="options">Opções do contexto.</param>
        public ConductorAppAdminContext (DbContextOptions<ConductorAppAdminContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Tabela de Comunicações.
        /// </summary>
        public DbSet<Communication> Communication { get; set; }

        #endregion
    }
}
