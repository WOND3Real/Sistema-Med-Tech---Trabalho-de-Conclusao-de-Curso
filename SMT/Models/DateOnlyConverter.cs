using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
{
    public DateOnlyConverter() 
        : base(
            dateOnly => DateTime.SpecifyKind(dateOnly.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc), // Converte DateOnly para DateTime com UTC
            dateTime => DateOnly.FromDateTime(dateTime) // Converte DateTime para DateOnly
        )
    {
    }
}
