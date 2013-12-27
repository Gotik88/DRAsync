using System;
using System.ServiceModel.DomainServices.Client;

namespace DR.Async.Task.Helpers.Exceptions
{
    public static class LoadOperationExtensions
    {
        /// <summary>
        /// Code of unauthorized access error
        /// </summary>
        private const int UnauthorizedErrorCode = 401;

        /// <summary>
        /// Payload code
        /// </summary>
        private const int PayloadCode = 424242;

        /// <summary>
        /// The server not found error template
        /// </summary>
        private const string ServerNotFoundErrorTemplate = "NotFound";

        /// <summary>
        /// Key for resource Common.Error.Title
        /// </summary>
        private const string CommonErrorTitleKey = "Common.Error.Title";

        /// <summary>
        /// Key for resource ConcurrencyErrorTitleKey
        /// </summary>
        private const string ConcurrencyErrorTitleKey = "Common.Messages.Error.Concurrency.Title";

        /// <summary>
        /// Key for resource Common.Messages.Error.Concurrency.Message
        /// </summary>
        private const string ConcurrencyMessageTemplateKey = "Common.Messages.Error.Concurrency.Message";

        /// <summary>
        /// Key for resource Common.Messages.Error.Timeout.Title
        /// </summary>
        private const string TimeoutErrorTitleKey = "Common.Messages.Error.Timeout.Title";

        /// <summary>
        /// Key for resource Common.Messages.Error.Timeout.Message
        /// </summary>
        private const string TimeoutMessageTemplateKey = "Common.Messages.Error.Timeout.Message";

        /// <summary>
        /// Key for resource Common.Messages.Error.Timeout.Title
        /// </summary>
        private const string NotEnoughPermissionsTitleKey = "Framework.Error.Operation.NotEnoughPermissions.Title";

        /// <summary>
        /// Key for resource Common.Messages.Error.Timeout.Message
        /// </summary>
        private const string NotEnoughPermissionsTemplateKey = "Framework.Error.Operation.NotEnoughPermissions.Message";

        /// <summary>
        /// Key for resource Common.Error.Title
        /// </summary>
        private const string CommonValidationFailedTitleKey = "Common.Error.Title";

        /// <summary>
        /// Key for resource Common.ValidationFailed.Message.Template
        /// </summary>
        private const string CommonValidationFailedMessageTemplateKey = "Common.ValidationFailed.Message.Template";

        /// <summary>
        /// Key for resource Common.Error.Title
        /// </summary>
        private const string EntityNotFoundTitleKey = "Framework.Error.EntityNotFound.Title";

        /// <summary>
        /// Key for resource Common.ValidationFailed.Message.Template
        /// </summary>
        private const string EntityNotFoundTemplateKey = "Framework.Error.EntityNotFound.Message";

        /// <summary>
        /// The entity name template key
        /// </summary>
        private const string EntityNameTemplateKey = "Common.Messages.Error.Concurrency.{0}";

        /// <summary>
        /// Key for resource Common.Error.Title
        /// </summary>
        private const string SqlErrorTitleKey = "Framework.Error.SqlError.Title";

        /// <summary>
        /// Key for resource Common.Error.Title
        /// </summary>
        private const string OperationTimeoutTitleKey = "Framework.Error.TimeOut.Title";

        /// <summary>
        /// Key for resource Common.ValidationFailed.Message.Template
        /// </summary>
        private const string OperationTimeoutMessage = "Framework.Error.TimeOut.Message";

        /// <summary>
        /// The message exceeded message.
        /// </summary>
        private const string MessageExceededMessage = "Framework.Error.MessageExceeded.Message";

        /// <summary>
        /// Key for resource Framework.Error.ServerNotFound.Message
        /// </summary>
        private const string OperationServerNotFoundMessage = "Framework.Error.ServerNotFound.Message";

        /// <summary>
        /// The message exceeded error template.
        /// </summary>
        private static readonly string[] MessageExceededErrorTemplate = new[] { "[Arg_COMException]", "COM" };

