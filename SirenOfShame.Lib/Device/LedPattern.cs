using System;

namespace SirenOfShame.Lib.Device
{
    [Serializable]
    public class LedPattern
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LedPattern)) return false;
            return Equals((LedPattern) obj);
        }

        public bool Equals(LedPattern other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id && Equals(other.Name, Name);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id*397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}