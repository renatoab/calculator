using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace calculadora
{
    public partial class Form1 : Form
    {
        public string ascOut;
        public string entrAux;
        public char chr;            
        public bool onlyNumbers;
        public bool unit;
        private unitType unitIn;
        private unitType unitOut;

        struct unitType
        {
            public double value;
            public int type;
        }

        /// <summary>
        /// true if the "+" check is checked. If it is true, the clipboard value is automatically
        /// pasted preeceded by "+" when the textBoxEntrada is double clicked.
        /// </summary>
        public bool enablePaste;
        
        /// <summary>
        /// InitializeComponent2 was created to add the MouseDoubleClick event to the textBoxEntrada.
        /// Adding the code to InitializeComponent was not working.
        /// </summary>
        private void InitializeComponent2()
        {
            this.textBoxEntrada.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pasteClipBoard);
            this.unit = false;
        }

        public Form1()
        {
            InitializeComponent();
            InitializeComponent2();
            ascOut = "";
            chr = '\n';
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Event called when some key is typed in textBoxEntrada.
        /// Also called by pasteClipBoard, to force refresh.
        /// Verifies the data in textBoxEntrada, call calculate to calculate the
        /// the result and print it on textBoxCalcOut.
        /// The ASCII translation is shown in the textBoxAscOut depending on the text entered.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshOut(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                textBoxEntrada.Text = "";
                textBoxCalcOut.Text = "";
                textBoxAscOut.Text = "";
                textBoxLabelOut.Text = "";
                useUnits(false);
            }
            else if (e.KeyValue == 189 && e.Modifiers == Keys.Shift)
            {
                textBoxEntrada.Text = textBoxEntrada.Text.Replace('_', '\n');
                useUnits(true);
            }
            else if (e.KeyValue >= 37 && e.KeyValue <= 40)
            {

            }
            else if (e.KeyValue == 84 && e.Modifiers == Keys.Control)
            {
                // Ctrl+T: enable or disable "Always on top"
                // Remembering that it only works while typing in the textBoxEntrada
                checkBoxAlwaysOnTop.Checked = !checkBoxAlwaysOnTop.Checked;
            }
            else if (e.KeyValue == 112)
            {
                // F1: open the About message.
                about();
            }
            else
            {
                try
                {
                    string textIn;
                    textIn = textBoxEntrada.Text;

                    double result;

                    if (!this.unit)
                        result = calculate(textIn);
                    else
                    {
                        this.unitIn = getUnitValue(textBoxUnitIn.Text);
                        this.unitOut = getUnitValue(textBoxUnitOut.Text);

                        if (unitIn.type == unitOut.type)
                            result = calculate(textIn) * unitIn.value / unitOut.value;
                        else if (unitIn.type * unitOut.type == 0)
                            result = calculate(textIn);
                        else
                            result = calculate("");
                    }

                    textBoxCalcOut.Text = Convert.ToString(result);
                }
                catch
                {
                    textBoxCalcOut.Text = "";
                }

                try
                {
                    ascOut = "";
                    entrAux = textBoxEntrada.Text;
                    int intChar;
                    if (entrAux != "")
                    {
                        chr = entrAux.Substring(0, 1).ToUpper().ToCharArray()[0];

                        // Checks if there are only numbers. If there are, calculates
                        // the ASCII^-1. Otherwise, calculates ASCII.

                        onlyNumbers = true;
                        for (int i = 0; i <= entrAux.Trim().Length - 1; i++)
                        {
                            chr = entrAux.Trim().Substring(i, 1).ToCharArray()[0];
                            if (chr < 48 || chr > 57) onlyNumbers = false;
                        }

                        if (onlyNumbers)
                        {
                            entrAux = entrAux.Trim();
                            if (entrAux.Length > 8) entrAux = entrAux.Substring(0, 8);
                            for (int i = 0; i <= entrAux.Length - 1; i++)
                            {
                                if (i % 2 == 0 && i + 1 <= entrAux.Length - 1)
                                {
                                    intChar = (entrAux.Substring(i, 1).ToCharArray()[0] - 48) * 10 +
                                              (entrAux.Substring(i + 1, 1).ToCharArray()[0] - 48);
                                    ascOut = ascOut + (char)intChar;
                                }
                            }
                            textBoxAscOut.Text = ascOut;
                            textBoxLabelOut.Text = "ASCII^-1 =";
                        }
                        else
                        {
                            if (entrAux.Length > 4) entrAux = entrAux.Substring(0, 4);
                            ascOut = "";
                            for (int i = 0; i <= entrAux.Length - 1; i++)
                            {
                                chr = entrAux.Substring(i, 1).ToUpper().ToCharArray()[0];
                                intChar = chr;
                                if (intChar > 32)
                                    ascOut = ascOut + intChar.ToString();
                            }
                            textBoxAscOut.Text = ascOut;
                            textBoxLabelOut.Text = "ASCII =";
                        }
                    }
                    else
                    {
                        textBoxAscOut.Text = "";
                        textBoxLabelOut.Text = "";
                    }
                }
                catch
                {
                    textBoxAscOut.Text = "";
                    textBoxLabelOut.Text = "";
                }

            }
            
        }
        /// <summary>
        /// Calculates the result of a mathematical expression entered as a string textIn.
        /// The basic calculations (+ - * /)are done by DataTable().Compute(textIn, null).
        /// Other calculations are implemented here: LN, EXP, ^.
        /// </summary>
        /// <param name="textIn"></param>
        /// <returns></returns>
        private double calculate(string textIn)
        {
            // The search for LN, EXP (or any other function to be implemented later have
            // to be appear in this routine before "^".
            
            // Changes every , to . to allow any decimal saparator, and eliminates unnecessary operators.
            textIn = textIn.Replace(',', '.');
            textIn = textIn.Replace("--", "+");
            textIn = textIn.Replace("-+", "-");
            textIn = textIn.Replace("+-", "-");
            textIn = textIn.Replace("++", "+");
            textIn = textIn.Replace(";;", ";");
            textIn = textIn.ToUpper().Replace("PI", "3.1415926535897932");

            // Ignores any operator at the end of the expression.
            switch (textIn.Substring(textIn.Length - 1, 1))
            {
                case "+":
                case "-":
                case "*":
                case "/":
                case "^": 
                case ";": return (double) calculate(textIn.Substring(0, textIn.Length - 1));
            }

            // Here the variables defined are substituted in the expression.
            // Always from right to left.
            // Ex: a=-2; b=a^2; a+b   --->   a=-2; a+(a^2)   --->   (-2)+((-2)^2)

            int lastSc = textIn.ToUpper().LastIndexOf(";");
            if (lastSc == 0)
            {
                return calculate(textIn.Substring(1, textIn.Length));
            }
            else if (lastSc != -1)
            {
                string strLeft;
                string strRight;
                int firstSc = textIn.ToUpper().IndexOf(";");
                string textInOrig = textIn;
                bool done = false;
                do
                {
                    if (firstSc == lastSc && firstSc != -1)
                    {   // If theres only one ";"
                        strLeft = textIn.Substring(0, firstSc);
                        strRight = textIn.Substring(firstSc + 1);
                        textIn = replaceVar(strLeft, strRight);
                        done = true;
                    }
                    else
                    {   // If there are many ";" , substitutes the variables from left to right
                        strLeft = textIn.Substring(0, lastSc);
                        strRight = textIn.Substring(lastSc + 1);
                        firstSc = strLeft.ToUpper().LastIndexOf(";");

                        strRight = replaceVar(strLeft.Substring(firstSc+1), strRight);
                        strLeft = textIn.Substring(0, firstSc);
                        textIn = strLeft + ";" + strRight;             
                    }
                    firstSc = textIn.ToUpper().IndexOf(";");
                    lastSc = textIn.ToUpper().LastIndexOf(";");
                } while (!done);
                
            }

            // Search for the functions "Function(", ex: "ln(", "exp(" and replace any f(xxx) on textIn 
            // by the value obtained on its evaluation. 
            // The expression in the parenthesis is evaluated recursively.

            int i = textIn.ToUpper().IndexOf("LN(");
            if (i >= 0)    // If "Function(" was found
            {
                // Separates everything before Function(xxx)
                string strLeft = i == 0 ? "" : textIn.Substring(0, i);

                // Separates the part xxx of Function(xxx)
                int abre = 1, fecha = 0;
                int j;
                for (j = i+3; abre > fecha; j++)
                {
                    if (textIn.Substring(j, 1) == "(") abre++;
                    if (textIn.Substring(j, 1) == ")") fecha++;
                }
                string strMid = textIn.Substring(i + 3, (j-1) - (i+3));

                // Separates the part after Function(xxx)
                string strRight = (j) <= textIn.Length - 1 ? textIn.Substring(j) : "";

                // Calculates the function, and calculates the whole expression.
                double resultPart = Math.Log(calculate(strMid));
                strMid = Convert.ToString(resultPart);
                string parc = String.Concat(strLeft, strMid, strRight);
                return (double) calculate(parc);
            }

            i = textIn.ToUpper().IndexOf("EXP(");
            if (i >= 0)    // If "EXP(" was found
            {
                int abre = 1, fecha = 0;
                string strLeft = i == 0 ? "" : textIn.Substring(0, i);
                int j;
                for (j = i + 4; abre > fecha; j++)
                {
                    if (textIn.Substring(j, 1) == "(") abre++;
                    if (textIn.Substring(j, 1) == ")") fecha++;
                }
                string strMid = textIn.Substring(i + 4, (j - 1) - (i + 4)); // Pega a parte xxx de EXP(xxx)
                string strRight = (j) <= textIn.Length - 1 ? textIn.Substring(j) : "";
                double resultPart = Math.Exp(calculate(strMid));
                strMid = Convert.ToString(resultPart);
                string parc = String.Concat(strLeft, strMid, strRight);
                return (double) calculate(parc);
            }

            // If any onther function is added, the protection against replacement 
            // have to be made in replaceVar.

            // Calculates the exponentiation
            // Separates the exponent (right) and base (left) operands.
            // Only recognizes parenthesized expressions and numeric values, not functions.
            // The functions have to be evaluated earlier.

            i = textIn.ToUpper().LastIndexOf("^", textIn.Length);
            if (i > 0)    // If "^" was found
                          // using ">" instead of ">=" because "^" can't be at beginning.
            {
                string strLeftIn = "";
                string strLeftOut = "";
                string strRightIn = "";
                string strRightOut = "";
                int abre, fecha;

                // obtains base operand (a of a^b)

                char leftChar = char.Parse(textIn.Substring(i - 1, 1));
                if ((leftChar >= 48 && leftChar <= 57) || leftChar == ')' || leftChar == '.')
                {
                    abre = 0; fecha = 0;
                    if (leftChar == ')') fecha = 1;
                    if (fecha >= 1)
                    // if the expression before is parenthesized, go through the characters until
                    // the number of "(" equals the number of ")".
                    {
                        int j = i - 2;
                        for (; abre < fecha; j--)
                        {
                            if (textIn.Substring(j, 1) == "(") abre++;
                            if (textIn.Substring(j, 1) == ")") fecha++;
                        }
                        strLeftOut = textIn.Substring(0, j + 1);
                        strLeftIn = textIn.Substring(j + 1, i - j - 1);
                    }
                    else
                        // if there are no parenthesis before "^", gets the whole string before the "^",
                        // and takes each of the beginning characteres until there is a numeric value.
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (isNumeric(textIn.Substring(j, i - j)))
                            {
                                if (textIn.Substring(j, 1) != "+" && textIn.Substring(j, 1) != "-")
                                {
                                    strLeftIn = textIn.Substring(j, i - j);
                                    strLeftOut = textIn.Substring(0, j);
                                    j = i;
                                }
                            }
                        }
                    }
                }

                // Obtains exponent operand (b of a^b)

                char rightChar = char.Parse(textIn.Substring(i + 1, 1));
                if ((rightChar >= 48 && rightChar <= 57) || rightChar == '(' || rightChar == '.' ||
                    rightChar == '-')
                {
                    abre = 0; fecha = 0;
                    if (rightChar == '(') abre = 1;
                    if (abre >= 1)
                    // if the expression after is parenthesized, go through the characters until
                    // the number of ")" equals the number of "(".
                    {
                        int j = i + 2;
                        for (; abre > fecha; j++)
                        {
                            if (textIn.Substring(j, 1) == "(") abre++;
                            if (textIn.Substring(j, 1) == ")") fecha++;
                        }
                        strRightIn = textIn.Substring(i + 1, j - i - 1);
                        strRightOut = textIn.Substring(j , textIn.Length - j);
                    }
                    else
                        // if there are no parenthesis after "^", gets the whole string until the end,
                        // and takes each of the ending characteres until there is a numeric value.
                    {
                        for (int j = textIn.Length; j >= i + 1; j--)
                        {
                            if (isNumeric(textIn.Substring(i + 1, j - i - 1)))
                            {
                                strRightIn = textIn.Substring(i + 1, j - i - 1);
                                strRightOut = textIn.Substring(j, textIn.Length - j);
                                j = i; 
                            }
                        }
                    }

                }

                // Calculates a^b and calculates the new whole expression
                double resultPart = Math.Pow(calculate(strLeftIn), calculate(strRightIn));
                //double resultPart = Math.Exp(calculate(strRightIn) * Math.Log(calculate(strLeftIn)));
                string strMid = Convert.ToString(resultPart);
                string parc = String.Concat(strLeftOut, strMid, strRightOut);
                return (double)calculate(parc);
            }

            // Calculates the expression. 
            // This command only evaluates the basic operations. The functions and exponentiation
            // have to be evaluated earlier.
            var result = new DataTable().Compute(textIn, null);
            double test = Convert.ToDouble(result);
            return test;
        }

        /// <summary>
        /// Replaces a variable, defined as a value or expression, in an expression.
        /// Uses parenthesis, so, "a=-2;a^2" results (-2)^2 instead of -2^2
        /// </summary>
        /// <param name="varDef">Variable definition. An expression of type a=b</param>
        /// <param name="expression">An expression that utilizes the variable a</param>
        /// <returns></returns>
        private string replaceVar(string varDef, string expression)
        {
            // The variable name and expression are converted to lower case, and the
            // functions to upper case, to avoid replacement in the functions name.
            // If any function is added, this protection has to be done.
            // Constants, like PI does note need this protection, because they are evaluated first.
            varDef = varDef.ToUpper();
            expression = expression.ToUpper();
            expression = expression.Replace("LN", "ln");
            expression = expression.Replace("EXP", "exp");

            int i = varDef.IndexOf("=");
            if (i <= 0)
            {
                return expression;
            }
            else
            {
                string strLeft = varDef.Substring(0, i);
                if (isValidVarName(strLeft))
                {
                    string strRight = varDef.Substring(i + 1);
                    return expression.Replace(strLeft, "(" + strRight + ")");
                }
                else
                {
                    return expression;
                }
            }            
        }

        /// <summary>
        /// Enable and disable the "Always on top" option
        /// </summary>
        private void setAlwaysOnTop(object sender, EventArgs e)
        {
            this.TopMost = this.checkBoxAlwaysOnTop.Checked;
        }

        /// <summary>
        /// Event called from MouseDoubleClick on textBoxEntrada.
        /// If this.enablePaste is true, Clipboard contains text and this text is a numeric value,
        /// the value is pasted preceeded by a "+" signal.
        /// </summary>
        private void pasteClipBoard(object sender, EventArgs e)
        {
            if (this.enablePaste)
            {
                if (Clipboard.ContainsText())
                {
                    if (isNumeric(Clipboard.GetText()))
                    {
                        textBoxEntrada.Text += (char) 13 + "+" + Clipboard.GetText();
                        refreshOut(null, new KeyEventArgs(new Keys()));
                    }
                }
            }
        }

        /// <summary>
        /// Shows a message box with an explanation about the program.
        /// </summary>
        private void about()
        {
            MessageBox.Show("Calculadora " + labelVersion.Text + (char)13 + (char)13 +
                            "Resolve operações matemáticas simples e mostra o resultado enquanto" + (char)13 +
                            "se digita, com o objetivo de reduzir a necessidade de cliques e manter" + (char)13 +
                            "visível a fórmula digitada. O cálculo deve ser digitado na caixa de texto" + (char)13 +
                            "superior e o resultado aparece na caixa inferior." + (char)13 +
                            "" + (char)13 +
                            "Suporta operações básicas, potência (^), ln(), exp(), e permite" + (char)13 +
                            "definição de variáveis. Ex: \"a=2; b=a^2; b+10\" resulta em 14."+ (char)13 +
                            "" + (char)13 +
                            "Se o chechBox \"+\" estiver habilitado, com duplo clique, valor" + (char)13 +
                            "numérico da área de transferência é colado seguido de \"+\"." + (char)13 +
                            "" + (char)13 +
                            "Para conversão de unidades, tecle \"_\" no valor de entrada e digite" + (char)13 +
                            "as unidades de entrada e saída, nos campos que aparecem à direita.");
        }

        /// <summary>
        /// Verifies if a string is a numeric value.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool isNumeric(string str)
        {
            try
            {
                Double.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Verifies if a string can be a variable name.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool isValidVarName(string str)
        {
            str = str.ToUpper();
            char ch;
            for (int i = 0; i < str.Length; i++)
            {
                ch = str.Substring(i, 1).ToCharArray()[0];
                if (ch != 95 && (ch < 65 || ch > 90))
                {
                    if (ch < 48 || ch > 57 || i == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Called when the "+" checkbox value is changed.
        /// The this.enablePaste is set. The textBoxEntrada height is set and the other elements positions
        /// are adjusted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void setAddClipboard(object sender, EventArgs e)
        {
            this.enablePaste = checkBoxAddClipboard.Checked;
            if (this.enablePaste)
            {
                this.Height += 100;
                this.textBoxEntrada.Height += 100;
                this.textBoxCalcOut.Location = new Point(this.textBoxCalcOut.Location.X, this.textBoxCalcOut.Location.Y + 100);
                this.textBoxAscOut.Location = new Point(this.textBoxAscOut.Location.X, this.textBoxAscOut.Location.Y + 100);
                this.textBoxLabelOut.Location = new Point(this.textBoxLabelOut.Location.X, this.textBoxLabelOut.Location.Y + 100);
                this.checkBoxAddClipboard.Location = new Point(this.checkBoxAddClipboard.Location.X, this.checkBoxAddClipboard.Location.Y + 100);
                this.checkBoxAlwaysOnTop.Location = new Point(this.checkBoxAlwaysOnTop.Location.X, this.checkBoxAlwaysOnTop.Location.Y + 100);
                this.labelVersion.Location = new Point(this.labelVersion.Location.X, this.labelVersion.Location.Y + 100);
            }
            else
            {
                this.Height -= 100;
                this.textBoxEntrada.Height -= 100;
                this.textBoxCalcOut.Location = new Point(this.textBoxCalcOut.Location.X, this.textBoxCalcOut.Location.Y - 100);
                this.textBoxAscOut.Location = new Point(this.textBoxAscOut.Location.X, this.textBoxAscOut.Location.Y - 100);
                this.textBoxLabelOut.Location = new Point(this.textBoxLabelOut.Location.X, this.textBoxLabelOut.Location.Y - 100);
                this.checkBoxAddClipboard.Location = new Point(this.checkBoxAddClipboard.Location.X, this.checkBoxAddClipboard.Location.Y - 100);
                this.checkBoxAlwaysOnTop.Location = new Point(this.checkBoxAlwaysOnTop.Location.X, this.checkBoxAlwaysOnTop.Location.Y - 100);
                this.labelVersion.Location = new Point(this.labelVersion.Location.X, this.labelVersion.Location.Y - 100);
            }
        }

        /// <summary>
        /// Receives a string, for example "kPa" and returns a struct with the value and
        /// type of the unit.
        /// All SI units have value 1.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private unitType getUnitValue(string str)
        {
            unitType u = new unitType();
            u.value = 1;
            u.type = 0;
            switch(str)
            {
                case "": u.value = 1; u.type = 0; break;

                case "m":  u.value = 1; u.type = 1; break;
                case "cm": u.value = 0.01; u.type = 1; break;
                case "mm": u.value = 0.001; u.type = 1; break;
                case "km": u.value = 1000; u.type = 1; break;
                case "ft": u.value = 0.3048; u.type = 1; break;
                case "in": u.value = 0.0254; u.type = 1; break;

                case "m3": u.value = 1; u.type = 2; break;
                case "ft3": u.value = 0.028316846592; u.type = 2; break;
                case "dm3":
                case "L": u.value = 0.001; u.type = 2; break;
                case "cm3":
                case "mL": u.value = 0.000001; u.type = 2; break;

                case "kg": u.value = 1; u.type = 3; break;
                case "g": u.value = 0.001; u.type = 3; break;
                case "mg": u.value = 0.000001; u.type = 3; break;
                case "lb": u.value = 0.4536; u.type = 3; break;

                case "s": u.value = 1; u.type = 4; break;
                case "min": u.value = 60; u.type = 4; break;
                case "h": u.value = 3600; u.type = 4; break;
                case "d": u.value = 86400; u.type = 4; break;

                case "Pa": u.value = 1; u.type = 5; break;
                case "kPa": u.value = 1e3; u.type = 5; break;
                case "MPa": u.value = 1e6; u.type = 5; break;
                case "bar": u.value = 1e5; u.type = 5; break;
                case "kgf/cm2": u.value = 98066.5; u.type = 5; break;
                case "atm": u.value = 101325; u.type = 5; break;
                case "mmHg": u.value = 133.3224; u.type = 5; break;
                case "psi": u.value = 6894.757; u.type = 5; break;

                case "K": u.value = 1; u.type = 6; break;
                case "°R": u.value = 1.0/1.8; u.type = 6; break;

                case "J": u.value = 1; u.type = 7; break;
                case "kJ": u.value = 1000; u.type = 7; break;
                case "cal": u.value = 4.1858; u.type = 7; break;
                case "kcal": u.value = 4185.8; u.type = 7; break;
                case "Btu": u.value = 1055.87; u.type = 7; break;

                case "kg/s": u.value = 1; u.type = 8; break;
                case "g/s": u.value = 0.001; u.type = 8; break;
                case "kg/h": u.value = 1.0/3600; u.type = 8; break;
                case "kg/d": u.value = 1.0/86400; u.type = 8; break;
                case "t/h": u.value = 1000.0/3600; u.type = 8; break;
                case "t/d": u.value = 1000.0/86400; u.type = 8; break;

                default: u.value=1; u.type=999; break;
            }
            return u;
        }

        /// <summary>
        /// Called when the text is changed in textBoxUnitIn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxUnitIn_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUnitIn.Text == "" && textBoxUnitOut.Text == "")
            {
                useUnits(false);
            }
            refreshOut(null, new KeyEventArgs(new Keys()));
        }

        /// <summary>
        /// Called when the text is changed in textBoxUnitOut
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxUnitOut_TextChanged(object sender, EventArgs e)
        {
            if (textBoxUnitIn.Text == "" && textBoxUnitOut.Text == "")
            {
                useUnits(false);
            }
            refreshOut(null, new KeyEventArgs(new Keys()));
        }

        private void textBoxUnitIn_Enter(object sender, EventArgs e)
        {
            textBoxUnitIn.SelectionStart = 0;
            textBoxUnitIn.SelectionLength = textBoxUnitIn.Text.Length;
        }

        private void textBoxUnitOut_Enter(object sender, EventArgs e)
        {
            textBoxUnitOut.SelectionStart = 0;
            textBoxUnitOut.SelectionLength = textBoxUnitOut.Text.Length;
        }

        private void textBoxEntrada_Enter(object sender, EventArgs e)
        {
            if (textBoxUnitIn.Text == "" || textBoxUnitOut.Text == "")
            {
                useUnits(false);
            }
        }

        /// <summary>
        /// Enables or disables the units textBox
        /// </summary>
        /// <param name="yesOrNo"></param>
        private void useUnits(bool yesOrNo)
        {
            if (yesOrNo)
            {
                textBoxEntrada.Width = 140;
                textBoxCalcOut.Width = 140;
                textBoxUnitIn.Visible = true;
                textBoxUnitOut.Visible = true;
                textBoxUnitIn.Focus();
                this.unit = true;
            }
            else
            {
                textBoxEntrada.Width = 197;
                textBoxCalcOut.Width = 197;
                textBoxUnitIn.Visible = false;
                textBoxUnitOut.Visible = false;
                this.unit = false;
                textBoxUnitIn.Text = ""; textBoxUnitOut.Text = "";
            }
        }

        private void textBoxUnit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 27)
            {
                useUnits(false);
                textBoxEntrada.Focus();
            }            
        }

        private void textBoxCalcOut_Enter(object sender, EventArgs e)
        {
            textBoxCalcOut.SelectionStart = 0;
            textBoxCalcOut.SelectionLength = textBoxCalcOut.Text.Length;
        }
    }
}
