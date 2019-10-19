using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dermayon.Common.Domain
{
    public abstract class EntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; protected set; }
        
        private DateTime? createdDate;
        [DataType(DataType.DateTime)]
        public virtual DateTime CreatedDate
        {
            get => createdDate ?? DateTime.UtcNow;
            set => createdDate = value;
        }

        [DataType(DataType.DateTime)]
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        
        [Timestamp]
        public virtual byte[] Version { get; set; }
        protected virtual object Actual => this;
        public override bool Equals(object obj)
        {
            var other = obj as EntityBase;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() == other.Actual.GetType())
                return true;

            if (Id == Guid.Empty || other.Id == null)
                return false;
            

            return Id == other.Id;
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b) => !(a == b);

        public override int GetHashCode() => (Actual.GetType().ToString() + Id).GetHashCode();
    }
}
