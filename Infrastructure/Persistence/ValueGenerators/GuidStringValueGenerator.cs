using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.ValueGenerators
{
    internal class GuidStringValueGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
