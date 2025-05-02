using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifyTime { get; set; }
}
