using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Enable.Common
{
    /// <summary>
    /// Represents one or more business logic or validation error conditions.
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        private readonly IEnumerable<string> _validationMessages = Enumerable.Empty<string>();

        public ValidationException()
        {
        }

        public ValidationException(string message)
            : base(message)
        {
            Argument.IsNotNull(message, "message");
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ValidationException(IEnumerable<string> validationMessages)
            : base(string.Empty)
        {
            Argument.IsNotNull(validationMessages, "validationMessages");

            _validationMessages = validationMessages;
        }

        public ValidationException(string message, IEnumerable<string> validationMessages)
            : base(message)
        {
            Argument.IsNotNull(message, "message");
            Argument.IsNotNull(validationMessages, "validationMessages");

            _validationMessages = validationMessages;
        }

        protected ValidationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }

        public IEnumerable<string> ValidationMessages
        {
            get
            {
                return _validationMessages;
            }
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public override string ToString()
        {
            var message = new StringBuilder();

            if (Message != null)
            {
                message.Append(Message);
            }

            if (ValidationMessages != null)
            {
                foreach (string validationMessage in ValidationMessages)
                {
                    message.AppendLine();
                    message.Append("  ");
                    message.Append(validationMessage);
                }
            }

            if (message.Length > 0)
            {
                return message.ToString();
            }

            return base.ToString();
        }
    }
}
