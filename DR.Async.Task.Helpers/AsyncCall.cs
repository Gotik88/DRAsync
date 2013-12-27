using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DR.Async.Task.Helpers.Exceptions;
using System.Runtime.CompilerServices;
using Tasks = System.Threading.Tasks;
using System.Threading;

namespace DR.Async.Task.Helpers
{
    public class AsyncCall
    {
        private readonly Func<Tasks.Task> _asyncLambda;

        private bool _isModal;

        private string _message;

        private Func<KnownOperationException, bool> _knownOperationExceptionHandler;

        private Func<OperationException, bool> _operationExceptionHandler;

        internal AsyncCall(Func<Tasks.Task> asyncLambda)
        {
            _asyncLambda = asyncLambda;
        }

        public AsyncCall Modal(string message)
        {
            if (_isModal)
            {
                throw new InvalidOperationException();
            }

            _isModal = true;
            _message = message;
            return this;
        }

        public AsyncCall CatchKnownOperationException(Func<KnownOperationException, bool> handler)
        {
            if (_knownOperationExceptionHandler != null)
            {
                throw new InvalidOperationException();
            }

            _knownOperationExceptionHandler = handler;
            return this;
        }

        public AsyncCall CatchOperationException(Func<OperationException, bool> handler)
        {
            if (_operationExceptionHandler != null)
            {
                throw new InvalidOperationException();
            }

            _operationExceptionHandler = handler;
            return this;
        }

        public void Call(
            Action<bool> doneCallback = null,
            Action succeeded = null,
            Action failed = null,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            /* var sourceFileInfo = SourceFileInformationUtils.Format(filePath, lineNumber);
             LogManager<AsyncCall>.Logger.Info(
                 "Async operation is beign started ({0})".Fmt(sourceFileInfo));*/

            var task = _asyncLambda();
            if (_isModal)
            {
                task = task.Modal(_message, filePath, lineNumber);
            }

            task.ContinueWith(
                    t =>
                    {
                        try
                        {
                            t.Exception.With(e => e.InnerException).DoTypeSwitch(
                                (KnownOperationException ex) =>
                                {
                                    var handled = _knownOperationExceptionHandler.Return(
                                        h => h(ex), (bool?)null);

                                    if (handled.GetValueOrDefault(false))
                                    {
                                        return;
                                    }

                                    if (!handled.HasValue)
                                    {
                                        handled = _operationExceptionHandler.Return(
                                            h => h(ex), false);

                                        if (handled.GetValueOrDefault(false))
                                        {
                                            return;
                                        }
                                    }

                                    //ex.Operation.ShowErrorMessage();
                                },
                                (OperationException ex) =>
                                {
                                    var handled = _operationExceptionHandler.Return(h => h(ex), false);
                                    if (handled)
                                    {
                                        return;
                                    }

                                    ex.RethrowOperationException(filePath, lineNumber);
                                },
                                ex => t.Exception.CheckExceptionAndWrap(filePath, lineNumber));
                        }
                        finally
                        {
                            if (t.Exception == null)
                            {
                                succeeded.Do(c => c());
                            }
                            else
                            {
                                failed.Do(c => c());
                            }

                            doneCallback.Do(c => c(t.Exception == null));

                            /* LogManager<AsyncCall>.Logger.Info(
                                 "Async operation is finished ({0})".Fmt(sourceFileInfo));*/
                        }
                    },
                    CancellationToken.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext())
                .HandleExceptions(filePath, lineNumber);
        }
    }
}
