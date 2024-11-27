using System;

namespace LogansDBTest.Models;

public class UsersAndPasswords
{
 public int ID {get; set; } // primary key
 public string? Username { get; set; } // Allow Name to be null
 public string? PasswordHash { get; set; } // Allow Email to be null
 public DateTime Insert_TimeStamp { get; set; }
   public UsersAndPasswords()
      {
    }
}
