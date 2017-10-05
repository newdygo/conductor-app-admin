namespace ConductorAppAdmin
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    /// <summary>
    /// Classe principal.
    /// </summary>
    public class Program
    {
        #region Methods

        /// <summary>
        /// Método inicial de execução.
        /// </summary>
        /// <param name="args">Argumentos de entrada.</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// Inicializa o host web.
        /// </summary>
        /// <param name="args">Argumentos de entrada.</param>
        /// <returns>Retorna um host web.</returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        #endregion
    }
}
