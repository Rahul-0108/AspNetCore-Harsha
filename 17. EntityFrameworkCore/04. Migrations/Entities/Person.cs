using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
  /// <summary>
  /// Person domain model class
  /// </summary>
  public class Person
  {
    [Key] // primary key
    public Guid PersonID { get; set; }

    // if non nullable then initialize with ""
    [StringLength(40)] //nvarchar(40)
    public string? PersonName { get; set; }

    [StringLength(40)] //nvarchar(40)
    public string? Email { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [StringLength(10)] //nvarchar(100)
    public string? Gender { get; set; }

    //uniqueidentifier
    public Guid? CountryID { get; set; }

    [StringLength(200)] //nvarchar(200)
    public string? Address { get; set; }

    //bit // 0 or 1
    public bool ReceiveNewsLetters { get; set; }
  }
}
