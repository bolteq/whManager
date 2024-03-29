﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using whManagerAPI.Helpers;
using whManagerAPI.Models;
using whManagerLIB.Helpers;
using whManagerLIB.Models;

namespace whManagerAPI.Services
{

    public interface ICompanyService
    {
        Task<Company> GetCompany(int id);
        IQueryable<Company> GetCompanies();
        Task<Company> AddCompany(Company company);
        Task<bool> DeleteCompany(int id);
    }
    /// <summary>
    /// Klasa serwisu odpowiedzialnego za operacje na obiektach Company w bazie danych
    /// </summary>
    public class CompanyService : ICompanyService
    {
        private readonly WHManagerDbContext _context;

        /// <summary>
        /// Konstruktor wstrzykujący zależności serwisu
        /// </summary>
        /// <param name="context">Klasa kontekstu bazy danych</param>
        public CompanyService(WHManagerDbContext context)
        {
            _context = context;
        }

        #region GetCompany
        /// <summary>
        /// Metoda pobierająca z bazy danych obiekt o podanym ID
        /// </summary>
        /// <param name="id">Id obiektu do pobrania</param>
        /// <returns>(awaitable) Pobrany obiekt Company</returns>
        public async Task<Company> GetCompany(int id)
        {
            var company = await _context
                                .Companies
                                .FirstOrDefaultAsync(c => c.Id == id);

            return company;
        }
        #endregion
        #region GetCompanies
        /// <summary>
        /// Metoda pobierająca obiekty Company z bazy danych
        /// </summary>
        /// <returns>IQueryable obiektów Company</returns>
        public IQueryable<Company> GetCompanies()
        {
            var companies = _context
                            .Companies;

            return companies;
        }
        #endregion
        #region AddCompany
        /// <summary>
        /// Metoda aktualizująca
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<Company> AddCompany(Company company)
        {

            bool bExists = await _context
                                .Companies
                                .AnyAsync(c => c.Id == company.Id);

            switch (bExists)
            {
                case true:
                    _context
                        .Companies
                        .Update(company);
                    await _context.SaveChangesAsync();
                    break;
                case false:
                    await _context.Companies.AddAsync(company);
                    await _context.SaveChangesAsync();
                    break;
            }

            return company;

        }
        #endregion
        #region DeleteCompany
        /// <summary>
        /// Metoda usuwająca z bazy danych obiekt Company o podanym Id
        /// </summary>
        /// <param name="id">Id obiektu do usunięcia</param>
        /// <returns>True - sukces, False - niepowodzenie</returns>
        public async Task<bool> DeleteCompany(int id)
        {
            var company = await _context
                                .Companies
                                .FirstOrDefaultAsync(c => c.Id == id);

            //Zwróc false jeśli nie istnieje taka firma w bazie
            if (company == null)
            {
                return false;
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
