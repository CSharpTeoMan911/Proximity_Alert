namespace Proximity_Alert{
    public class Configuration_File_Model{
        public string? api_key {get;set;}
        public string? user_email {get;set;}
        public string? user_password {get;set;}
        public string? firebase_auth_domain {get;set;}
        public string? firebase_database_url {get;set;}

        // E.g. (Delete expired alerts starting 10 days ago) 
        public int alert_expiration_days {get;set;} = 10;

        // Interval in minutes to check for proximity alerts
        public int notification_period_minutes {get;set;} = 5;

        public int distance_cm = 50;
    }
}