using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
    // [GraphQLDescription("Some desciprion of Platform Model")]
    public class Platform
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        // [GraphQLDescription("valid License of Platform Model")]
        public string LicenseKey { get; set; }

        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}