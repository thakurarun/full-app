using System;

namespace DataStore.Exceptions
{
    public class ThrowException
    {
        public static void If(bool throwException, Exception exception)
        {
            if (throwException)
            {
                throw exception;
            }
        }
    }
}
