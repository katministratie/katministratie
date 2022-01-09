using System.ComponentModel.DataAnnotations;

public class SuperkatDto
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}