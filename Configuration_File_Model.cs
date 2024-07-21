namespace Proximity_Alert{
    public class Configuration_File_Model{
        public string? api_key {get;set;}
        public string? user_email {get;set;}
        public string? user_password {get;set;}
        public int proximity_alert_expiration {get;set;} = 10;
    }
}