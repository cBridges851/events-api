namespace EventsAPI.Models {
    public class Event {
        public virtual Guid Id { get; set; } = Guid.NewGuid();
        public virtual string Name { get; set; } = string.Empty;
        public virtual string Description { get; set; } = string.Empty;
        public virtual EventType EventType { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public enum EventType { 
        Online,
        InPerson,
        Hybrid
    }
}
