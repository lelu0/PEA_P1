using System;

namespace project1PEA
{
    public class EmptyMatrixException : Exception
    {
        public EmptyMatrixException(Exception innerException, string message = "City matrix is empty") : base(message, innerException)
        {
            Console.WriteLine(message);
        }
    }
}