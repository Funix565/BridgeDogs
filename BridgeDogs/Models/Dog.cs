using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BridgeDogs.Models
{
    public class Dog
    {
        // Let's make `Name` as primary key because it is required by EF.
        // And it helps handle dog with same name.
        [Key]
        [Column("name")]
        public string? Name { get; set; }

        [Column("color")]
        public string? Color { get; set; }

        [Column("tail_length")]
        [JsonPropertyName("tail_length")]
        public int TailLength { get; set; }

        [Column("weight")]
        public int Weight { get; set; }
    }
}
