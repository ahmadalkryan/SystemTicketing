using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLyer.Common
{
    public class BaseEntity : IEquatable<BaseEntity>
    { 
        public int Id { get; set; } 
        public bool Equals(BaseEntity? other)
        {
            throw new NotImplementedException();
        }
    }
}
