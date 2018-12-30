using System;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Models
{
    public partial class QuanLyNhanSuContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public QuanLyNhanSuContext()
        {
        }

        public QuanLyNhanSuContext(DbContextOptions<QuanLyNhanSuContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Allowance> Allowance { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<ContractType> ContractType { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContract { get; set; }
        public virtual DbSet<EmployeeDiscipline> EmployeeDiscipline { get; set; }
        public virtual DbSet<EmployeeInsurrance> EmployeeInsurrance { get; set; }
        public virtual DbSet<EmployeeReward> EmployeeReward { get; set; }
        public virtual DbSet<EmployeeSalaryLevel> EmployeeSalaryLevel { get; set; }
        public virtual DbSet<Insurrance> Insurrance { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<PersonalTaxRate> PersonalTaxRate { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Relatives> Relatives { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }
        public virtual DbSet<RewardAndDisciplineMethod> RewardAndDisciplineMethod { get; set; }
        public virtual DbSet<SalaryLevel> SalaryLevel { get; set; }
        public virtual DbSet<Specialize> Specialize { get; set; }
        public virtual DbSet<TimeKeeping> TimeKeeping { get; set; }
        public virtual DbSet<TimeWork> TimeWork { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }
        public virtual DbSet<WorkProcess> WorkProcess { get; set; }

        // Unable to generate entity type for table 'dbo.Salary'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\ANHNGOC;Database=QuanLyNhanSu;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allowance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveDate).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();
                
                entity.Property(e => e.ContractTypeId).HasColumnName("ContractTypeID");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.ContractTypeId)
                    .HasConstraintName("FK_Contract_ContractType");
            });

            modelBuilder.Entity<ContractType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();
                
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasColumnType("text");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);


                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desciption).HasMaxLength(255);

                entity.Property(e => e.DicisionNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveDate).HasColumnType("date");

                entity.Property(e => e.RewardAndDisciplineMethodId).HasColumnName("RewardAndDisciplineMethodID");

                entity.Property(e => e.SignBy).HasMaxLength(250);

                entity.Property(e => e.SignDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.RewardAndDisciplineMethod)
                    .WithMany(p => p.Discipline)
                    .HasForeignKey(d => d.RewardAndDisciplineMethodId)
                    .HasConstraintName("FK_Discipline_RewardAndDisciplineMethod");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Avatar).HasColumnType("image");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.CurrentAddress).HasMaxLength(50);

                entity.Property(e => e.DayInCompany).HasColumnType("date");

                entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                

                entity.Property(e => e.Fullname).HasMaxLength(100);

                entity.Property(e => e.PayrollDay).HasColumnType("date");

                entity.Property(e => e.PositionId).HasColumnName("PositionID");

                entity.Property(e => e.SpecializeId).HasColumnName("SpecializeID");

                entity.Property(e => e.WardId).HasColumnName("WardID");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Employee_Department");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employee_Position");

                entity.HasOne(d => d.Specialize)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.SpecializeId)
                    .HasConstraintName("FK_Employee_Specialize");

                entity.HasOne(d => d.Ward)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.WardId)
                    .HasConstraintName("FK_Employee_Employee");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.SigningDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Contract)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.ContractId)
                    .HasConstraintName("FK_EmployeeContract_Contract");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeContract_Employee");
            });

            modelBuilder.Entity<EmployeeDiscipline>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DisciplineId).HasColumnName("DisciplineID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.HasOne(d => d.Discipline)
                    .WithMany(p => p.EmployeeDiscipline)
                    .HasForeignKey(d => d.DisciplineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDiscipline_Discipline");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeDiscipline)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeDiscipline_Employee");
            });

            modelBuilder.Entity<EmployeeInsurrance>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.InsurranceId).HasColumnName("InsurranceID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeInsurrance)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeInsurrance_Employee");

                entity.HasOne(d => d.Insurrance)
                    .WithMany(p => p.EmployeeInsurrance)
                    .HasForeignKey(d => d.InsurranceId)
                    .HasConstraintName("FK_EmployeeInsurrance_Insurrance");
            });

            modelBuilder.Entity<EmployeeReward>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Note).HasMaxLength(255);
                entity.Property(e => e.RewardId).HasColumnName("RewardID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeReward)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeReward_Employee");

                entity.HasOne(d => d.Reward)
                    .WithMany(p => p.EmployeeReward)
                    .HasForeignKey(d => d.RewardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeReward_Reward");
            });

            modelBuilder.Entity<EmployeeSalaryLevel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.SalaryLevelId).HasColumnName("SalaryLevelID");

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeSalaryLevel)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_EmployeeSalaryLevel_Employee");

                entity.HasOne(d => d.SalaryLevel)
                    .WithMany(p => p.EmployeeSalaryLevel)
                    .HasForeignKey(d => d.SalaryLevelId)
                    .HasConstraintName("FK_EmployeeSalaryLevel_SalaryLevel");
            });

            modelBuilder.Entity<Insurrance>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();


                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FailLogin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Fullname).HasMaxLength(500);

                entity.Property(e => e.LastChangePass).HasColumnType("datetime");

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PersonalTaxRate>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
                

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
                

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(255);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Relatives>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.Career).HasMaxLength(255);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Fullname).HasMaxLength(255);

                entity.Property(e => e.WardId).HasColumnName("WardID");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Relatives)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Relatives_Employee");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Desciption).HasMaxLength(255);

                entity.Property(e => e.DicisionNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EffectiveDate).HasColumnType("date");

                entity.Property(e => e.RewardAndDisciplineMethodId).HasColumnName("RewardAndDisciplineMethodID");

                entity.Property(e => e.SignBy).HasMaxLength(250);

                entity.Property(e => e.SignDate).HasColumnType("datetime");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.RewardAndDisciplineMethod)
                    .WithMany(p => p.Reward)
                    .HasForeignKey(d => d.RewardAndDisciplineMethodId)
                    .HasConstraintName("FK_Reward_Reward");
            });

            modelBuilder.Entity<RewardAndDisciplineMethod>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
                

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Reason).HasMaxLength(255);
            });

            modelBuilder.Entity<SalaryLevel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EffectiveDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Specialize>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);
            });
            

            modelBuilder.Entity<TimeKeeping>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Note).HasMaxLength(255);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.TimeKeeping)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_TimeKeeping_Employee");
            });

            modelBuilder.Entity<TimeWork>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Month).HasColumnType("date");

                entity.Property(e => e.TotalExtraTimeWork).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Ward)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Ward_District");
            });

            modelBuilder.Entity<WorkProcess>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CompanyWorkedName).HasMaxLength(255);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.EndWork).HasColumnType("date");

                entity.Property(e => e.StartWork).HasColumnType("date");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.WorkProcess)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_WorkProcess_Employee");
            });
        }
    }
}
