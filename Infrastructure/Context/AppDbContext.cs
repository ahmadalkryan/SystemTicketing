using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {
            
        }

        #region
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }

        public virtual DbSet<TicketTrace> TicketTraces { get; set; }

        public virtual DbSet<Notifiction> Notifictions { get; set; }

        public virtual DbSet<DeviceCategory> DeviceCategories { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Ticket>(t =>
            {
                t.ToTable("Ticket").HasKey(t => t.Id);
                t.Property(t => t.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id");
                t.Property(t => t.TicketNumber).HasColumnName("TicketNumber").IsRequired();
                t.Property(t => t.CreatedDate).HasColumnType("datetime2(7)")
                .HasColumnName("CreateDate").IsRequired();
                t.Property(t => t.UpdatedDate).HasColumnName("updateDate")
                .HasColumnType("datetime2(7)");
                t.Property(t => t.AttachmentPath).HasColumnType("nvarchar(50)").
                HasColumnName("AttachementPath").IsRequired();
                t.Property(t => t.Description).HasColumnName("Description")
                .HasColumnType("nvarchar(100)").IsRequired();

                t.HasOne(t => t._deviceType).WithMany(t => t.Tickets).
                HasForeignKey(f => f.DeciveCategoryId).OnDelete(DeleteBehavior.NoAction);
                t.Property(t => t.DeciveCategoryId).HasColumnName("DeviceCtegoryID");
                t.HasOne(t => t._status).WithMany(t => t.Tickets).
                HasForeignKey(f => f.TicketStatusId).OnDelete(DeleteBehavior.NoAction);
                t.Property(t => t.TicketStatusId).HasColumnName("TicketStatusId");
            });
            modelBuilder.Entity<DeviceCategory>(d =>
            {
                d.ToTable("DeviceCategory").HasKey(t => t.Id);
                d.Property(t => t.Id).ValueGeneratedOnAdd().HasColumnName("ID");

                d.Property(t => t.CategoryName).HasColumnType("nvarchar(30)").
                HasColumnName("Name").IsRequired();
                d.Property(t => t.Abbreviation).
                HasColumnName("abbreviation").HasColumnType("nvarchar(20)").IsRequired();



            });
            modelBuilder.Entity<TicketStatus>(t =>
            {
                t.ToTable("TicketStatus").HasKey(d => d.Id);
                t.Property(t => t.Id).ValueGeneratedOnAdd().HasColumnName("id");
                t.Property(t => t.StatusName).HasConversion<string>()
                .HasColumnName("Name").IsRequired().HasColumnType("nvarchar(10)");

            });
            modelBuilder.Entity<Notifiction>(t =>
            {
             t.ToTable("Notifiction").HasKey(t=>t.Id);
                t.Property(t=>t.Id).ValueGeneratedOnAdd ().HasColumnName("Id");

                t.Property(t => t.Message).HasColumnName("Message").
                HasColumnType("nvarchar(40)").IsRequired();
                t.Property(t=>t.IsRead).HasColumnName("IsRead")
                .HasDefaultValue(false).IsRequired();

                t.Property(t => t.SentAt).HasColumnName("sentAt")
                .HasColumnType("datetime2(7)").IsRequired();

                t.HasOne(t => t._ticket).WithMany(t => t.Notifictions).HasForeignKey(t => t.TicketId).OnDelete(DeleteBehavior.NoAction);
                t.Property(t => t.TicketId).HasColumnName("TicketID");
                t.HasOne(t=>t._user).WithMany(t=>t.Notifictions).HasForeignKey(t=>t.UserID).OnDelete(DeleteBehavior.NoAction);
                t.Property(t => t.UserID).HasColumnName("UserID").HasColumnType("nvarchar(100)");



            });
            modelBuilder.Entity<TicketTrace>(
                t =>
                {
                    t.ToTable("TicketTrace").HasKey(t=>t.Id);
                    t.Property(t => t.Id).ValueGeneratedOnAdd().HasColumnName("Id");
                    t.Property(t => t.Note).HasColumnName("Note").HasColumnType("nvarchar(50)").IsRequired();
                    t.Property(t => t.CreateTime).HasColumnName("CreateTime").HasColumnType("datetime2(7)").IsRequired();
                    t.Property(t => t.UpdateTime).HasColumnName("UpdateTime").HasColumnType("datetime2(7)");
                    //   t.Property(t => t.NewStatusID).HasColumnName("NewStatus");
                    t.HasOne(t => t._ticket).WithMany(t => t.ticketTraces).HasForeignKey(t => t.TicketId).
                    OnDelete(DeleteBehavior.NoAction);
                    t.Property(t => t.TicketId).HasColumnName("TicketID");

                    t.HasOne(t=>t._user).WithMany(t=>t.TicketTraces).HasForeignKey(t=>t.UserId).
                    OnDelete(DeleteBehavior.NoAction);

                    t.Property(t => t.UserId).HasColumnName("UserID").HasColumnType("nvarchar(100)");

                    t.HasOne(t => t._ticketStatus).WithMany(t => t.TicketsTraces).
                    HasForeignKey(t => t.StatusID).OnDelete(DeleteBehavior.NoAction);


                }
                
                );
     
            modelBuilder.Entity<User>(t=>{
                t.ToTable("User").HasKey(t=>t.UserId);
                t.Property(t => t.UserId).HasColumnName("user_id")
                .HasColumnType("nvarchar(100)");
                t.Property(t => t.Name).HasColumnName("name").
                HasColumnType("nvarchar(20)").IsRequired();
                t.Property(t => t.Department).HasColumnName("Department")
                .HasColumnType("nvarchar(20)").IsRequired();

                t.Property(t => t.Email).HasColumnName("Email").
                HasColumnType("nvarchar(20)").IsRequired();

                t.Property(t => t.Password).HasColumnType("nvarchar(10)").
                HasColumnName("Password").IsRequired();

                
            });

            modelBuilder.Entity<UserRole>(
                t =>
                {
                    t.ToTable("UserRole").HasKey(t => new {t.RoleId,t.UserId});
                    t.HasOne(t=>t._user).WithMany(t=>t.UserRoles).HasForeignKey(t=>t.UserId).OnDelete(DeleteBehavior.NoAction);
                    t.Property(t => t.UserId).HasColumnName("user_id").HasColumnType("nvarchar(100)");

                    t.HasOne(t => t._role).WithMany(t => t.UserRoles).HasForeignKey(t => t.RoleId).OnDelete(DeleteBehavior.NoAction);
                    t.Property(t=>t.RoleId).HasColumnName("Role_id");



                });
            modelBuilder.Entity<Role>(
                t =>
                {

                    t.ToTable("Role").HasKey(t=>t.Id);
                    t.Property(t => t.Id).HasColumnName("id").HasColumnType("nvarchar(30)");
                    t.Property(t => t.Name).HasColumnName("Name").HasColumnType("nvarchar(20)").IsRequired();
                }
                
                
                );           
        }
    }
 }
