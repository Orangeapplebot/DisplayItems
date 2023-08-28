using System;
using System.ComponentModel.DataAnnotations;

namespace DisplayItems.Models
{
    public class Log
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public LogType LogType { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }

    public enum LogType
    {
        Bug, Error, Event
    }

    public class LocalDB
    {
        public List<Log> dbInstance = new List<Log>();

//      public List<Log> Get() { return dbInstance; }

//      public List<Log> Set(List<Log> updatedDB) { dbInstance = updatedDB; }
    }
}
