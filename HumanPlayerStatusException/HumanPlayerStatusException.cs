using System;

namespace HumanPlayerStatusExceptions
{
    public class HumanPlayerStatusException:Exception
    {
        public int ErrorCode { get; set; }

        public HumanPlayerStatusException( string Message, int errorCode) : base(Message)
        {
            ErrorCode = errorCode;
        }
    }
}
