using Microsoft.AspNetCore.Mvc;

namespace WebApplication1;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAllRooms(int? minCapacity, bool? hasProjector, bool? activeOnly)
    {
        return Ok(Database.Rooms.Where(r => ( minCapacity == null || r.Capacity >= minCapacity) 
                                            && (hasProjector == null || r.HasProjector == hasProjector) 
                                            && (activeOnly == null || r.IsActive == activeOnly)));
    }
    
    [HttpGet("{id}")]
    public IActionResult GetRoom(int id)
    {
        Room room = Database.Rooms.FirstOrDefault(x => x.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        return Ok(room);
    }

    [HttpGet("building/{buildingCode}")]
    public IActionResult GetBuilding(string buildingCode)
    {
        return Ok(Database.Rooms.Where(room => room.BuildingCode == buildingCode));
    }

    [HttpPost]
    public IActionResult CreateRoom([FromBody] Room room)
    {
        room.Id = Database.Rooms.Max(x => x.Id) + 1;
        Database.Rooms.Add(room);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateRoom(int id, [FromBody] Room room)
    {
        Room toEdit = Database.Rooms.FirstOrDefault(r => r.Id == id);
        
        if (toEdit == null)
        {
            return NotFound();
        }
        
        toEdit.Name = room.Name;
        toEdit.BuildingCode = room.BuildingCode;
        toEdit.Floor = room.Floor;
        toEdit.Capacity = room.Capacity;
        toEdit.HasProjector = room.HasProjector;
        toEdit.IsActive = room.IsActive;
        return Ok(toEdit);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteRoom(int id)
    {
        Room toDelete = Database.Rooms.FirstOrDefault(r => r.Id == id);

        if (toDelete == null)
        {
            return NotFound();
        }

        if (Database.Reservations.Any(r => r.RoomId == id))
        {
            return Conflict("Istnieja rezerwacje dla tej sali!");
        }

        Database.Rooms.Remove(toDelete);
        return NoContent();
    }
}