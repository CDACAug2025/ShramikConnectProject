using ShramikConnectWebApi.Data.Seed;

namespace ShramikConnectWebApi.Data;

using Microsoft.EntityFrameworkCore;
using ShramikConnectWebApi.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // ======================
    // DbSets
    // ======================
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Worker> Workers => Set<Worker>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<KycDocument> KycDocuments => Set<KycDocument>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<EscrowPayment> EscrowPayments => Set<EscrowPayment>();
    public DbSet<ChatRoom> ChatRooms => Set<ChatRoom>();
    public DbSet<ChatMessage> ChatMessages => Set<ChatMessage>();
    public DbSet<Dispute> Disputes => Set<Dispute>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<SystemLog> SystemLogs => Set<SystemLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ======================
        // USER & ROLE
        // ======================
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // ONE-TO-ONE PROFILES
        // ======================
        modelBuilder.Entity<Worker>()
            .HasOne(w => w.User)
            .WithOne(u => u.Worker)
            .HasForeignKey<Worker>(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Client>()
            .HasOne(c => c.User)
            .WithOne(u => u.Client)
            .HasForeignKey<Client>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Organization>()
            .HasOne(o => o.User)
            .WithOne(u => u.Organization)
            .HasForeignKey<Organization>(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        
        // ======================
// DECIMAL PRECISION (MySQL SAFE)
// ======================
        modelBuilder.Entity<Contract>()
            .Property(c => c.AgreedAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<EscrowPayment>()
            .Property(e => e.Amount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Job>()
            .Property(j => j.Budget)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Order>()
            .Property(o => o.TotalAmount)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Worker>()
            .Property(w => w.Rating)
            .HasPrecision(3, 2);

        // ======================
        // KYC DOCUMENTS
        // ======================
        modelBuilder.Entity<KycDocument>()
            .HasOne(k => k.User)
            .WithMany()
            .HasForeignKey(k => k.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<KycDocument>()
            .HasOne(k => k.VerifiedByUser)
            .WithMany()
            .HasForeignKey(k => k.VerifiedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // JOBS
        // ======================
        modelBuilder.Entity<Job>()
            .HasOne(j => j.PostedByUser)
            .WithMany(u => u.Jobs)
            .HasForeignKey(j => j.PostedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Job>()
            .HasOne(j => j.Contract)
            .WithOne(c => c.Job)
            .HasForeignKey<Contract>(c => c.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        // ======================
        // JOB APPLICATIONS
        // ======================
        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.Job)
            .WithMany(j => j.Applications)
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<JobApplication>()
            .HasOne(a => a.ApplicantUser)
            .WithMany()
            .HasForeignKey(a => a.ApplicantUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // CONTRACTS (MULTIPLE USER FKs)
        // ======================
        modelBuilder.Entity<Contract>()
            .HasOne(c => c.WorkerUser)
            .WithMany()
            .HasForeignKey(c => c.WorkerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Contract>()
            .HasOne(c => c.ClientUser)
            .WithMany()
            .HasForeignKey(c => c.ClientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // ESCROW PAYMENTS
        // ======================
        modelBuilder.Entity<EscrowPayment>()
            .HasKey(e => e.EscrowId);

        modelBuilder.Entity<EscrowPayment>()
            .HasOne(e => e.Contract)
            .WithMany(c => c.EscrowPayments)
            .HasForeignKey(e => e.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        // ======================
        // CHAT
        // ======================
        modelBuilder.Entity<ChatRoom>()
            .HasOne(cr => cr.Contract)
            .WithOne(c => c.ChatRoom)
            .HasForeignKey<ChatRoom>(cr => cr.ContractId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<ChatMessage>()
            .HasKey(cm => cm.MessageId);


        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.ChatRoom)
            .WithMany(cr => cr.Messages)
            .HasForeignKey(cm => cm.ChatRoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.SenderUser)
            .WithMany()
            .HasForeignKey(cm => cm.SenderUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // DISPUTES
        // ======================
        modelBuilder.Entity<Dispute>()
            .HasOne(d => d.Contract)
            .WithMany(c => c.Disputes)
            .HasForeignKey(d => d.ContractId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Dispute>()
            .HasOne(d => d.RaisedByUser)
            .WithMany()
            .HasForeignKey(d => d.RaisedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Dispute>()
            .HasOne(d => d.ResolvedByUser)
            .WithMany()
            .HasForeignKey(d => d.ResolvedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // ORDERS
        // ======================
        modelBuilder.Entity<Order>()
            .HasOne(o => o.BuyerUser)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.BuyerUserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // ======================
        // SYSTEM LOGS
        // ======================
        modelBuilder.Entity<SystemLog>()
            .HasOne(l => l.User)
            .WithMany()
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        
        // ======================
        // SEED DATA
        // ======================
        DbSeeder.Seed(modelBuilder);

    }
}
