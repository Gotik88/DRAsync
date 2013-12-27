using System;
using System.ServiceModel.DomainServices.Client;

namespace DR.Async.Task.Helpers.Exceptions
{
    public class KnownOperationException : OperationException
    {
        public KnownOperationException(Exception innerException, OperationBase operation)
            : base(innerException, operation)
        {
        }

        public KnownOperationException(
            Exception innerException, string additionalMessage, OperationBase operation)
            : base(innerException, additionalMessage, operation)
        {
        }
    }
}
