namespace Proximity_Alert{
    public class ProximityEvent{
        public DateTime proximity {get; set;}
    }

    public class ProximityEvents:Dictionary<string, DateTime>{
    }
}