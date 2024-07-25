namespace Proximity_Alert{
    public class Firebase_Storage_File{
        public string? file_name {get;set;}
    }
    public class Firebase_Database_Model:Dictionary<string, Firebase_Storage_File>{}
}