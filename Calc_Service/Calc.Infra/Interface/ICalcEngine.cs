using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Infra.Interface
{
    public interface ICalcEngine
    {
        string Result { get; set; }

        void InputNumber(object obj);
        void InputBracket(object obj);
        void InputOperation(object obj);
        void InputFunctionParam1(object obj);
        void InputFunctionParam2(object obj);
        void InputEqual();
        void InputPoint();
        void InputClear();
        void InputDelete();
    }
}
