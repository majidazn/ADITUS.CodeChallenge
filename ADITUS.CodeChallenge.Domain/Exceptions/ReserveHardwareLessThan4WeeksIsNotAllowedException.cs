using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADITUS.CodeChallenge.Domain.Exceptions
{
  [Serializable]
  public class ReserveHardwareLessThan4WeeksIsNotAllowedException : Exception
  {
    public ReserveHardwareLessThan4WeeksIsNotAllowedException()
    { }

    public ReserveHardwareLessThan4WeeksIsNotAllowedException(string message)
        : base(message)
    { }

    public ReserveHardwareLessThan4WeeksIsNotAllowedException(string message, Exception innerException)
        : base(message, innerException)
    { }
  }
}
