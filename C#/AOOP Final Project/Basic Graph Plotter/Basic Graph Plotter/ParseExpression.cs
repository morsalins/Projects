using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Basic_Graph_Plotter
{
    class ParseExpression
    {
        public static Mathos.Parser.MathParser parser = new Mathos.Parser.MathParser();

        public ParseExpression()
        {
            //parser = new Mathos.Parser.MathParser();
        }

        public static string replaceVariable(string variableExpr, string val)
        {
            string expr = "";

            for (int i = 0; i < variableExpr.Length; i++)
            {
                if (variableExpr[i] == 'x')
                {
                    expr += val;
                }
                else
                {
                    expr += variableExpr[i];
                }
            }
            return expr;
        }

        public static double parse(string expr)
        {
            //try
            //{
                return Convert.ToDouble(parser.Parse(expr));
            //}
            //catch (Exception e)
            //{
            //    Trace.WriteLine(e.Message + "\n");
            //    return 0.0;
            //}
        }
    }
}
