using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;


[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllReservations( DateOnly? date, string? status, int? roomId)
    {
        return Ok(Database.Reservations.Where(r => ( date == null || r.Date >= date) 
                                            && (status == null || r.Status == status) 
                                            && (roomId == null || r.RoomId == roomId)));
    }

    [HttpGet("{id}")]
    public IActionResult GetReservation(int id)
    {
        Reservation reservation = Database.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }
        
        return Ok(reservation);
    }

    [HttpPost]
    public IActionResult CreateReservation([FromBody] Reservation reservation)
    {
        Room room = Database.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        
        if (room == null)
        {
            return NotFound("Sala nie istnieje!");
        }

        if (!room.IsActive)
        {
            return BadRequest("Sala jest nieaktywna!");
        }

        if (Database.Reservations.Any(r =>
                r.RoomId == reservation.RoomId && 
                r.Date == reservation.Date &&
                reservation.StartTime < r.EndTime && 
                reservation.EndTime > r.StartTime))
        {
            return Conflict("Sala jest zajęta w tym terminie!");
        }

        reservation.Id = Database.Reservations.Max(r => r.Id) + 1;
        Database.Reservations.Add(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReservation(int id, [FromBody] Reservation reservation)
    {
        Reservation toEdit = Database.Reservations.FirstOrDefault(r => r.Id == id);

        if (toEdit == null)
        {
            return  NotFound(); 
        }
        
        Room room = Database.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        
        if (room == null)
        {
            return NotFound("Sala nie istnieje!");
        }

        if (!room.IsActive)
        {
            return BadRequest("Sala jest nieaktywna!");
        }

        if (Database.Reservations.Any(r =>
                r.Id != id &&
                r.RoomId == reservation.RoomId && 
                r.Date == reservation.Date &&
                reservation.StartTime < r.EndTime && 
                reservation.EndTime > r.StartTime))
        {
            return Conflict("Sala jest zajęta w tym terminie!");
        }
        
        toEdit.RoomId = reservation.RoomId;
        toEdit.OrganizerName = reservation.OrganizerName;
        toEdit.Topic = reservation.Topic;
        toEdit.Date = reservation.Date;
        toEdit.StartTime = reservation.StartTime;
        toEdit.EndTime = reservation.EndTime;
        toEdit.Status = reservation.Status;
        
        return Ok(toEdit);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReservation(int id)
    {
        Reservation toDelete = Database.Reservations.FirstOrDefault(r => r.Id == id);

        if (toDelete == null)
        {
            return NotFound();
        }
        
        Database.Reservations.Remove(toDelete);
        return NoContent();
    }
}