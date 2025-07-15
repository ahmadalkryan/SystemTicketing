using DataAccessLyer.Enum;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Infrastructure.Seeds
{
    public class DataSeeder(
       AppDbContext context
      )
    {
        private readonly AppDbContext _context = context;
      
        public void SeedData()
        {
            var shouldSave = false;


            if (shouldSave)
            {
                shouldSave = false;
                _context.SaveChanges();
            }
           

            if(!_context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        Name="Employee"
                    },
                    new Role
                    {
                        Name ="MaintenanceEmployee"
                    },
                    new Role
                    {
                        Name ="MaintenanceManger"
                    }
                };

                _context.Roles.AddRange(roles);
                shouldSave=true;
            }


            // 2. Seed TicketStatuses
            if (!_context.TicketStatuses.Any())
            {
                _context.TicketStatuses.AddRange(
                    new TicketStatus { StatusName =TicketStatusEnum.Pending },
                    new TicketStatus { StatusName = TicketStatusEnum.New },
                    new TicketStatus { StatusName = TicketStatusEnum.Refund },
                    new TicketStatus { StatusName = TicketStatusEnum.Complete}
                );
                shouldSave = true;
            }

            if (!_context.DeviceCategories.Any())
            {
                _context.DeviceCategories.AddRange(
                    new DeviceCategory { CategoryName = "Laptop", Abbreviation = "LT" },
                    new DeviceCategory { CategoryName = "Desktop", Abbreviation = "DT" },
                    new DeviceCategory { CategoryName = "Printer", Abbreviation = "PR" },
                    new DeviceCategory { CategoryName = "Server", Abbreviation = "SV" }
                );
                shouldSave = true;
            }

            if (!_context.Users.Any())
            {
                var users = new List<User>
    {
        new User {
            Name = "Admin User",
            Department = "IT",
            Email = "admin@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("Admin@123")
        },
        new User {
            Name = "Tech Staff",
            Department = "Maintenance",
            Email = "tech@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("Tech@456")
        },
        new User {
            Name = "End User",
            Department = "Sales",
            Email = "user@example.com",
            Password = BCrypt.Net.BCrypt.HashPassword("User@789")
        }
    };
                _context.Users.AddRange(users);
                shouldSave = true;
            }




            if (!_context.Tickets.Any())
            {
                var openStatus = _context.TicketStatuses.First(s => s.StatusName == TicketStatusEnum.New);
                var adminUser = _context.Users.First(u => u.Email == "admin@example.com");

                _context.Tickets.AddRange(
                    new Ticket
                    {
                        TicketNumber = "TICKET-001",
                        Description = "Monitor not working",
                        AttachmentPath = "/attachments/monitor.jpg",
                        CreatedDate = DateTime.UtcNow.AddDays(-2),
                        UpdatedDate = DateTime.UtcNow.AddDays(-1),
                        _status= openStatus
                    },
                    new Ticket
                    {
                        TicketNumber = "TICKET-002",
                        Description = "Keyboard keys stuck",
                        AttachmentPath = null,
                        CreatedDate = DateTime.UtcNow.AddHours(-3),
                        UpdatedDate = DateTime.UtcNow.AddHours(-1),
                        _status = openStatus
                    }
                );
                shouldSave = true;
            }



            if (shouldSave)
            {
                _context.SaveChanges();
                shouldSave = false;
            }




            if (!_context.TicketTraces.Any())
            {
                var tickets = _context.Tickets.ToList();
                var admin = _context.Users.First(u => u.Email == "admin@example.com");
                var inProgress = _context.TicketStatuses.First(s => s.StatusName == TicketStatusEnum.Pending);

                foreach (var ticket in tickets)
                {
                    _context.TicketTraces.Add(new TicketTrace
                    {
                        Note = $"Ticket created by {admin.Name}",
                        CreateTime = ticket.CreatedDate,
                        UpdateTime = ticket.CreatedDate,
                        _ticket = ticket,
                        _user = admin,
                        _ticketStatus = ticket._status,
                    });

                    // Add status update trace
                    _context.TicketTraces.Add(new TicketTrace
                    {
                        Note = "Started troubleshooting",
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow,
                        _ticket = ticket,
                        _user = admin,
                        _ticketStatus = inProgress
                    });
                }
                shouldSave = true;
            }




            //if (!_context.Notifications.Any())
            //{
            //    var ticket = _context.Tickets.First();
            //    _context.Notifications.AddRange(
            //        new Notification
            //        {
            //            Message = "Your ticket has been received",
            //            IsRead = false,
            //            SentAt = DateTime.UtcNow,
            //            _ticket = ticket
            //        },
            //        new Notification
            //        {
            //            Message = "Technician assigned to your ticket",
            //            IsRead = true,
            //            SentAt = DateTime.UtcNow.AddHours(1),
            //            _ticket = ticket
            //        }
            //    );
            //    shouldSave = true;
            //}

            // Final save if any changes were made
            if (shouldSave)
            {
                _context.SaveChanges();
            }
           





























        }
    }
}
//if (!_identityAppDbContext.Roles.Any())
//{
//    shouldUpdateContext = true;

