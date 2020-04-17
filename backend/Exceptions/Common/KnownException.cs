using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace backend.Exceptions.Common
{
    public abstract class KnownException : Exception
    {
        public abstract string Identifier { get; }
        public abstract StatusCodeResult StatusCode { get; }
        public string ErrorMessage { get; set; }
    }

    public class InvalidRequestParametersException : KnownException
    {
        public InvalidRequestParametersException()
        { }

        public InvalidRequestParametersException(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public override string Identifier => "InvalidRequestParameters";

        public override StatusCodeResult StatusCode => new StatusCodeResult(StatusCodes.Status400BadRequest);

        public int HtttpResponseCode
        {
            get
            {
                return this.StatusCode.StatusCode;
            }
        }
    }

}
