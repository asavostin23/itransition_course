using Course.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionField> CollectionFields { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<IntegerItemField> IntegerItemFields { get; set; }
    public DbSet<StringItemField> StringItemFields { get; set; }
    public DbSet<TextItemField> TextItemFields { get; set; }
    public DbSet<BoolItemField> BoolItemFields { get; set; }
    public DbSet<DatetimeItemField> DatetimeItemFields { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>(UserConfigure);
        builder.Entity<Collection>(CollectionConfigure);
        builder.Entity<CollectionField>(CollectionFieldConfigure);
        builder.Entity<Item>(ItemConfigure);
        builder.Entity<Comment>(CommentConfigure);
        builder.Entity<Tag>(TagConfigure);
        builder.Entity<IntegerItemField>(IntegerItemFieldConfigure);
        builder.Entity<StringItemField>(StringItemFieldConfigure);
        builder.Entity<TextItemField>(TextItemFieldConfigure);
        builder.Entity<BoolItemField>(BoolItemFieldConfigure);
        builder.Entity<DatetimeItemField>(DatetimeItemFieldConfigure);
    }
    public void UserConfigure(EntityTypeBuilder<User> builder)
    {
        builder.HasAlternateKey(user => user.UserName);
        builder.Property(user => user.Active).HasDefaultValue(true);
        builder.Property(user => user.Language).HasDefaultValue("en");
        builder.Property(user => user.Theme).HasDefaultValue("light");
        builder.HasMany(user => user.Comments).WithOne(comment => comment.User).HasForeignKey(comment => comment.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(user => user.Items).WithOne(item => item.User).HasForeignKey(item => item.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(user => user.Collections).WithOne(collection => collection.User).HasForeignKey(collection => collection.UserId);
        builder.HasMany(user => user.LikedItems).WithMany(item => item.LikedUsers)
            .UsingEntity<Like>(
            like => like.HasOne(l => l.Item).WithMany(i => i.Likes).HasForeignKey(l => l.ItemId).OnDelete(DeleteBehavior.NoAction),
            like => like.HasOne(l => l.User).WithMany(u => u.Likes).HasForeignKey(l => l.UserId),
            like => 
            { 
                like.HasKey(l => new { l.UserId, l.ItemId });
                like.ToTable("Likes");
            });
    }
    public void CollectionConfigure(EntityTypeBuilder<Collection> builder)
    {

    }
    public void CollectionFieldConfigure(EntityTypeBuilder<CollectionField> builder)
    {
        builder.HasMany(cf => cf.BoolItemFields).WithOne(BoolItemField => BoolItemField.CollectionField).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(cf => cf.IntegerItemFields).WithOne(IntegerItemField => IntegerItemField.CollectionField).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(cf => cf.DatetimeItemFields).WithOne(DatetimeItemField => DatetimeItemField.CollectionField).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(cf => cf.StringItemFields).WithOne(StringItemField => StringItemField.CollectionField).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(cf => cf.TextItemFields).WithOne(TextItemField => TextItemField.CollectionField).OnDelete(DeleteBehavior.NoAction);
    }
    public void ItemConfigure(EntityTypeBuilder<Item> builder)
    {

    }
    public void CommentConfigure(EntityTypeBuilder<Comment> builder)
    {

    }
    public void TagConfigure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasAlternateKey(x => x.Name);
    }
    public void IntegerItemFieldConfigure(EntityTypeBuilder<IntegerItemField> builder)
    {

    }
    public void StringItemFieldConfigure(EntityTypeBuilder<StringItemField> builder)
    {
        builder.Property(p => p.Value).HasMaxLength(140);
    }
    public void TextItemFieldConfigure(EntityTypeBuilder<TextItemField> builder)
    {

    }
    public void BoolItemFieldConfigure(EntityTypeBuilder<BoolItemField> builder)
    {

    }
    public void DatetimeItemFieldConfigure(EntityTypeBuilder<DatetimeItemField> builder)
    {

    }
}
