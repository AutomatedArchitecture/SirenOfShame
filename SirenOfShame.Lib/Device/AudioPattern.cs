using System;

namespace SirenOfShame.Lib.Device
{
    [Serializable]
    public class AudioPattern
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (AudioPattern)) return false;
            return Equals((AudioPattern) obj);
        }

        public bool Equals(AudioPattern other)
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