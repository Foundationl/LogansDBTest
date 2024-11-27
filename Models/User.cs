namespace LogansDBTest.Models 
{
    public class User
{
    public int ID {get; set; } // primary key
    public string? Name { get; set; } // Allow Name to be null
    public string? Email { get; set; } // Allow Email to be null
    public DateTime Insert_TimeStamp { get; set; }
   public User()
   {

   }
}
}