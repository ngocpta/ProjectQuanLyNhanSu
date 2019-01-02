using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class QuanLyNhanSuContext : DbContext
    {
        public QuanLyNhanSuContext()
        {
        }

        public QuanLyNhanSuContext(DbContextOptions<QuanLyNhanSuContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allowance> Allowance { get; set; }
        public virtual DbSet<ConfigureTimeWork> ConfigureTimeWork { get; set; }
        public virtual DbSet<ContractType> ContractType { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Discipline> Discipline { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<EmployeeAllowance> EmployeeAllowance { get; set; }
        public virtual DbSet<EmployeeContract> EmployeeContract { get; set; }
        public virtual DbSet<EmployeeDiscipline> EmployeeDiscipline { get; set; }
        public virtual DbSet<EmployeeInsurrance> EmployeeInsurrance { get; set; }
        public virtual DbSet<EmployeeReward> EmployeeReward { get; set; }
        public virtual DbSet<Insurrance> Insurrance { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<PersonalTaxRate> PersonalTaxRate { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }
        public virtual DbSet<RewardAndDisciplineMethod> RewardAndDisciplineMethod { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<Specialize> Specialize { get; set; }
        public virtual DbSet<TimeKeeping> TimeKeeping { get; set; }
        public virtual DbSet<Ward> Ward { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=35.198.246.115,1433;User Id=ngocpta1691; Password=ngoc69nlan; Encrypt=True;TrustServerCertificate=True;Connection Timeout=30; Database=Ngocpta_6969;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Allowance>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<ConfigureTimeWork>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<ContractType>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<Contracts>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractTypeId).IsUnicode(false);

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.Contracts)
                    .HasForeignKey(d => d.ContractTypeId)
                    .HasConstraintName("FK_Contract_ContractType");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.PhoneNumber).IsUnicode(false);
            });

            modelBuilder.Entity<Discipline>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DicisionNo).IsUnicode(false);

                entity.Property(e => e.RewardAndDisciplineMethodId).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.HasOne(d => d.RewardAndDisciplineMethod)
                    .WithMany(p => p.Discipline)
                    .HasForeignKey(d => d.RewardAndDisciplineMethodId)
                    .HasConstraintName("FK_Discipline_RewardAndDisciplineMethod");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DepartmentId).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.PositionId).IsUnicode(false);

                entity.Property(e => e.SpecializeId).IsUnicode(false);

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

            modelBuilder.Entity<EmployeeAllowance>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AllowanceId).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.HasOne(d => d.Allowance)
                    .WithMany(p => p.EmployeeAllowance)
                    .HasForeignKey(d => d.AllowanceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAllowance_Allowance");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeAllowance)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeAllowance_Employee");
            });

            modelBuilder.Entity<EmployeeContract>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ContractId).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeContract)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EmployeeContract_Employee");
            });

            modelBuilder.Entity<EmployeeDiscipline>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DisciplineId).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

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
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.InsurranceId).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

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
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.RewardId).IsUnicode(false);

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

            modelBuilder.Entity<Insurrance>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_Tbl_Login");

                entity.Property(e => e.Username)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FailLogin).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<PersonalTaxRate>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.Code).IsUnicode(false);
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedBy).IsUnicode(false);

                entity.Property(e => e.DicisionNo).IsUnicode(false);

                entity.Property(e => e.RewardAndDisciplineMethodId).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);

                entity.HasOne(d => d.RewardAndDisciplineMethod)
                    .WithMany(p => p.Reward)
                    .HasForeignKey(d => d.RewardAndDisciplineMethodId)
                    .HasConstraintName("FK_Reward_Reward");
            });

            modelBuilder.Entity<RewardAndDisciplineMethod>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Month, e.Year })
                    .HasName("PK_Salary_1");

                entity.Property(e => e.EmployeeId).IsUnicode(false);
            });

            modelBuilder.Entity<Specialize>(entity =>
            {
                entity.Property(e => e.Id)
                    .IsUnicode(false)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<TimeKeeping>(entity =>
            {
                entity.HasKey(e => new { e.EmployeeId, e.Date })
                    .HasName("PK_TimeKeeping_1");

                entity.Property(e => e.EmployeeId).IsUnicode(false);

                entity.Property(e => e.UpdatedBy).IsUnicode(false);
            });

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Code).IsUnicode(false);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Ward)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Ward_District");
            });
        }
    }
}
