using Balta.Domain.SharedContext.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balta.Domain.Test.Mocks
{
    public class MockYesterdayDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow.AddDays(-1);
    }
}
