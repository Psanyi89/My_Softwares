using System;

namespace PersonEntities
{
    public interface IPerson
    {
        DateTime DateOfBirth { get; set; }
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }

        string ToString();
    }
}