//    var roleUser = new ApplicationRole()
//    {
//        Name = DefaultSetting.UserRoleName,
//    };
//    var roleEmployee = new ApplicationRole()
//    {
//        Name = DefaultSetting.EmployeeRoleName,
//    };
//    var roleAdmin = new ApplicationRole()
//    {
//        Name = DefaultSetting.AdminRoleName
//    };
//    var roleCustomer = new ApplicationRole()
//    {
//        Name = DefaultSetting.CustomerRoleName
//    };
//    _roleManeger.CreateAsync(roleAdmin).GetAwaiter().GetResult();
//    _roleManeger.CreateAsync(roleUser).GetAwaiter().GetResult();
//    _roleManeger.CreateAsync(roleEmployee).GetAwaiter().GetResult();
//    _roleManeger.CreateAsync(roleCustomer).GetAwaiter().GetResult();

//}

//if (!_identityAppDbContext.Users.Any(u => u.Email == DefaultSetting.DefaultAdminOneEmail))
//{
//    var adminUser = new ApplicationUser
//    {
//        Email = DefaultSetting.DefaultAdminOneEmail,
//        UserName = DefaultSetting.DefaultAdminOneUserName,
//        PhoneNumber = DefaultSetting.DefaultAdminOnePhone,
//        PhoneNumberConfirmed = true,
//        IsActive = true
//    };

//    var result = _userManager.CreateAsync(adminUser, DefaultSetting.DefaultAdminPassword).GetAwaiter().GetResult();

//    if (result.Succeeded)
//    {
//        _userManager.AddToRoleAsync(adminUser, DefaultSetting.AdminRoleName).GetAwaiter().GetResult();
//        var code = _userManager.GenerateEmailConfirmationTokenAsync(adminUser).GetAwaiter().GetResult();
//        _userManager.ConfirmEmailAsync(adminUser, code).GetAwaiter().GetResult();
//    }

//    shouldUpdateContext = true;
//}
//if (shouldUpdateContext)
//{
//    _identityAppDbContext.SaveChanges();
//    shouldUpdateContext = false;
//}
//var identityUser = _identityAppDbContext.Users.FirstOrDefault(u => u.Email == DefaultSetting.DefaultAdminOneEmail);
//if (identityUser == null)
//{
//    throw new InvalidOperationException("Admin user not found after seeding.");
//}

//var adminUserId = identityUser.Id;

//// Seed ContactTypes
//if (!_context.ContactTypes.Any())
//{
//    var contactTypes = Enum.GetValues<ContactTypeEnum>()
//        .Select(type => new ContactType { Type = type })
//        .ToList();

//    _context.ContactTypes.AddRange(contactTypes);
//    shouldUpdateContext = true;
//}

//// Seed Customer (linked to Identity User)
//if (!_context.Customers.Any(c => c.UserId == adminUserId))
//{
//    var customer = new Customer
//    {
//        FirstName = DefaultSetting.DefaultAdminOneFName,
//        LastName = DefaultSetting.DefaultAdminOneLName,
//        UserId = adminUserId,
//        Country = "Damascus"
//    };

