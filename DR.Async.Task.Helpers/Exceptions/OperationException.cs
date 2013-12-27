using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using DR.Async.Task.Helpers.Exceptions;

namespace DR.Async.Task.Helpers.Exceptions
{
    /// <summary>
    /// Exception of RIA services operation
    /// </summary>
    public class OperationException : Exception
    {
        public OperationException(Exception innerException, OperationBase operation)
            : base(innerException.Message, innerException)
        {
            Operation = operation;
        }

        public OperationException(Exception innerException, string additionalMessage, OperationBase operation)
            : base(innerException.Message + ' ' + additionalMessage, innerException)
        {
            Operation = operation;
        }

        /// <summary>
        /// Gets the RIA services operation.
        /// </summary>
        public OperationBase Operation { get; private set; }

        public static OperationException CreateFromOperation(OperationBase operation)
        {
            return null;
            /* return operation.IsKnownError()
                ? new KnownOperationException(operation.Error, operation)
                : new OperationException(operation.Error, operation);*/
        }
    }
}
