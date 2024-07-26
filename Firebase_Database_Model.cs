namespace Proximity_Alert{
    public class Firebase_Alert{
        public string? alert_name {get;set;}
    }
    public class Firebase_Database_Model:Dictionary<string, Firebase_Alert>{}
}