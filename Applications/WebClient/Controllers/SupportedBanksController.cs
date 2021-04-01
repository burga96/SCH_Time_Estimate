using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Applications.WebClient.Helpers;
using Applications.WebClient.Models;
using Applications.WebClient.Models.ViewModel;
using Core.ApplicationServices.ApplicationDTOs;
using Core.ApplicationServices.ApplicationServiceInterfaces;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Applications.WebClient.Controllers
{
    public class SupportedBanksController : Controller
    {
        private readonly ISupportedBankService _supportedBankService;
        private readonly string _adminPassword;

        private const int PageSize = 10;

        public SupportedBanksController(ISupportedBankService supportedBankService,
            IConfiguration configuration)
        {
            _adminPassword = configuration["Wallet:AdminPassword"];
            _supportedBankService = supportedBankService;
        }

        public async Task<IActionResult> Index(string adminPassword,
            string filter = null,
            string columnName = null,
            bool isDescending = false,
            int pageNumber = 1)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Must be a positive integer.");
            }
            if (adminPassword != _adminPassword)
            {
                ViewData["AdminPasswordError"] = string.IsNullOrEmpty(adminPassword) ? "" : "Wrong admin password";
                ViewBag.Filter = filter;
                ViewBag.ColumnName = columnName;
                ViewBag.PageNumber = pageNumber;
                ViewBag.IsDescendingString = isDescending.ToString().ToLower();
                ViewBag.PageCount = 0;
                ViewBag.HasAdminPassword = false;
                return View(new List<SupportedBankVM>());
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
            ViewBag.HasAdminPassword = true;
            ViewBag.AdminPassword = adminPassword;

            IEnumerable<SupportedBankVM> supportedBankVMs = resultsAndTotalCountSupportedBanks.Results.ToSupportedBankVMs();
            return View(supportedBankVMs);
        }

        // GET: supportedBanks/Create
        [HttpGet]
        public IActionResult Create(string adminPassword)
        {
            SupportedBankVM supportedBankVM = new SupportedBankVM();
            supportedBankVM.HasAdminPassword = adminPassword == _adminPassword;
            supportedBankVM.Password = adminPassword;
            supportedBankVM.Error = supportedBankVM.HasAdminPassword ? "" : "Enter valid admin password";

            return View(supportedBankVM);
        }

        // POST: supportedBanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(SupportedBankVM supportedBank)
        {
            try
            {
                if (supportedBank.Password != _adminPassword)
                {
                    return RedirectToAction(nameof(Index));
                }
                await _supportedBankService.CreateSupportedBankAsync(supportedBank.Name);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message));
            }
        }

        // GET: supportedBanks/Details/5
        public async Task<IActionResult> Details(string adminPassword, int? id)
        {
            try
            {
                if (id == null)
                {
                    return View("~/Views/Shared/Error.cshtml", new ErrorViewModel("Id not found"));
                }
                SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
                SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
                supportedBankVM.HasAdminPassword = adminPassword == _adminPassword;
                supportedBankVM.Password = adminPassword;
                supportedBankVM.Error = supportedBankVM.HasAdminPassword ? "" : "Enter valid admin password";

                return View(supportedBankVM);
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message));
            }
        }

        // GET: supportedBanks/Edit/5
        public async Task<IActionResult> Edit(string adminPassword, int? id)
        {
            try
            {
                SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
                SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
                supportedBankVM.HasAdminPassword = adminPassword == _adminPassword;
                supportedBankVM.Password = adminPassword;
                supportedBankVM.Error = supportedBankVM.HasAdminPassword ? "" : "Enter valid admin password";

                return View(supportedBankVM);
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message));
            }
        }

        // POST: supportedBanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SupportedBankVM supportedBank)
        {
            if (!ModelState.IsValid)
            {
                return View(supportedBank);
            }

            try
            {
                if (supportedBank.Password != _adminPassword)
                {
                    supportedBank.HasAdminPassword = false;
                    supportedBank.Error = "Enter valid admin password";
                    return View(supportedBank);
                }
                await _supportedBankService.UpdateSupportedBankAsync(id,
                    supportedBank.Name);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message));
            }
        }

        // GET: supportedBanks/Delete/5
        public async Task<IActionResult> Delete(string adminPassword, int? id)
        {
            if (id == null)
            {
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel("Id not found"));
            }

            SupportedBankDTO supportedBankDTO = await _supportedBankService.GetSupportedBankByIdAsync(id.Value);
            SupportedBankVM supportedBankVM = new SupportedBankVM(supportedBankDTO);
            supportedBankVM.HasAdminPassword = adminPassword == _adminPassword;
            supportedBankVM.Password = adminPassword;
            supportedBankVM.Error = supportedBankVM.HasAdminPassword ? "" : "Enter valid admin password";
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
                return View("~/Views/Shared/Error.cshtml", new ErrorViewModel(e.Message));
            }
        }
    }
}