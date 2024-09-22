using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADITUS.CodeChallenge.Domain.Exceptions
{
  public class NotSufficientHardwareException :Exception
  {
    public NotSufficientHardwareException()
    { }

    public NotSufficientHardwareException(string message)
        : base(message)
    { }

    public NotSufficientHardwareException(string message, Exception innerException)
        : base(message, innerException)
    { }
  }
}
