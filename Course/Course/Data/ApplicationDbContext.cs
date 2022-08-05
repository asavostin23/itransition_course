using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Course.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
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
        builder.HasAlternateKey(x => x.UserName);
        builder.Property(p => p.Active).HasDefaultValue(true);
        builder.HasMany(user => user.Comments).WithOne(comment => comment.User).HasForeignKey(comment => comment.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(user => user.Items).WithOne(item => item.User).HasForeignKey(item => item.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(user => user.Collections).WithOne(collection => collection.User).HasForeignKey(collection => collection.UserId);
    }
    public void CollectionConfigure(EntityTypeBuilder<Collection> builder)
    {

    }
    public void CollectionFieldConfigure(EntityTypeBuilder<CollectionField> builder)
    {

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