        /// <summary>
        /// Mark an error as handled if error is not unauthorized access exception.
        /// </summary>
        /// <param name="operationBase">The operation.</param>
        public static void SecureMarkErrorAsHandled(this OperationBase operationBase)
        {
            /*LogManager.GetLog(operationBase.GetType()).Error(operationBase.Error);

            if (ServiceLocator.Current.GetInstance<IApplicationSettingsService>().HandleAllUnhandledErrors)
            {
                if (!IsAuthorizationError(operationBase))
                {
                    operationBase.MarkErrorAsHandled();
                }
            }*/
        }
        /*
                /// <summary>
                /// Shows the error message.
                /// </summary>
                /// <param name="operationBase">The operation base.</param>
                /// <param name="title">The title.</param>
                /// <param name="buttons">The buttons.</param>
                /// <param name="icons">The icons.</param>
                /// <param name="confirmed">The confirmed.</param>
                /// <param name="declined">The declined.</param>
                /// <param name="canceled">The canceled.</param>
                /// <param name="showValidationErrors">Show validation errors.</param>
                /// <param name="filePath">Source file path.</param>
                /// <param name="lineNumber">Source file line number.</param>
                /// <returns><code>True</code> error is handled.</returns>
                public static bool ShowErrorMessage(
                    this OperationBase operationBase,
                    string title = null,
                    PopupButtons buttons = PopupButtons.OK,
                    ConfirmationIcons? icons = null,
                    Action confirmed = null,
                    Action declined = null,
                    Action canceled = null,
                    bool showValidationErrors = true,
                    [CallerFilePath] string filePath = null,
                    [CallerLineNumber] int lineNumber = 0)
                {
                    var message = operationBase.GetErrorTitleAndMessage(showValidationErrors);
                    if (message != null)
                    {
                        var confirmService = ServiceLocator.Current.GetInstance<IConfirmationService>();
                        confirmService.Show(
                            message.Item2,
                            title ?? message.Item1,
                            buttons,
                            icons ?? (!message.Item3 ? ConfirmationIcons.Error : ConfirmationIcons.Warning),
                            maxWidth: 600,
                            maxHeight: 250,
                            confirmed: confirmed,
                            declined: declined,
                            canceled: canceled,
                            callerFilePath: filePath,
                            callerLineNumber: lineNumber);

                        return true;
                    }

                    return false;
                }
                */
        /// <summary>
        /// Determines whether payload exists in the specified operation.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>
        ///   <c>true</c> if payload exists in the specified operation base; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPayloadExists(this OperationBase operationBase)
        {
            if (operationBase.HasError)
            {
                var errorCode = 0;
                var domainOperationException = operationBase.Error as DomainOperationException;
                var domainException = operationBase.Error as DomainException;

                if (domainOperationException != null)
                {
                    errorCode = domainOperationException.ErrorCode;
                }
                else if (domainException != null)
                {
                    errorCode = domainException.ErrorCode;
                }

                return errorCode == PayloadCode;
            }

            return false;
        }

        /// <summary>
        /// Gets the payload data.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>The payload data</returns>
        public static object GetPayloadData(this OperationBase operationBase)
        {
            /*try
            {
                if (operationBase.HasError)
                {
                    return JsonConvert.DeserializeObject(operationBase.Error.Message);
                }
            }
            catch (JsonSerializationException e)
            {
                LogManager.GetLog(operationBase.GetType()).Error(e, "Unable get payload");
            }*/

            return null;
        }

        /// <summary>
        /// Gets the payload data.
        /// </summary>
        /// <typeparam name="TPayload">The type of the payload.</typeparam>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>The payload data</returns>
        public static TPayload GetPayloadData<TPayload>(this OperationBase operationBase)
        {
            /* try
             {
                 if (operationBase.HasError)
                 {
                     return (TPayload)
                         JsonConvert.DeserializeObject(
                             operationBase.Error.Message,
                             typeof(TPayload),
                             new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Error });
                 }
             }
             catch (JsonSerializationException e)
             {
                 LogManager.GetLog(operationBase.GetType()).Error(e, "Unable get payload");
             }*/

            return default(TPayload);
        }

        /// <summary>
        /// Determines whether is known error in the specified operation.
        /// </summary>
        /// <param name="operationBase">The operating.</param>
        /// <returns>
        ///   <c>true</c> if is known error in the specified operation; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKnownError(this OperationBase operationBase)
        {
            /*return (operationBase.IsPayloadExists() && operationBase.GetPayloadData<ServiceOperationResult>() != null) ||
                IsTimeOutError(operationBase) ||
                IsMessageExceededError(operationBase) ||
                IsServerNotFoundError(operationBase) ||
                GetValidationErrors(operationBase).Any();*/
            return true;
        }

        /// <summary>
        /// Determines whether is security error in the specified operation.
        /// </summary>
        /// <param name="operationBase">The operating.</param>
        /// <returns>
        ///   <c>true</c> if error is security error in the specified operation; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSecurityError(this OperationBase operationBase)
        {
            return true;
            /* return
                 operationBase.IsPayloadExists()
                 && operationBase.GetPayloadData<ServiceOperationResult>() != null
                 && operationBase.GetPayloadData<ServiceOperationResult>().Message == "SecurityException";*/
        }

