using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc.Infra
{
    public class InputNumberEvent : PubSubEvent<object> { }
    public class InputOperationiEvent : PubSubEvent<object> { }
    public class InputPointEvent : PubSubEvent<object> { }
    public class InputEqualEvent : PubSubEvent<object> { }
    public class InputClearEvent : PubSubEvent<object> { }
    public class InputDeleteEvent : PubSubEvent<object> { }
    public class InputBracketEvent : PubSubEvent<object> { }
    public class InputFunctionParam1Event : PubSubEvent<object> { }
    public class InputFunctionParam2Event : PubSubEvent<object> { }

}
