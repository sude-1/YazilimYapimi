using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //temel voidler için başlangıç
    public interface IResult
    {
        bool Success { get; } //işlem başarılı mı true-false döner
        string Message { get; } //işlem sonucunda ne yaptı işlem başarısız veya başarılı gibi mesaj 

    }
}
