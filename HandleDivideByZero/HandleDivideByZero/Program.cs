using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandleDivideByZero
{
    class Program
    {
        static void Main(string[] args)
        {
            string expression = "abc+(bcd)/(cde*def)-efg";

            Console.WriteLine(expression);
            Console.WriteLine(HandleDivideByZero(expression));
            Console.ReadLine();
        }


        public static string HandleDivideByZero(string expression)
        {
            List<char> operators = new List<char>()
            { '+', '-', '/', '*', '(', ')' };
            Dictionary<int, string> parsedExpression = new Dictionary<int, string>();

            if(expression.Contains('/'))
            {
                var startIndex = -1;
                var i = 0;

                while (i < expression.Length)
                {
                    if (operators.Contains(expression[i]))
                    {
                        if(startIndex != -1)
                            parsedExpression.Add(startIndex, expression.Substring(startIndex, i - startIndex));

                        parsedExpression.Add(i, expression[i].ToString());
                        startIndex = -1;
                    }
                    else if(startIndex == -1)
                    {
                        startIndex = i;
                    }
                    i++;
                }

                parsedExpression.Add(startIndex, expression.Substring(startIndex, i - startIndex));

                var divisionOperators = parsedExpression.Where(_ => _.Value == "/").Select(_ => _.Key).ToList();

                foreach (var item in divisionOperators)
                {
                    var preceedingElement = parsedExpression.Where(_ => _.Key < item).Last();

                    if (preceedingElement.Value == ")")
                    {
                        //Find the corresponding opening bracket
                        var level = 0;
                        
                        while (preceedingElement.Value != "(" && level == 0)
                        {
                            preceedingElement = parsedExpression.Where(_ => _.Key < preceedingElement.Key).Last();
                            if (preceedingElement.Value == ")")
                            {
                                level++;
                            }
                            else if (preceedingElement.Value == "(")
                            {
                                level--;
                            }
                        }
                    }

                    parsedExpression[preceedingElement.Key] = string.Format("COALESCE({0}", parsedExpression[preceedingElement.Key]);

                    var suceedingElement = parsedExpression.Where(_ => _.Key > item).First();

                    if (suceedingElement.Value == "(")
                    {
                        parsedExpression[suceedingElement.Key] = string.Format("NULLIF({0}", parsedExpression[suceedingElement.Key]);
                        //Find the corresponding closing bracket
                        var level = 0;

                        while (suceedingElement.Value != ")" && level == 0)
                        {
                            suceedingElement = parsedExpression.Where(_ => _.Key > suceedingElement.Key).First();
                            if (suceedingElement.Value == "(")
                            {
                                level++;
                            }
                            else if (suceedingElement.Value == ")")
                            {
                                level--;
                            }
                        }
                    }
                    parsedExpression[suceedingElement.Key] = string.Format("{0},0),0)", parsedExpression[suceedingElement.Key]);
                    
                }

                var str = parsedExpression.Select(_ => _.Value).ToList();
                expression = string.Join("", str);
            }

            return expression;
        }
    }
}
