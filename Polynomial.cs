using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Polynomial
    {
        private SortedDictionary<int, double> polynomials;


        public Polynomial()
        {
            polynomials = new SortedDictionary<int, double>();
        }

        public int GetMaxOrder()
        {
            return polynomials.LastOrDefault().Key;
        }


        public void AddMonomials(int key, double value)
        {
            polynomials.Add(key, value);
        }

        public double this[int index]
        {
            get
            {
                return polynomials[index];
            }
            set
            {
                if(value != 0 && polynomials.ContainsKey(index))
                {
                    polynomials[index] = value;
                }

                if(value != 0 && !polynomials.ContainsKey(index))
                {
                    polynomials.Add(index, value);
                }

                if(value == 0 && polynomials.ContainsKey(index))
                {
                    polynomials.Remove(index);
                }
            }
        }

        public static Polynomial AddPolynomials(Polynomial p, Polynomial other)
        {
            int max = Math.Max(p.GetMaxOrder(), other.GetMaxOrder());
            Polynomial newPolynomial = new Polynomial();

            for (int i = 0; i <= max; ++i)
            {
                if (other.polynomials.ContainsKey(i) && p.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, p.polynomials[i] + other.polynomials[i]);
                }
                else if(other.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, other.polynomials[i]);
                }
                else if(p.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, p.polynomials[i]);
                }
            }
            return newPolynomial;
        }

        public static Polynomial SubstractPolynomials(Polynomial p, Polynomial other)
        {
            int max = Math.Max(p.GetMaxOrder(), other.GetMaxOrder());
            Polynomial newPolynomial = new Polynomial();

            for (int i = 0; i <= max; ++i)
            {
                if (other.polynomials.ContainsKey(i) && p.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, p.polynomials[i] - other.polynomials[i]);
                }
                else if (other.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, other.polynomials[i]);
                }
                else if (p.polynomials.ContainsKey(i))
                {
                    newPolynomial.AddMonomials(i, p.polynomials[i]);
                }
            }
            return newPolynomial;
        }

        public static Polynomial MultiplyPolynomial(Polynomial p, Polynomial other)
        {
            int n = Math.Max(p.polynomials.Count, other.polynomials.Count);

            Polynomial newPolynomial = new Polynomial();
            double value = 0.0;
            int key = 0;
            for (int i = 0; i < p.polynomials.Count; ++i)
            {
                for (int j = 0; j < other.polynomials.Count; ++j)
                {
                    value = (p.polynomials.ElementAt(i).Value) * (other.polynomials.ElementAt(j).Value);
                    key = (p.polynomials.ElementAt(i).Key) + (other.polynomials.ElementAt(j).Key);

                    if (newPolynomial.polynomials.ContainsKey(key))
                    {
                        newPolynomial[key] += value;
                    }
                    else
                    {
                        newPolynomial.AddMonomials(key, value);
                    }
                }
                
              
            }
            return newPolynomial;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var coef in polynomials)
            {
                sb.Append(string.Format("{0}*x^{1} + ", coef.Value, coef.Key));
            }
             
            sb.Append("\n");
            return sb.ToString(); 
        }

        private KeyValuePair<int, double> ParseMonomial(char sign, string monomial)
        {
            int power = 0;
            double coef = 0.0;
            for (int i = 0; i < monomial.Length; ++i)
            {
                if (monomial[i] == 'x')
                {
                    if(i + 2 >= monomial.Length)
                    {
                        throw new IndexOutOfRangeException("");
                    }
                    if(monomial[i+1] != '^')
                    {
                        throw new FormatException("Incorrect format of monomial. Expected '^'");
                    }
                    
                    if(!char.IsDigit(monomial[i + 2]))
                    {
                        throw new Exception("Expected power of the monomial.");
                    }
                    power = Convert.ToInt32(monomial.Substring(i + 2, monomial.Length - i - 2));  
                   
                    if(i!=0 && !double.TryParse(monomial.Substring(0, i), out coef))
                    {
                        throw new FormatException("Can't parse the value of coefficient");
                    }
                    break;
                }
               
            }
            if(sign == '-')
            {
                coef = -coef;
            }
            return new KeyValuePair<int, double>(power, coef);
        }

        public void Parse(string polynomial)
        {
            int currentIndex = 0;
            char sign = '+';
            while (currentIndex < polynomial.Length) 
            {
                string monomial = "";

                for (int i = currentIndex; i < polynomial.Length; ++i)
                {
                    if (polynomial[i] == ' ')
                    {
                        continue;
                    }
                    if (polynomial[i] == '+' || polynomial[i] == '-')
                    {
                        if(i == currentIndex)
                        {
                            sign = polynomial[i];
                        }
                        else
                        {
                         
                            currentIndex = i;
                            break;
                        }
                     
                    }

                    else
                    {
                        monomial += polynomial[i];
                        if(i+1 >= polynomial.Length)
                        {
                            currentIndex = i + 1;
                        }
                    }
                }

                var m = ParseMonomial(sign, monomial);      
                this.polynomials.Add(m.Key, m.Value);

            }

        }

    }
}

