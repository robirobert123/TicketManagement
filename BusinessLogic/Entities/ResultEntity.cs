﻿namespace BusinessLogic.Entities
{
    public class ResultEntity<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
