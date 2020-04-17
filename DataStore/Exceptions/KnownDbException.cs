using System;

namespace DataStore.Exceptions
{
    public abstract class KnownDbException : Exception
    {
        public abstract string Identifier { get; }
        public string ErrorMessage { get; protected set; }
        public abstract int? StatusCode { get; }
    }

    public class NoUserExistException : KnownDbException
    {
        public override string Identifier => "NoUserExist";
        public override int? StatusCode { get => 404; }

        public NoUserExistException()
        { }

        public NoUserExistException(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }

    public class UserAlreadyRegisteredWithEmailException : KnownDbException
    {
        public override string Identifier => "UserAlreadyRegisteredWithEmail";
        public override int? StatusCode { get => 409; }
        public UserAlreadyRegisteredWithEmailException()
        { }

        public UserAlreadyRegisteredWithEmailException(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }
    }
}
