using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tasks = System.Threading.Tasks;

namespace DR.Async.Task.Helpers.Exceptions
{
    public static class TaskExtensions
    {
        public static Tasks.Task<T> Safe<T>(
            this Tasks.Task<T> task,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            return task.ContinueWith(
                t =>
                {
                    t.Exception.With(e => e.InnerException).DoTypeSwitch(
                        (KnownOperationException ex) =>
                        {
                            throw new KnownOperationException(
                                ex, GetMessageInBrackets(filePath, lineNumber), ex.Operation);
                        },
                        (OperationException ex) =>
                        {
                            throw new OperationException(
                                ex, GetMessageInBrackets(filePath, lineNumber), ex.Operation);
                        },
                        ex => t.Exception.CheckExceptionAndWrap(filePath, lineNumber));

                    return t.Result;
                },
                CancellationToken.None,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Tasks.Task Safe(
            this Tasks.Task task,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            return task.ContinueWith(
                t =>
                {
                    t.Exception.With(e => e.InnerException).DoTypeSwitch(
                        (KnownOperationException ex) =>
                        {
                            throw new KnownOperationException(
                                ex, GetMessageInBrackets(filePath, lineNumber), ex.Operation);
                        },
                        (OperationException ex) => RethrowOperationException(ex, filePath, lineNumber),
                        ex => t.Exception.CheckExceptionAndWrap(filePath, lineNumber));
                },
                CancellationToken.None,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void HandleExceptions(
            this Tasks.Task task,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            task.ContinueWith(t => t.Exception.CheckExceptionAndWrap(filePath, lineNumber),
                /*t => Deployment.Current.Dispatcher.BeginInvoke(
                    () => t.Exception.CheckExceptionAndWrap(filePath, lineNumber)),*/
                CancellationToken.None,
                TaskContinuationOptions.OnlyOnFaulted,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Task<T> Modal<T>(
            this Task<T> task,
            string message,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            var id = Guid.NewGuid();
            ShowBusyIndicator(id, message, true, filePath, lineNumber);

            return task.ContinueWith(
                t =>
                {
                    ShowBusyIndicator(id, message, false, filePath, lineNumber);
                    return t.Result;
                },
                CancellationToken.None,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static Tasks.Task Modal(
            this Tasks.Task task,
            string message,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            var id = Guid.NewGuid();
            ShowBusyIndicator(id, message, true, filePath, lineNumber);

            return task.ContinueWith(
                t =>
                {
                    ShowBusyIndicator(id, message, false, filePath, lineNumber);
                    try
                    {
                        t.Wait();
                    }
                    catch (AggregateException ex)
                    {
                        throw ex.Flatten().InnerException;
                    }
                },
                CancellationToken.None,
                TaskContinuationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }

        public static void CheckExceptionAndWrap(
            this Exception exception,
            [CallerFilePath] string filePath = null,
            [CallerLineNumber] int lineNumber = 0)
        {
            if (exception == null)
            {
                return;
            }

            throw new TargetInvocationException(GetMessage(filePath, lineNumber), exception);
        }

        internal static void RethrowOperationException(
            this OperationException ex, string filePath, int lineNumber)
        {
            throw new OperationException(
                ex, GetMessageInBrackets(filePath, lineNumber), ex.Operation);
        }

        private static string GetMessageInBrackets(string filePath, int lineNumber)
        {
            return "(" + GetMessage(filePath, lineNumber) + ")";
        }

        private static string GetMessage(string filePath, int lineNumber)
        {
            return filePath == null
                ? "N/A"
                : "SourceFileInformationUtils.Format(filePath, lineNumber)";
        }

        private static void ShowBusyIndicator(
            Guid eventIdentifier,
            string message,
            bool isStart,
            string callerFilePath,
            int callerLineNumber)
        {
            /*ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<BusyEvent>().Publish(
                new BusyEventArgs(callerFilePath, callerLineNumber)
                {
                    CorrelationToken = eventIdentifier,
                    IsStart = isStart,
                    Message = message
                });*/
        }
    }
}
