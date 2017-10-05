namespace ConductorAppAdmin.Models
{
    /// <summary>
    /// Opções da storage account.
    /// </summary>
    public class StorageAccountOption
    {
        #region Propriedades
        
        /// <summary>
        /// Chave da storage account.
        /// </summary>
        public string StorageAccountKeyOption { get; set; }

        /// <summary>
        /// Nome da fila.
        /// </summary>
        public string QueueNameOption { get; set; }

        #endregion
    }
}