        /* /// <summary>
         /// Gets the error title and message.
         /// </summary>
         /// <param name="operationBase">The operation base.</param>
         /// <param name="showValidationErrors">if set to <c>true</c> [show validation errors].</param>
         /// <returns>Title, message, is warning</returns>
         public static Tuple<string, string, bool> GetErrorTitleAndMessage(
             this OperationBase operationBase, bool showValidationErrors = true)
         {
             if (IsAuthorizationError(operationBase))
             {
                 return null;
             }

             if (IsServerNotFoundError(operationBase))
             {
                 operationBase.SecureMarkErrorAsHandled();
                 return GetServerNotFoundErrorMessage();
             }

             if (IsMessageExceededError(operationBase))
             {
                 operationBase.SecureMarkErrorAsHandled();
                 return GetMessageExceededErrorMessage();
             }

             if (operationBase.IsPayloadExists())
             {
                 var serviceOperationResult = operationBase.GetPayloadData<ServiceOperationResult>();

                 if (serviceOperationResult == null)
                 {
                     return null;
                 }

                 operationBase.SecureMarkErrorAsHandled();
                 return GetPayloadedErrorMessage(serviceOperationResult);
             }

             if (IsTimeOutError(operationBase))
             {
                 operationBase.SecureMarkErrorAsHandled();
                 return GetTimeOutErrorMessage();
             }

             var validationErrors = GetValidationErrors(operationBase);
             if (!validationErrors.Any())
             {
                 return null;
             }

             operationBase.SecureMarkErrorAsHandled();
             if (!showValidationErrors)
             {
                 return null;
             }

             var localizationService = ServiceLocator.Current.GetInstance<ILocalizationService>();
             var message = localizationService.Resources[CommonValidationFailedMessageTemplateKey].Replace(
                 "\\n", Environment.NewLine);

             message += BuildValidationErrorMessage(validationErrors);
             return Tuple.Create(localizationService.Resources[CommonValidationFailedTitleKey], message, false);
         }*/

        /*private static IEnumerable<ValidationResult> GetValidationErrors(OperationBase operationBase)
        {
            var submitOperation = operationBase as SubmitOperation;
            var invokeOperation = operationBase as InvokeOperation;
            var loadOperation = operationBase as LoadOperation;

            var validationErrors = Enumerable.Empty<ValidationResult>();

            if (submitOperation != null)
            {
                validationErrors = submitOperation.EntitiesInError.Cast<EntityBase>().SelectMany(x => x.ValidationErrors);
            }
            else if (invokeOperation != null)
            {
                validationErrors = invokeOperation.ValidationErrors;
            }
            else if (loadOperation != null)
            {
                validationErrors = loadOperation.ValidationErrors;
            }

            return validationErrors;
        }*/

