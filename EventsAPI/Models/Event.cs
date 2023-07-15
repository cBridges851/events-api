namespace EventsAPI.Models {
    public class Event {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public EventType EventType { get; set; }
        public DateTime Date { get; set; }
    }

    public enum EventType { 
        Online,
        InPerson,
        Hybrid
    }
}
