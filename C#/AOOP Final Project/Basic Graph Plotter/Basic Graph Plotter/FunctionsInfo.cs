using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Media;

namespace Basic_Graph_Plotter
{
    class FunctionsInfo
    {
        private Label indexLabel;
        private Label functionExpressionLabel;
        private StackPanel functionInfo;

        public FunctionsInfo(string functionExpression, int index, double height)
        {
            this.configureLabels(functionExpression, index, height);
            this.configureFunctionList(height);
        }

        private void configureFunctionList(double height)
        {
            functionInfo = new StackPanel();
            functionInfo.Orientation = Orientation.Horizontal;
            functionInfo.Height = height;
            //functionInfo.Background = Brushes.Black;
            functionInfo.Children.Add(indexLabel);
            functionInfo.Children.Add(functionExpressionLabel);
        }

        private void configureLabels(string expr, int index, double height)
        {
            functionExpressionLabel = new Label();
            functionExpressionLabel.Content = expr;
            functionExpressionLabel.Height = height;
            functionExpressionLabel.FontSize = functionExpressionLabel.Height / 2.0;

            indexLabel = new Label();
            indexLabel.Content = index + ".";
            indexLabel.Height = height;
            indexLabel.FontSize = functionExpressionLabel.Height / 2.0;
        }

        public string FunctionString
        {
            get { return functionExpressionLabel.Content.ToString(); }
            set { functionExpressionLabel.Content = value; }
        }

        public StackPanel FunctionInfo
        {
            get { return functionInfo; }
            set { functionInfo = value; }
        }
    }
}
