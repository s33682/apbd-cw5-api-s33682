namespace WebApplication1;

public static class Database
{
    public static List<Room> Rooms { get; set; } = new List<Room>
    {
        new Room(1, "Lab 201", "A", 2, 15, true, true),
        new Room(2, "Lab 204", "B", 2, 24, true, true),
        new Room(3, "Sala Wykładowa 1", "A", 0, 100, true, true),
        new Room(4, "Pokój Konsultacyjny", "C", 1, 4, false, true),
        new Room(5, "Magazyn", "A", -1, 0, false, false)
    };

    public static List<Reservation> Reservations { get; set; } = new List<Reservation>
    {
        new Reservation(1, 1, "Jan Kowalski", "Wstęp do C#", new DateOnly(2026, 5, 10), new TimeOnly(8, 0, 0), new TimeOnly(10, 0, 0), "confirmed"),
        new Reservation(2, 2, "Anna Nowak", "Warsztaty REST API", new DateOnly(2026, 5, 10), new TimeOnly(10, 0, 0), new TimeOnly(12, 30, 0), "planned"),
        new Reservation(3, 3, "Piotr Wiśniewski", "Wykład gościnny", new DateOnly(2026, 5, 11), new TimeOnly(14, 0, 0), new TimeOnly(16, 0, 0), "confirmed"),
        new Reservation(4, 4, "Jan Kowalski", "Konsultacje projektowe", new DateOnly(2026, 5, 12), new TimeOnly(9, 0, 0), new TimeOnly(10, 0, 0), "cancelled")
    };
}