//    _context.Customers.Add(customer);
//    shouldUpdateContext = true;
//}

//// Seed Employee (linked to Identity User)
//if (!_context.Employees.Any(e => e.UserId == adminUserId))
//{
//    var employee = new Employee
//    {
//        HireDate = DateTime.Now,
//        UserId = adminUserId
//    };

//    _context.Employees.Add(employee);
//    shouldUpdateContext = true;
//}


            //if (!_context.Bookings.Any())
            //{
            //    var bookings = new List<Booking>
            //    {
            //        new Booking
            //        {
            //            CustomerId = adminUserId,
            //            Employeeid = adminUserId,
            //            BookingType = "Car Rental",
            //            StartDateTime = DateTime.Now.AddDays(1),
            //            EndDateTime = DateTime.Now.AddDays(2),
            //            Status = BookingStatusEnum.Pending,
            //            NumOfPassengers = 2
            //        },
            //        new Booking
            //        {
            //            CustomerId = 1,
            //            BookingType = "Event Booking",
            //            StartDateTime = DateTime.Now.AddDays(-5),
            //            EndDateTime = DateTime.Now.AddDays(-3),
            //            Status = BookingStatusEnum.Completed,
            //            NumOfPassengers = 6
            //        }
            //    };

            //    _context.Bookings.AddRange(bookings);
            //    shouldUpdateContext = true;
            //}

            //if (!_context.Categories.Any())
            //{
            //    var categories = new List<Category>
            //    {
            //        new Category
            //        {
            //           Title = "VIP"
            //        },
            //        new Category
            //        {
            //            Title = "Eco"
            //        },
            //        new Category
            //        {
            //            Title = "Family"
            //        },
            //        new Category
            //        {
            //            Title = "Luxury"
            //        },
            //        new Category
            //        {
            //            Title = "SUV"
            //        },
            //        new Category
            //        {
            //            Title = "Compact"
            //        }
            //    };

            //    _context.Categories.AddRange(categories);
            //    _context.SaveChanges();

            //    shouldUpdateContext = true;
            //}

            //// Seed Cars
            //if (!_context.Cars.Any())
            //{
            //    // Get category IDs for reference
            //    var vipCategory = _context.Categories.FirstOrDefault(c => c.Title == "VIP");
            //    var ecoCategory = _context.Categories.FirstOrDefault(c => c.Title == "Eco");
            //    var familyCategory = _context.Categories.FirstOrDefault(c => c.Title == "Family");
            //    var luxuryCategory = _context.Categories.FirstOrDefault(c => c.Title == "Luxury");
            //    var suvCategory = _context.Categories.FirstOrDefault(c => c.Title == "SUV");
            //    var compactCategory = _context.Categories.FirstOrDefault(c => c.Title == "Compact");

            //    var cars = new List<Car>
            //    {
            //        // VIP Cars
            //        new Car
            //        {
            //            Model = "Mercedes",
            //            Capacity = 4,
            //            Color = "Black",
            //            Image = "mercedes-s-class.jpg",
            //            CategoryId = vipCategory?.Id ?? 1,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 150.00m,
            //            Ppd = 1200.00m,
            //            Mbw = 5000.00m
            //        },
            //        new Car
            //        {
            //            Model = "BMW",
            //            Capacity = 4,
            //            Color = "White",
            //            Image = "bmw-7-series.jpg",
            //            CategoryId = vipCategory?.Id ?? 1,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 140.00m,
            //            Ppd = 1100.00m,
            //            Mbw = 4500.00m
            //        },

            //        // Eco Cars
            //        new Car
            //        {
            //            Model = "Tesla",
            //            Capacity = 5,
            //            Color = "Red",
            //            Image = "tesla-model-3.jpg",
            //            CategoryId = ecoCategory?.Id ?? 2,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 80.00m,
            //            Ppd = 600.00m,
            //            Mbw = 2000.00m
            //        },
            //        new Car
            //        {
            //            Model = "Toyota Prius",
            //            Capacity = 5,
            //            Color = "Blue",
            //            Image = "toyota-prius.jpg",
            //            CategoryId = ecoCategory?.Id ?? 2,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 60.00m,
            //            Ppd = 450.00m,
            //            Mbw = 1500.00m
            //        },

            //        // Family Cars
            //        new Car
            //        {
            //            Model = "Honda Odyssey",
            //            Capacity = 7,
            //            Color = "Silver",
            //            Image = "honda-odyssey.jpg",
            //            CategoryId = familyCategory?.Id ?? 3,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 70.00m,
            //            Ppd = 500.00m,
            //            Mbw = 1800.00m
            //        },
            //        new Car
            //        {
            //            Model = "Toyota Sienna",
            //            Capacity = 8,
            //            Color = "Gray",
            //            Image = "toyota-sienna.jpg",
            //            CategoryId = familyCategory?.Id ?? 3,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 75.00m,
            //            Ppd = 550.00m,
            //            Mbw = 2000.00m
            //        },

            //        // Luxury Cars
            //        new Car
            //        {
            //            Model = "Audi A8",
            //            Capacity = 4,
            //            Color = "Black",
            //            Image = "audi-a8.jpg",
            //            CategoryId = luxuryCategory?.Id ?? 4,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 120.00m,
            //            Ppd = 900.00m,
            //            Mbw = 3000.00m
            //        },
            //        new Car
            //        {
            //            Model = "Lexus LS",
            //            Capacity = 4,
            //            Color = "White",
            //            Image = "lexus-ls.jpg",
            //            CategoryId = luxuryCategory?.Id ?? 4,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 110.00m,
            //            Ppd = 850.00m,
            //            Mbw = 2800.00m
            //        },

            //        // SUV Cars
            //        new Car
            //        {
            //            Model = "Toyota Highlander",
            //            Capacity = 7,
            //            Color = "Black",
            //            Image = "toyota-highlander.jpg",
            //            CategoryId = suvCategory?.Id ?? 5,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 85.00m,
            //            Ppd = 650.00m,
            //            Mbw = 2200.00m
            //        },
            //        new Car
            //        {
            //            Model = "Honda CR-V",
            //            Capacity = 5,
            //            Color = "Blue",
            //            Image = "honda-cr-v.jpg",
            //            CategoryId = suvCategory?.Id ?? 5,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 65.00m,
            //            Ppd = 480.00m,
            //            Mbw = 1600.00m
            //        },

            //        // Compact Cars
            //        new Car
            //        {
            //            Model = "Honda Civic",
            //            Capacity = 5,
            //            Color = "Red",
            //            Image = "honda-civic.jpg",
            //            CategoryId = compactCategory?.Id ?? 6,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 45.00m,
            //            Ppd = 320.00m,
            //            Mbw = 1000.00m
            //        },
            //        new Car
            //        {
            //            Model = "Toyota Corolla",
            //            Capacity = 5,
            //            Color = "Silver",
            //            Image = "toyota-corolla.jpg",
            //            CategoryId = compactCategory?.Id ?? 6,
            //            CarStatus = CarStatusEnum.Available,
            //            Pph = 42.00m,
            //            Ppd = 300.00m,
            //            Mbw = 900.00m
            //        },
            //        new Car
            //        {
            //            Model = "Ford Focus",
            //            Capacity = 5,
            //            Color = "White",
            //            Image = "ford-focus.jpg",
            //            CategoryId = compactCategory?.Id ?? 6,
            //            CarStatus = CarStatusEnum.NotAvailable,
            //            Pph = 40.00m,
            //            Ppd = 280.00m,
            //            Mbw = 800.00m
            //        }
            //    };

            //    _context.Cars.AddRange(cars);
            //    shouldUpdateContext = true;
            //}

            //if (shouldUpdateContext)
            //{
            //    shouldUpdateContext = false;
            //    _identityAppDbContext.SaveChanges();
            //    _context.SaveChanges();
            //}