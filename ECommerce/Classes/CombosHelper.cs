using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Classes
{
    public class CombosHelper : IDisposable
    {
        private static ECommerceContext db = new ECommerceContext();

        public static List<Department> GetDepartments()
        {
            var departments = db.Departments.ToList();

            departments.Add(new Department
            {
                DepartmentId = 0,
                Name = "[Select a department]..."
            });

            return departments.OrderBy(d => d.Name).ToList();

        }

        public static List<City> GetCities()
        {
            var cities = db.Cities.ToList();
            cities.Add(new City
            {
                CityId = 0,
                Name = "[Select a city...]"
            });

            return cities.OrderBy(d => d.Name).ToList();
        }

        public static List<Company> GetCompanies()
        {
            var companies = db.Companies.ToList();
            companies.Add(new Company
            {
                CityId = 0,
                Name = "[Select a company...]"
            });

            return companies.OrderBy(d => d.Name).ToList();
        }

        public static List<Tax> GetTaxes(int companyId)
        {
            var taxes = db.Taxes.Where(t => t.CompanyId == companyId).ToList();
            taxes.Add(new Tax
            {
                TaxId = 0,
                Description = "[Select a tax...]"
            });

            return taxes.OrderBy(d => d.Description).ToList();
        }

        public static List<Category> GetCategories(int companyId)
        {
            var categories = db.Categories.Where(c => c.CompanyId == companyId).ToList();
            categories.Add(new Category
            {
                CategoryId = 0,
                Description = "[Select a category...]"
            });

            return categories.OrderBy(d => d.Description).ToList();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}