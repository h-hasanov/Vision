namespace HH.TestUtils
{
    public class Person : IPerson
    {
        public int Age { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
    }
}
