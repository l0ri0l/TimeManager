﻿
namespace TimeManager.BLL.Infrastruction
{
    public class ValidationExeption : Exception
    {
        public string Property { get; protected set; }
        public ValidationExeption(string message, string prop) : base (message)
        {
            Property = prop;
        }
    }
}
