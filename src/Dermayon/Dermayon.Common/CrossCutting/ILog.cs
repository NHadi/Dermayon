using System;
using System.Collections.Generic;
using System.Text;

namespace Dermayon.Common.CrossCutting
{
    public interface ILog
    {
        void Info(string message);
        void Info(object obj);
        void Info(string message, object obj);
        void Warning(string message);
        void Warning(object obj);
        void Warning(string message, object obj);
        void Error(string message);
        void Error(object obj);
        void Error(string message, object obj);
        void InitForDebug();
    }
}
