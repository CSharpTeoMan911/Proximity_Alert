namespace Proximity_Alert{
    public class Configuration_File_Model{
        public string? api_key {get;set;}
        public string? user_email {get;set;}
        public string? user_password {get;set;}
        public string? firebase_storage_bucket_url {get;set;}
        public string? firebase_auth_domain {get;set;}
        public string? firebase_database_url {get;set;}

        // E.g. (Delete expired alerts starting 10 days ago and ending 20 days ago ) 
        public int proximity_alert_expiration_start {get;set;} = 10;
        public int proximity_alert_expiration_end {get;set;} = 20;
    }
}