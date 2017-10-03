using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Basic_Graph_Plotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public GraphPlotter graphPlotter;
        public double horizontalRange;
        public double verticalRange;
        int inputTBtextPosition;

        Ellipse tracingCircle;
        WrapPanel tracingInfoBox;
       
        private Brush[] colorsList = {Brushes.Red, Brushes.DarkOrange, Brushes.DarkCyan,
                                           Brushes.DarkSeaGreen, Brushes.Brown, Brushes.DarkGreen,
                                           Brushes.Blue, Brushes.DarkGoldenrod, Brushes.Chocolate,
                                           Brushes.DarkOrchid};

        private const string functionSP_empty_message = "Function Stack is Empty.\nAdd function and then click Draw Graph.";
        private const string FunctionListOverloadMessage = "Function List is Full!";
        private const string invalidFunctionErrorMessage = "Invalid Function!\n";

        public MainWindow()
        {
            InitializeComponent();
            initializeLocalField();
        }

        private void initializeLocalField()
        {
            horizontalRange = Math.Abs(Convert.ToDouble(upperXTB.Text) - Convert.ToDouble(lowerXTB.Text));
            verticalRange =  Math.Abs(Convert.ToDouble(upperYTB.Text) - Convert.ToDouble(lowerYTB.Text));

            graphPlotter = new GraphPlotter(GraphCanvas, Convert.ToDouble(lowerXTB.Text), Convert.ToDouble(upperXTB.Text), Convert.ToDouble(lowerYTB.Text), Convert.ToDouble(upperYTB.Text));
            graphPlotter.drawAxis();

            tracingCircle = new Ellipse();
            tracingInfoBox = new WrapPanel();
           
            inputTBtextPosition = 0;
            
            configureInputButton();
            //createLabel();
        }
        
        private void graphCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Source.ToString() == "System.Windows.Shapes.Line")
            {
                GraphCanvas.Children.Remove(tracingCircle);
                configureTracingCircle(e.GetPosition(GraphCanvas), e.Source as Line);
                showTracingInfo(e.GetPosition(GraphCanvas), e.Source as Line);
            }
        }

        private void graphCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            GraphCanvas.Children.Remove(tracingCircle);
            tracingInfoBox.Children.Clear();
            GraphCanvas.Children.Remove(tracingInfoBox);
        }

        private void inputButton_Click(object sender, RoutedEventArgs e)
        {
            string[] firstBracketAppend = {"sin", "cos", "tan", "e^", "log", "sqrt"};
            Button inputButton = sender as Button;
            bool firstBracketsToAppend = false;

            foreach (string s in firstBracketAppend)
            {
                if (inputButton.Content.ToString() == s)
                {
                    firstBracketsToAppend = true;
                }
            }

            if (firstBracketsToAppend)
            {
                appendInputTB(inputButton.Content + "(");
            }
            else if (inputButton.Content.ToString() == "VAR")
            {
                appendInputTB("x");
            }
            else if (inputButton.Content.ToString() == "Inv")
            {
                appendInputTB("^(-");
            }
            else if (inputButton.Content.ToString() == "Backspace")
            {
                eraseInputTB();
            }
            else
            {
                appendInputTB(inputButton.Content.ToString());
            }
        }
               
        private void btnDrawGraph_Click(object sender, RoutedEventArgs e)
        {
            GraphCanvas.Children.Clear();
            graphPlotter.drawAxis();
            if (FunctionList.Items.Count == 0)
            {
                MessageBox.Show(functionSP_empty_message);
            }
            else 
            {
                drawGraphFromListBox();
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            inputTB.Clear();
            inputTBtextPosition = 0;
            GraphCanvas.Children.Clear();
            graphPlotter.drawAxis();
        }

        private void inputTBkeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                try
                {
                    string expr = ParseExpression.replaceVariable(inputTB.Text, "(0)");
                    ParseExpression.parse(expr);
                    addExpressionToList();
                }
                catch (Exception exception)
                {
                    if (exception.Message.StartsWith("Value was either too large") 
                        || exception.Message.StartsWith("Attempted to divide by zero"))
                    {
                        addExpressionToList();
                    }
                    else
                    {
                        MessageBox.Show(invalidFunctionErrorMessage + exception.Message);
                    }
                }
            }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string expr = ParseExpression.replaceVariable(inputTB.Text, "(0)");
                ParseExpression.parse(expr);
                addExpressionToList();
            }
            catch (Exception exception)
            {
                if (exception.Message.StartsWith("Value was either too large")
                        || exception.Message.StartsWith("Attempted to divide by zero"))
                {
                    addExpressionToList();
                }
                else
                {
                    MessageBox.Show(invalidFunctionErrorMessage + exception.Message);
                }
            }
        }

        private void lowerXkeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    changeHorizontalRange();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void upperXkeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    changeHorizontalRange();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void lowerYkeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    changeVerticalRange();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void upperYkeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Return)
                {
                    changeVerticalRange();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            GraphCanvas.Children.Clear();
            graphPlotter.drawAxis();
            FunctionList.Items.Clear();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Object child = FunctionList.SelectedItem;
            FunctionList.Items.Remove(child);

            foreach (var o in FunctionList.Items)
            {
                StackPanel stackPanel = o as StackPanel;
                stackPanel.Children.OfType<Label>().ElementAt(0).Content = FunctionList.Items.IndexOf(stackPanel) + 1 + ".";
            }
        }

        private void addExpressionToList()
        {
            if (FunctionList.Items.Count < 10) 
            {
                FunctionList.Items.Add(creatAndGetListItem());
            }
            else 
            {
                MessageBox.Show(FunctionListOverloadMessage);
            }
        }

        private void appendInputTB(string s)
        {
            inputTBtextPosition = inputTB.CaretIndex;
            inputTB.Text = inputTB.Text.Insert(inputTBtextPosition, s);
            inputTB.CaretIndex = inputTBtextPosition + s.Length;
            inputTB.Focus();
        }
        
        private void eraseInputTB()
        {
            if (inputTB.Text.Length > 0)
            {
                inputTBtextPosition = inputTB.CaretIndex;
                if (inputTBtextPosition > 0)
                {
                    inputTB.Text = inputTB.Text.Remove(inputTBtextPosition - 1, 1);
                    inputTB.CaretIndex = inputTBtextPosition - 1;
                }
                if (inputTB.CaretIndex <= 0)
                {
                    inputTB.CaretIndex = 1;
                }
            }
        }

        private void drawGraphFromListBox()
        {
            foreach (var child in FunctionList.Items)
            {
                StackPanel stackPanel = (StackPanel)(child);
                Label exprLabel = stackPanel.Children.OfType<Label>().ElementAt(1);
                Trace.WriteLine("Before");
                graphPlotter.drawGraph(exprLabel.Content.ToString(), exprLabel.Foreground);
                Trace.WriteLine("After");
            }
        }

        private StackPanel creatAndGetListItem()
        {
            StackPanel panel = new StackPanel();
            panel.Orientation = Orientation.Horizontal;

            Label exprIndex = new Label();
            exprIndex.Content = (FunctionList.Items.Count + 1) + "." ;
            exprIndex.Height = FunctionList.Height / 8.00;
            exprIndex.FontSize = exprIndex.Height / 2.00;

            Label exprLabel = new Label();
            exprLabel.Content = inputTB.Text;
            exprLabel.Height = FunctionList.Height / 8.00;
            exprLabel.FontSize = exprIndex.Height / 2.00;
            exprLabel.Foreground = colorsList[FunctionList.Items.Count];

            panel.Children.Add(exprIndex);
            panel.Children.Add(exprLabel);

            return panel;
        }

        private void configureTracingCircle(Point point, Line line)
        {
            tracingCircle.Height = line.StrokeThickness + 10;
            tracingCircle.Width = line.StrokeThickness + 10;
            tracingCircle.Fill = line.Stroke;
            tracingCircle.Margin = new Thickness(point.X - tracingCircle.Width / 2.00, point.Y - tracingCircle.Height / 2.00, 0, 0);
            GraphCanvas.Children.Add(tracingCircle);
        }

        private void showTracingInfo(Point point, Line line)
        {
            tracingInfoBox.Children.Clear();
            GraphCanvas.Children.Remove(tracingInfoBox);
            
            tracingInfoBox.Orientation = Orientation.Vertical;
            tracingInfoBox.Margin = new Thickness(0, 0, 0, 0);
            tracingInfoBox.Background = Brushes.White;

            string expr = "";

            foreach (var child in FunctionList.Items)
            {
                StackPanel stackPanel = (StackPanel)(child);
                Label exprLabel = stackPanel.Children.OfType<Label>().ElementAt(1);
                if (exprLabel.Foreground == line.Stroke)
                {
                    expr = exprLabel.Content.ToString();
                    break;
                }
            }

            Label funcLabel = new Label();
            funcLabel.Content = expr;

            Label xLabel = new Label();
            xLabel.Content = "X: " + (graphPlotter.LowerX + (point.X / graphPlotter.UnitWidth));

            Label yLabel = new Label();
            try
            {
                yLabel.Content = "Y: " + Math.Round(ParseExpression.parse(ParseExpression.replaceVariable(expr,
                                              "(" + Convert.ToString(Math.Round((graphPlotter.LowerX + (point.X / graphPlotter.UnitWidth)), 3)) + ")")), 2);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }

            tracingInfoBox.Children.Add(funcLabel);
            tracingInfoBox.Children.Add(xLabel);
            tracingInfoBox.Children.Add(yLabel);

            GraphCanvas.Children.Add(tracingInfoBox);
        }

        private void changeHorizontalRange() 
        {
            graphPlotter.updateHorizontalRange(Convert.ToDouble(lowerXTB.Text), Convert.ToDouble(upperXTB.Text));
            GraphCanvas.Children.Clear();
            graphPlotter.drawAxis();
            drawGraphFromListBox();
        }

        private void changeVerticalRange()
        {
            graphPlotter.updateVerticalRange(Convert.ToDouble(lowerYTB.Text), Convert.ToDouble(upperYTB.Text));
            GraphCanvas.Children.Clear();
            graphPlotter.drawAxis();
            drawGraphFromListBox();
        }

        private void configureInputButton()
        {
            foreach (var childWrapPanel in buttonHolderWrapPanel.Children)
            {
                WrapPanel wrapPanel = childWrapPanel as WrapPanel;
                foreach (var child in wrapPanel.Children)
                {
                    Button button = child as Button;
                    button.Click += inputButton_Click;
                }                
            }
        }

        private void createLabel()
        {
            for (double x = GraphCanvas.Margin.Left; x <= GraphCanvas.Width; x += GraphCanvas.Width / 10)
            {
                Trace.WriteLine("DONE");
                Label l = new Label();
                l.Content = "Test";
                l.Margin = new Thickness(x, GraphCanvas.Margin.Top + GraphCanvas.Height + 5.00, 0, 0);
                MainGrid.Children.Add(l);
            }
        }
    }
}