        /// <summary>
        /// Determines whether error is not authorization exception.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>
        ///   <c>true</c> if error is authorization exception; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsAuthorizationError(OperationBase operationBase)
        {
            if (operationBase.HasError)
            {
                var error = operationBase.Error as DomainOperationException;
                if (error != null && error.ErrorCode == UnauthorizedErrorCode)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the message was exceeded available size.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>
        ///   <c>true</c> if the message was exceeded available size; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsMessageExceededError(OperationBase operationBase)
        {
            /*if (operationBase.HasError)
            {
                if (operationBase.Error.TrailException<Exception>().Any(x => MessageExceededErrorTemplate.Any(t => x.Message.Contains(t))))
                {
                    return true;
                }
            }*/

            return false;
        }

        /// <summary>
        /// Determines whether error is server not found.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>
        ///   <c>true</c> if error is server not found; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsServerNotFoundError(OperationBase operationBase)
        {
            /*if (operationBase.HasError)
            {
                return
                    operationBase.Error.TrailException<WebException>().Any(x => x.Message.Contains(ServerNotFoundErrorTemplate));
            }*/

            return false;
        }

        /// <summary>
        /// Determines whether when time out error occur in the specified operation.
        /// </summary>
        /// <param name="operationBase">The operation base.</param>
        /// <returns>
        ///   <c>true</c> if time out error occur in the specified operation; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsTimeOutError(OperationBase operationBase)
        {
            if (operationBase.HasError)
            {
                if (operationBase.Error != null && operationBase.Error.InnerException is TimeoutException)
                {
                    return true;
                }
            }

            return false;
        }
        /*
                /// <summary>
                /// Gets the error message from payload.
                /// </summary>
                /// <param name="serviceOperationResult">The service operation result.</param>
                /// <returns>Title, message, is warning</returns>
                private static Tuple<string, string, bool> GetPayloadedErrorMessage(
                    ServiceOperationResult serviceOperationResult)
                {
                    var localizationService = ServiceLocator.Current.GetInstance<ILocalizationService>();
                    var parameterService = ServiceLocator.Current.GetInstance<IParameterService>();
                    var useLocalTime = parameterService.GetValue<string>("UseLocalTime") == "1";

                    if (serviceOperationResult.Message == "ConcurrencyException")
                    {
                        var param = serviceOperationResult.Details.Split(',');

                        var user = Uri.UnescapeDataString(param[0]);
                        var date = DateTime.FromBinary(long.Parse(param[1])); // error message in utc format

                        var message = localizationService.Resources[ConcurrencyMessageTemplateKey].FormatString(user, useLocalTime ? date.ToLocalTime() : date);

                        return Tuple.Create(localizationService.Resources[ConcurrencyErrorTitleKey], message, false);
                    }

                    if (serviceOperationResult.Message == "TimeoutException")
                    {
                        return Tuple.Create(
                            localizationService.Resources[TimeoutErrorTitleKey],
                            localizationService.Resources[TimeoutMessageTemplateKey],
                            false);
                    }

                    if (serviceOperationResult.Message == "SecurityException")
                    {
                        return Tuple.Create(
                            localizationService.Resources[NotEnoughPermissionsTitleKey],
                            localizationService.Resources[NotEnoughPermissionsTemplateKey],
                            false);
                    }

                    if (serviceOperationResult.Message == "EntityNotFoundException")
                    {
                        var param = serviceOperationResult.Details.Split(',');

                        var entityName = Uri.UnescapeDataString(param[1]);
                        var entetyNameKey = EntityNameTemplateKey.FormatString(entityName);
                        var localizedEntityName = localizationService.Resources.GetValueOrDefault(entetyNameKey, entityName);
                        var date = DateTime.FromBinary(long.Parse(param[2])); // error message in utc format

                        var message = localizationService.Resources[EntityNotFoundTemplateKey].FormatString(
                            localizedEntityName, // entity type
                            Uri.UnescapeDataString(param[0]), // user name
                           useLocalTime ? date.ToLocalTime() : date); // deleted on

                        return Tuple.Create(localizationService.Resources[EntityNotFoundTitleKey], message, false);
                    }

                    if (serviceOperationResult.Message == "SqlException")
                    {
                        return Tuple.Create(
                                localizationService.Resources[SqlErrorTitleKey],
                                serviceOperationResult.Details,
                                false);
                    }

                    return Tuple.Create(
                        localizationService.Resources[CommonErrorTitleKey],
                        serviceOperationResult.Message,
                        serviceOperationResult.Code == OperationResultCode.Warning);
                }
        
                /// <summary>
                /// Gets the time out error message.
                /// </summary>
                /// <returns>Title, message, is warning</returns>
                private static Tuple<string, string, bool> GetTimeOutErrorMessage()
                {
                    return Tuple.Create(
                       OperationTimeoutTitleKey.TransformToLocalizedValue(),
                       OperationTimeoutMessage.TransformToLocalizedValue(),
                       false);
                }

                /// <summary>
                /// Gets the error not found error message.
                /// </summary>
                /// <returns>Title, message, is warning</returns>
                private static Tuple<string, string, bool> GetServerNotFoundErrorMessage()
                {
                    return Tuple.Create(
                       OperationTimeoutTitleKey.TransformToLocalizedValue(),
                       OperationServerNotFoundMessage.TransformToLocalizedValue(),
                       false);
                }

                /// <summary>
                /// Gets the message exceeded error message.
                /// </summary>
                /// <returns>Title, message, is warning</returns>
                private static Tuple<string, string, bool> GetMessageExceededErrorMessage()
                {
                    return Tuple.Create(
                        CommonErrorTitleKey.TransformToLocalizedValue(),
                        MessageExceededMessage.TransformToLocalizedValue(),
                        false);
                }
        
                /// <summary>
                /// Builds the validation error message.
                /// </summary>
                /// <param name="validationErrors">The validation errors.</param>
                /// <returns>Built message.</returns>
                private static string BuildValidationErrorMessage(IEnumerable<ValidationResult> validationErrors)
                {
                    const string ValidationLineFormat = "{0}: {1}";
                    var errorSammury = new StringBuilder();

                    foreach (var validationError in validationErrors)
                    {
                        var messegeLine = validationError.MemberNames != null && validationError.MemberNames.Count() > 0
                            ? ValidationLineFormat.FormatString(string.Join(",", validationError.MemberNames), validationError.ErrorMessage)
                            : validationError.ErrorMessage;
                        errorSammury.AppendLine(messegeLine);
                    }

                    return errorSammury.ToString();
                }*/
    }
}
