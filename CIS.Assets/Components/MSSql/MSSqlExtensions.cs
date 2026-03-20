using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CIS.Assets.Components.MSSql;

public static class MSSqlExtensions
{
    public static EntityTypeBuilder<TEntity> AsTable<TEntity>(this EntityTypeBuilder<TEntity> builder, string schema) where TEntity : class
    {
        return builder.ToTable(typeof(TEntity).Name, schema);
    }

    public static EntityTypeBuilder<TEntity> IsPrimaryKey<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, object?>> keyExpression) where TEntity : class
    {
        builder.HasKey(keyExpression);
        return builder;
    }

    public static PropertyBuilder<string?> IsNvarchar<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, string?>> propertyExpression, int length) where TEntity : class
    {
        return builder.Property(propertyExpression).HasMaxLength(length).HasColumnType($"nvarchar({length})");
    }
    public static PropertyBuilder<string?> IsNvarcharMax<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, string?>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasColumnType("nvarchar(max)");
    }

    public static PropertyBuilder<TEnum> IsNvarcharEnum<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, TEnum>> propertyExpression, int length) where TEntity : class where TEnum : struct
    {
        return builder.Property(propertyExpression).HasConversion<string>().HasMaxLength(length).HasColumnType($"nvarchar({length})");
    }

    public static PropertyBuilder<long> IsLong2<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, long>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasColumnType("bigint");
    }

    public static PropertyBuilder<bool> IsBool<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, bool>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasColumnType("bit");
    }

    public static PropertyBuilder<DateOnly?> IsDateOnly<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, DateOnly?>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasColumnType("date");
    }

    public static PropertyBuilder<DateTime?> IsDateTime<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, DateTime?>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasColumnType("datetime2");
    }

    public static PropertyBuilder<DateTimeOffset> IsOffsetNow<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, DateTimeOffset>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression).HasDefaultValueSql("SYSDATETIMEOFFSET()");
    }

    public static PropertyBuilder<DateTimeOffset?> IsOffset<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, DateTimeOffset?>> propertyExpression) where TEntity : class
    {
        return builder.Property(propertyExpression);
    }

    public static PropertyBuilder<List<TEnum>> IsNvarcharJsonList<TEntity, TEnum>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, List<TEnum>>> propertyExpression, int length) where TEntity : class where TEnum : struct
    {
        return builder.Property(propertyExpression)
            .HasConversion(
                v => string.Join(",", v.Select(e => e.ToString())),
                v => v.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(e => System.Enum.Parse<TEnum>(e)).ToList()
            )
            .HasMaxLength(length)
            .HasColumnType($"nvarchar({length})");
    }

    public static PropertyBuilder<decimal?> IsDecimal<TEntity>(this EntityTypeBuilder<TEntity> builder, System.Linq.Expressions.Expression<Func<TEntity, decimal?>> propertyExpression, int precision, int scale) where TEntity : class
    {
        return builder.Property(propertyExpression).HasPrecision(precision, scale);
    }
}