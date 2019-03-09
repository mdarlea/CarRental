using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace CarRental.Identity.Data.ValueObjects
{
	[Owned]
    public class Name
    {
		#region equality
		protected bool Equals(Name other)
		{
			return string.Equals(FirstName, other.FirstName) && string.Equals(LastName, other.LastName) && string.Equals(MiddleName, other.MiddleName);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((Name)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = (FirstName != null ? FirstName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (LastName != null ? LastName.GetHashCode() : 0);
				hashCode = (hashCode * 397) ^ (MiddleName != null ? MiddleName.GetHashCode() : 0);
				return hashCode;
			}
		}

		#endregion equality

		public Name(string firstName, string lastName, string middleName = null)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrEmpty(lastName)) throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        private Name() { } //required for EF

        [Required]
        [MaxLength(150)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; private set; }

		[MaxLength(150)]
		public string MiddleName { get; private set; }
    }
}
