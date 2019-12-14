using System;

namespace DSS_Busol
{
    class CheckedFormExceprtion : Exception
    {
        static private string messg = "Error in CheckedForm.";

        public CheckedFormExceprtion()
            : base(messg)
        {
        }

        public CheckedFormExceprtion(string message)
            : base(messg + " " + message)
        {
        }

        public CheckedFormExceprtion(string message, Exception inner)
            : base(messg + " " + message, inner)
        {
        }
    }

    class ARMAModlClassException : Exception
    {
        public ARMAModlClassException()
            : base("**ARMAModlClassException** Error in parameter initialization.")
        { }
    }

    class StatisticalAnalisFormException : Exception
    {
        static string mssg = "Error in StatisticalAnalisForm.";

        public StatisticalAnalisFormException()
            : base(mssg)
        {
        }

        public StatisticalAnalisFormException(string message)
            : base(mssg + " " + message)
        {
        }

        public StatisticalAnalisFormException(string message, Exception inner)
            : base(mssg + " " + message, inner)
        {
        }

    }
}
