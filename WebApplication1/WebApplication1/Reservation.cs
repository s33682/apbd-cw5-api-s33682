using System.ComponentModel.DataAnnotations;

namespace WebApplication1;

public class Reservation : IValidatableObject
{
    public int Id { get ; set; }
    public int RoomId { get; set; }
    [Required]
    public string OrganizerName { get; set; }
    [Required]
    public string Topic { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public string Status { get; set; }

    public Reservation( int id, int roomId, string organizerName, string topic, DateOnly date, TimeOnly startTime, TimeOnly endTime, string status )
    {
        this.Id = id;
        this.RoomId = roomId;
        this.OrganizerName = organizerName;
        this.Topic = topic;
        this.Date = date;
        this.StartTime = startTime;
        this.EndTime = endTime;
        this.Status = status;
    }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartTime >= EndTime)
        {
            yield return new ValidationResult("Koniec musi być późniejszy niż start.", new[] { nameof(EndTime) });
        }
    }
}