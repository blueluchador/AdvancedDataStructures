namespace AdvancedDataStructures.Tests.Trees;

public class ComparableSkipListTests
{
    private class MyObject : IComparable<MyObject>, IEquatable<MyObject>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CompareTo(MyObject other)
        {
            if (other == null)
                return 1;

            return Id.CompareTo(other.Id);
        }

        public bool Equals(MyObject other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is MyObject otherEntity)
            {
                return Equals(otherEntity);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }
}