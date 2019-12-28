namespace HH.TestUtils
{
    public interface IPerson
    {
        int Age { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string LastName { get; set; }
        Gender Gender { get; set; }
    }
}