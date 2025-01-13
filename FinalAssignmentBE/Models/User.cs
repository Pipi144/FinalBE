using System.ComponentModel.DataAnnotations;

namespace FinalAssignmentBE.Models;

public class User
{
    [Key]
    public int UserId {get; set;}
    public string Username {get; set;}
    public string  Password {get; set;}
    public List<Game> Games {get; set;}
}