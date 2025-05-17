using Application.Common;
using Domain.Entities;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class TicketNumberGenerator : ITicketNumberGenerator
    {
        private readonly AppDbContext _context;

        public TicketNumberGenerator(AppDbContext context)
        {
            _context = context;
        }
        public string GenerateTicketNumber(int deviceCategoryId)
        {
           
            var device = _context.DeviceCategories.Find(deviceCategoryId);
            if (device == null) throw new ArgumentException("Invalid device category");

            var currentYear = DateTime.Now.Year;
            var lastNumber = _context.Tickets
                .Count(t => t.DeciveCategoryId==deviceCategoryId &&
                          t.CreatedDate.Year == currentYear);
            return $"{currentYear}-{(lastNumber + 1):D3}-{ device.Abbreviation}";
        }
    }
}
