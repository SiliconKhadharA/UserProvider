
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Data.Entities;

public class UserEntity : IdentityUser
{


    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Bio { get; set; }

    public string? ProfileImage { get; set; } = "avatar1.jpg";

    public bool IsExtrernal { get; set; }

    public ICollection<AddressEntity> Addresses { get; set; } = [];

}

public class AddressEntity
{
    [Key]
    public int Id { get; set; }


    public string UserId { get; set; } = null!;

    public UserEntity User { get; set; } = null!;

    public string AddressLine_1 { get; set; } = null!;

    public string? AddressLine_2 { get; set; }


    public string PostalCode { get; set; } = null!;


    public string City { get; set; } = null!;

}
