//using Calc.Infra;
//using Prism.Commands;
//using Prism.Events;
//using Prism.Mvvm;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Input;

//namespace Calc.Engine
//{
//    public class CalcEngine: BindableBase
//    {
//        private bool point = true;
//        private int count = 0;

//        private string result;
//        public string Result
//        {
//            get { return result; }
//            set { SetProperty(ref result, value); }
//        }

         
//        public CalcEngine(IEventAggregator ea)
//        {
//            Result = "0";

//            ea.GetEvent<InputNumberEvent>().Subscribe(InputNumber);
//            ea.GetEvent<InputOperationiEvent>().Subscribe(InputOperation);
//            ea.GetEvent<InputPointEvent>().Subscribe(InputPoint);
//            ea.GetEvent<InputEqualEvent>().Subscribe(InputEqual);
//            ea.GetEvent<InputClearEvent>().Subscribe(InputClear);
//            ea.GetEvent<InputDeleteEvent>().Subscribe(InputDelete);
//            ea.GetEvent<InputBracketEvent>().Subscribe(InputBracket);
//            ea.GetEvent<InputFunctionParam1Event>().Subscribe(InputFunctionParam1);
//            ea.GetEvent<InputFunctionParam2Event>().Subscribe(InputFunctionParam2);
//        }

//        public void InputBracket(object obj)
//        {
//            string exp = getExpression(obj);

//            if (exp.Equals(("(")))
//            {
//                if (CheckLastChar(Result.Last()) == 0)
//                {
//                    Result = Result + exp;
//                    count++;
//                }
//                else if (Result.Count() == 1 && Result.Last() == '0')
//                {
//                    Result = exp;
//                    count++;
//                }
//            }
//            else if (exp.Equals(")") && (CheckLastChar(Result.Last()) == 3 || CheckLastChar(Result.Last()) == 4) && count > 0)
//            {
//                Result = Result + exp;
//                count--;
//            }
//        }

//        public void InputDelete(object obj)
//        {
//            if (Result.Count() > 0)
//            {
//                if (CheckLastChar(Result.Last()) == 2)
//                {
//                    Result = Result.Substring(0, Result.Count() - 1);
//                    while (Result.Count() > 0 && CheckLastChar(Result.Last()) == 10)
//                    {
//                        Result = Result.Substring(0, Result.Count() - 1);
//                    }
//                }
//                else
//                    Result = Result.Substring(0, Result.Count() - 1);
//            }
//            if (Result.Count() == 0)
//                Result = "0";
//        }

//        public void InputClear(object obj)
//        {
//            Result = "0";
//            count = 0;
//            point = true;
//        }

//        public void InputEqual(object obj)
//        {
//            while (count > 0)
//            {
//                Result = Result + ')';
//                count--;
//            }

//            if (CheckLastChar(Result.Last()) == 3 || CheckLastChar(Result.Last()) == 4)
//            {
//                NCalc.Expression e = new NCalc.Expression(Result);
//                Result = Convert.ToString(e.Evaluate());
//            }
//            else
//                MessageBox.Show("수식이 완성되지 않았습니다.");

//        }

//        public void InputPoint(object obj)
//        {

//            if (point && CheckLastChar(Result.Last()) == 4)
//            {
//                Result = Result + ".";
//                point = false;
//            }
//        }

//        public void InputOperation(object obj)
//        {
//            string exp = getExpression(obj);

//            if (CheckLastChar(Result.Last()) == 4 || CheckLastChar(Result.Last()) == 3)
//            {
//                Result = Result + exp;
//                point = true;
//            }
//            if (exp.Equals("-") && CheckLastChar(Result.Last()) == 2)
//            {
//                Result = Result + exp;
//            }
//        }

//        public void InputNumber(object obj)
//        {
//            string exp = getExpression(obj);

//            if (CheckLastChar(Result.Last()) != 3)
//            {
//                if (Result == "0")
//                    Result = exp;
//                else
//                    Result = Result + exp;
//            }
//        }

//        public void InputFunctionParam1(object obj)
//        {
//            string exp = getExpression(obj);
//            if (CheckLastChar(Result.Last()) == 4)
//            {
//                string num = string.Empty;
//                while (Result.Count() > 0 && CheckLastChar(Result.Last()) != 0)
//                {
//                    num = Result.Last() + num;
//                    Result = Result.Substring(0, Result.Count() - 1);
//                }
//                Result = Result + exp + "(" + num + ")";
//            }
//            else if (CheckLastChar(Result.Last()) == 3)
//            {
//                string num = string.Empty;
//                while (CheckLastChar(Result.Last()) != 2)
//                {
//                    num = Result.Last() + num;
//                    Result = Result.Substring(0, Result.Count() - 1);
//                }
//                num = Result.Last() + num;
//                Result = Result.Substring(0, Result.Count() - 1);
//                Result = Result + exp + num;
//            }
//        }

//        public void InputFunctionParam2(object obj)
//        {
//            string exp = getExpression(obj);
//            if (CheckLastChar(Result.Last()) == 4)
//            {
//                string num = string.Empty;
//                while (Result.Count() > 0 && CheckLastChar(Result.Last()) != 0)
//                {
//                    num = Result.Last() + num;
//                    Result = Result.Substring(0, Result.Count() - 1);
//                }
//                Result = Result + exp + '(' + num + ',';
//                count++;
//            }
//            else if (CheckLastChar(Result.Last()) == 3)
//            {
//                string num = string.Empty;
//                while (CheckLastChar(Result.Last()) != 2)
//                {
//                    num = Result.Last() + num;
//                    Result = Result.Substring(0, Result.Count() - 1);
//                }
//                num = Result.Last() + num;
//                Result = Result.Substring(0, Result.Count() - 1);
//                Result = Result + exp + num + ',';
//                count++;
//            }
//        }

//        public string getExpression(object obj)
//        {
//            return (obj is string) ? obj as string : string.Empty;
//        }

//        private int CheckLastChar(char c)
//        {
//            switch (c)
//            {
//                case '+':
//                case '-':
//                case '/':
//                case '*':
//                case '%':
//                    return 0;
//                case '.':
//                case ',':
//                    return 1;
//                case '(':
//                    return 2;
//                case ')':
//                    return 3;
//                case '1':
//                case '2':
//                case '3':
//                case '4':
//                case '5':
//                case '6':
//                case '7':
//                case '8':
//                case '9':
//                case '0':
//                    return 4;
//                default:
//                    return 10;
//            }
//        }
//    }
//}
