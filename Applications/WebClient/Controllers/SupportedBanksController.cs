using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Applications.WebClient.Helpers;
using Applications.WebClient.Models.ViewModel;
using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Applications.WebClient.Controllers
{
    public class SupportedBanksController : Controller
    {
        private readonly ISupportedBankService _supportedBankService;
        private const int PageSize = 10;

        public SupportedBanksController(ISupportedBankService supportedBankService)
        {
            _supportedBankService = supportedBankService;
        }

        public async Task<IActionResult> Index(string filter = null,
            string columnName = null,
            bool isDescending = false,
            int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Must be a positive integer.");
            }

            OrderBySettings<SupportedBank> orderBySettings;
            if (string.IsNullOrEmpty(columnName))
            {
                orderBySettings = null;
            }
            else
            {
                Expression<Func<SupportedBank, object>> orderByExpression = columnName switch
                {
                    "Name" => _bank => _bank.Name,
                    _ => throw new NotImplementedException($"{nameof(columnName)} = {columnName}"),
                };
                orderBySettings = new OrderBySettings<SupportedBank>(orderByExpression, !isDescending);
            }
            int skip = (pageNumber - 1) * PageSize;

            var resultsAndTotalCountSupportedBanks = await _supportedBankService.GetResultAndTotalCountSupportedBanksAsync(filter, orderBySettings, skip, PageSize);
            int pageCount = Utils.CalculatePageCount(resultsAndTotalCountSupportedBanks.TotalCount, PageSize);

            ViewBag.Filter = filter;
            ViewBag.ColumnName = columnName;
            ViewBag.PageNumber = pageNumber;
            ViewBag.IsDescendingString = isDescending.ToString().ToLower();
            ViewBag.PageCount = pageCount;

            IEnumerable<SupportedBankVM> supportedBankVMs = resultsAndTotalCountSupportedBanks.Results.ToSupportedBankVMs();
            return View(supportedBankVMs);
        }

        // GET: supportedBanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: supportedBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupportedBankVM supportedBank)
        {
            try
            {
                await _supportedBankService.CreateSupportedBankAsync(supportedBank.Name);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: supportedBanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
                SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
                return View(supportedBankVM);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // GET: supportedBanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string name = User.FindFirst("Name")?.Value;
            ViewData["Name"] = name;
            try
            {
                SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
                SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
                return View(supportedBankVM);
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }

        // POST: supportedBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupportedBankVM supportedBank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _supportedBankService.UpdateSupportedBankAsync(id,
                        supportedBank.Name);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return View(e.Message);
                }
            }
            return View(supportedBank);
        }

        // GET: supportedBanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
            SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
            return View(supportedBankVM);
        }

        // POST: supportedBanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _supportedBankService.DeleteSupportedBankAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View(e.Message);
            }
        }
    }
}