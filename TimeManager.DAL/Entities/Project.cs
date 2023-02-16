using System.ComponentModel.DataAnnotations;

namespace TimeManager.DAL.Entities
{
    public class Project
    {
        [Key]
        public Guid Id { get; set; }
        public string ProjectName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
