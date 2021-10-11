using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial p = new Polynomial();
            p.AddMonomials(0, 3);
            p.AddMonomials(1, 2);
            p.AddMonomials(2, -5);
            p.AddMonomials(3, 8);
            Console.WriteLine(p.ToString());

            Polynomial p2 = new Polynomial();
            p2.AddMonomials(0, 1);
            p2.AddMonomials(1, 2);
            p2.AddMonomials(2, 3);
            p2.AddMonomials(5, 4);
            Console.WriteLine(p2.ToString());

            var other = Polynomial.AddPolynomials(p2, p);
            Console.WriteLine(other.ToString());
            var other2 = Polynomial.SubstractPolynomials(p2, p);
            Console.WriteLine(other2.ToString());

            var multiply = Polynomial.MultiplyPolynomial(p, p2);
            Console.WriteLine(multiply.ToString());

            Polynomial p3 = new Polynomial();
            p3 = p * p2;
            Console.WriteLine(p3.ToString());

            double number = 34.2;
            Polynomial p4 = number;
            Console.WriteLine(p4.ToString());
            Console.ReadLine();
        }
    }
}
