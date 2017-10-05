namespace ConductorAppAdmin.Controllers
{
    using ConductorAppAdmin.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Queue;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Controller da comunicação
    /// </summary>
    public class CommunicationController : Controller
    {
        #region Propriedades

        /// <summary>
        /// Contexto do banco de dados.
        /// </summary>
        private readonly ConductorAppAdminContext _context;

        /// <summary>
        /// Opções de configuração da storage account..
        /// </summary>
        private readonly StorageAccountOption _option;

        #endregion

        #region Construtores

        /// <summary>
        /// Inicializa a controller de comunicação
        /// </summary>
        /// <param name="context">Contexto do banco de dados.</param>
        /// <param name="option">Opção de configuração.</param>
        public CommunicationController(ConductorAppAdminContext context, IOptions<StorageAccountOption> option)
        {
            _context = context;
            _option = option.Value;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// GET: CommunicationModels
        /// </summary>
        /// <returns>View</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Communication.ToListAsync());
        }

        /// <summary>
        /// GET: CommunicationModels/Details/5
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }

            return View(communicationModel);
        }

        /// <summary>
        /// GET: CommunicationModels/Create
        /// </summary>
        /// <returns>View</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST: CommunicationModels/Create
        /// </summary>
        /// <param name="communicationModel">Comunicação.</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] Communication communicationModel)
        {
            if (ModelState.IsValid)
            {
                var account = CloudStorageAccount.Parse(_option.StorageAccountKeyOption);
                var client = account.CreateCloudQueueClient();
                var queue = client.GetQueueReference(_option.QueueNameOption);

                await queue.AddMessageAsync(new CloudQueueMessage($"{communicationModel.Id}"));

                await _context.AddAsync(communicationModel);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(communicationModel);
        }

        /// <summary>
        /// GET: CommunicationModels/Edit/5
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }
            return View(communicationModel);
        }

        /// <summary>
        /// POST: CommunicationModels/Edit/5
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="communicationModel">Comunicação</param>
        /// <returns>View</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description")] Communication communicationModel)
        {
            if (id != communicationModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(communicationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Communication.Any(e => e.Id == communicationModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(communicationModel);
        }

        /// <summary>
        /// GET: CommunicationModels/Delete/5
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>View</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            if (communicationModel == null)
            {
                return NotFound();
            }

            return View(communicationModel);
        }

        /// <summary>
        /// POST: CommunicationModels/Delete/5
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>View</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var communicationModel = await _context.Communication.SingleOrDefaultAsync(m => m.Id == id);
            _context.Communication.Remove(communicationModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}
