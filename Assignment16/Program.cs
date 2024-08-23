using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Xml.Linq;
using static Assignment16.ListGenerator;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Assignment16
{
    #region EqualityComparer
    class EqualityComparer : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y)
        {
            return Sort(x) == Sort(y);
        }

        public int GetHashCode([DisallowNull] string obj)
        {
            return Sort(obj).GetHashCode();
        }

        public string Sort(string? x)
        {
            var chars = x.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        }
    }
        #endregion
    
    internal class Program
    {
        static void Main(string[] args)
        {
            #region LINQ - Restriction Operators

            #region  Find all products that are out of stock.

            var outOfStockProducts = ProductsList.FindAll(p => p.UnitsInStock == 0);



            #endregion


            #region Find all products that are in stock and cost more than 3.00 per unit

            var expensiveInStockProducts = ProductsList.FirstOrDefault(p => p.UnitsInStock > 0 && p.UnitPrice > 3);



            #endregion


            #region  Returns digits whose name is shorter than their value.
            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var shortNamedDigits = Arr.Where((digit, index) => digit.Length < index);

            #endregion


            #endregion


            #region LINQ - Element Operators

            #region  Get first Product out of Stock 

            var firstOutOfStockProduct = ProductsList.FirstOrDefault(p => p.UnitsInStock == 0);


            #endregion


            #region Return the first product whose Price > 1000, unless there is no match, in which case null is returned.

            var expensiveProduct = ProductsList.FirstOrDefault(p => p.UnitPrice > 1000);

            #endregion

            #region  Retrieve the second number greater than 5 
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var secondNumberGreaterThanFive = Arr2.Where(n => n > 5).Skip(1).FirstOrDefault();
            #endregion

            #endregion

            #region LINQ - Aggregate Operators

            #region Uses Count to get the number of odd numbers in the array

            int[] Arr3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var oddCount = Arr3.Count(n => n % 2 != 0);

            #endregion

            #region  Return a list of customers and how many orders each has.

            var listOfCustomers = CustomersList.Select(c => new
            {
                c.CustomerName,
                OrderCount  = c.Orders.Count()
            });


            #endregion

            #region Return a list of categories and how many products each has

            var listOfCategories = ProductsList.Select(p => new
            {
                Category = p.Category ,
                NumOfProduct = ProductsList.Count(c=>c.Category == p.Category)

            });

            #endregion


            #region  Get the total of the numbers in an array.

            int[] Arr4 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var totalSum = Arr4.Sum();


            #endregion

            #region Get the total number of characters of all words in dictionary_english.txt(Read dictionary_english.txt into Array of String First)

            //var words = File.ReadAllLines("dictionary_english.txt");

            //var totalWords = words.Sum(w => w.Length);





            #endregion

            #region Get the length of the shortest word in dictionary_english.txt(Read dictionary_english.txt into Array of String First).

            //var words = File.ReadAllLines("dictionary_english.txt");
            //var shortWord = words.Min(s => s.Length);

            #endregion

            #region Get the length of the longest word in dictionary_english.txt(Read dictionary_english.txt into Array of String First).

            //var words = File.ReadAllLines("dictionary_english.txt");
            //var LongWord = words.Max(s => s.Length);

            #endregion

            #region Get the length of the Average word in dictionary_english.txt(Read dictionary_english.txt into Array of String First).

            var words = File.ReadAllLines("dictionary_english.txt");
            var AverageWord = words.Average(s => s.Length);


            #endregion

            #region Get the total units in stock for each product category.
           
            var totalUnitsOfStock = ProductsList.GroupBy(p =>p.Category)
                                    .Select(p=> new
                                    {
                                        category = p.Key,
                                        UnitsInStock = p.Sum(S => S.UnitsInStock)
                                    } );

            #endregion



            #region Get the cheapest price among each category's products

            var CheapestPrice = ProductsList.GroupBy(p => p.Category)
                                            .Select(p => new
                                            { 
                                                category = p.Key,
                                                Price = p.Min( m => m.UnitPrice )
                                            
                                            });

            #endregion

            #region Get the most expensive price among each category's products.


            var ExpensivePrice = ProductsList.GroupBy(p => p.Category)
                                            .Select(p => new
                                            {
                                                category = p.Key,
                                                Price = p.Max(m => m.UnitPrice)

                                            });

            #endregion

            #region Get the average price of each category's products.

            var AveragePrice = ProductsList.GroupBy(p => p.Category)
                                            .Select(p => new
                                            {
                                                category = p.Key,
                                                Price = p.Average(m => m.UnitPrice)

                                            });


            #endregion











            #endregion


            #region LINQ - Ordering Operators

            #region Sort a list of products by name
            var sortedProductsByName = ProductsList.OrderBy(p => p.ProductName).ToList();
            #endregion

            #region Uses a custom comparer to do a case - insensitive sort of the word

            string[] word = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var wordVariations = words.Select(w => new { Upper = w.ToUpper(), Lower = w.ToLower() }).ToList();


            #endregion

            #region Sort a list of products by units in stock from highest to lowest.
           
            var sortedProducts = ProductsList.OrderByDescending(p => p.UnitsInStock).ToList();

            #endregion

            #region Sort a list of digits, first by length of their name, and then alphabetically by the name itself.
           
            string[] Arr5 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            var sortedArr = Arr5
                .OrderBy(word => word.Length)                       
                .ThenBy(word => word, StringComparer.OrdinalIgnoreCase)  
                .ToArray();

            #endregion

            #region Sort a list of products, first by category, and then by unit price, from highest to lowest.

            var sortedListProducts = ProductsList
            .OrderBy(p => p.Category)            // First, sort by category
            .ThenByDescending(p => p.UnitPrice)  // Then, sort by unit price descending
            .ToList();

            #endregion


            #region Sort first by-word length and then by a case -insensitive descending sort of the words in an array

            string[] Arr6 = {"aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry"};

            var sortedArr2 = Arr6
                .OrderBy(word => word.Length)                        // First, sort by word length
                .ThenByDescending(word => word, StringComparer.OrdinalIgnoreCase)  // Then, sort case-insensitively in descending order
                .ToArray();

            #endregion

            #region Create a list of all digits in the array whose second letter is 'i' that is reversed from the  order in the original array.

            string[] Arr7 = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"};

            var ListAllDigits = Arr7
           .Where(word => word.Length > 1 && word[1] == 'i')  // Filter words where the second letter is 'i'
           .Reverse()                                         // Reverse the order of the filtered words
           .ToList();
            #endregion





            #endregion

            #region LINQ – Transformation Operators

            #region Return a sequence of just the names of a list of products.

            var productNames = ProductsList.Select(p => p.ProductName).ToList();

            #endregion

            #region Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
           
            string[] wordUpp = { "aPPLE", "BlUeBeRrY", "cHeRry" };

            var Wordsequence = words.Select(word => new
            {
                Uppercase = word.ToUpper(),
                Lowercase = word.ToLower()
            });

            #endregion

            #region Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.

            var productDetails = ProductsList.Select(p => new
            {
                p.ProductName,
                Price = p.UnitPrice,  
                p.Category
            });

            #endregion


            #region Determine if the value of int in an array matches their position in the array.

            //int[] Arr8 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //var matches = Arr.Select((value, index) => new { Value = value, Index = index, Match = value == index });
            #endregion

            #region Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.

            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            var pairs = from a in numbersA
                        from b in numbersB
                        where a < b
                        select new { NumberA = a, NumberB = b };

            #endregion


            #endregion

            #region LINQ - Set Operators
           
            #region Find the unique Category names from Product List

            var uniqueCategory = ProductsList.Select(p => p.Category).Distinct();

            #endregion

            #region Produce a Sequence containing the unique first letter from both product and customer names

            var uniqueFirstletter = ProductsList.Select(p => p.ProductName[0]).Union(CustomersList.Select(c => c.CustomerName[0])).Distinct();

            #endregion

            #region Create one sequence that contains the common first letter from both product and customer names

            var CommonFirstLetter = ProductsList.Select(p => p.ProductName[0]).Intersect(CustomersList.Select(c => c.CustomerName[0]));

            #endregion

            #region Create one sequence that contains the first letters of product names that are not also first letters of customer names

            var firstLetters = ProductsList.Select(p => p.ProductName[0]).Except(CustomersList.Select(c => c.CustomerName[0]));

            #endregion

            #region Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates

            var lastThreeCharacters = ProductsList.Select(p => p.ProductName[^3..]).Concat(CustomersList.Select(c => c.CustomerName[^3..]));

            #endregion


            #endregion


            #region LINQ - Partitioning Operators

            #region Get the first 3 orders from customers in Washington

            var First3OrdersInWashington = CustomersList.Where(c => c.Region == "WA").SelectMany(o => o.Orders).Take(3);

            #endregion

            #region Get all but the first 2 orders from customers in Washington.

            var AllBut2FirstOrdersInWashington = CustomersList.Where(c => c.Region == "WA").SelectMany(o => o.Orders).Skip(2);


            #endregion

            #region Return elements starting from the beginning of the array until a number is hit that is less than its position in the array

            //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //var Result = numbers.TakeWhile((n, i) => n >= i);

            #endregion

            #region Get the elements of the array starting from the first element divisible by 3

            //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //var Result = numbers.SkipWhile(n => n % 3 != 0);

            #endregion

            #region Get the elements of the array starting from the first element less than its position.

            //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //var Result = numbers.SkipWhile((n, i) => n >= i);

            #endregion
            #endregion

            #region LINQ - Quantifiers

            #region Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.

            var wordsubstring = File.ReadAllLines("dictionary_english.txt");

            var substring = words.Any(w => w.Contains("ei"));

            #endregion

            #region Return a grouped a list of products only for categories that have at least one product that is out of stock

            var groupeListProducts = ProductsList.GroupBy(p => p.Category).Where(c => c.Any(p => p.UnitsInStock == 0)).Select(p => p);

            #endregion

            #region  Return a grouped a list of products only for categories that have all of their products in stock.

            var groupeListProductOnlyCategories = ProductsList.GroupBy(p => p.Category).Where(c => c.All(p => p.UnitsInStock > 0)).Select(p => p);

            #endregion
            #endregion

            #region LINQ – Grouping Operators

            #region Use group by to partition a list of numbers by their remainder when divided by 5

            //List<int> numbers = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            //var partitionList  = numbers.GroupBy(n => n % 5);

            //foreach (var item in partitionList)
            //{
            //    Console.WriteLine($"Numbers with Remainder of {item.Key} when devided by 5:");
            //    foreach (var item1 in item)
            //    {
            //        Console.WriteLine(item1);
            //    }
            //}

            #endregion

            #region Uses group by to partition a list of words by their first letter. Use dictionary_english.txt for Input

            //var partitionWords = File.ReadAllLines("dictionary_english.txt");

            //var partitionList = words.GroupBy(w => w[0]);

            #endregion

            #region Use Group By with a custom comparer that matches words that are consists of the same Characters Together

            string[] Arr10 = { "from", "salt", "earn", " last", "near", "form" };

            var MatchWord = Arr10.GroupBy(w => w.Trim(), new EqualityComparer());

            foreach (var item in MatchWord)
            {
                foreach (var item1 in item)
                {
                    Console.WriteLine(item1);
                }
                Console.WriteLine("---------------");
            }

            #endregion 

            #endregion

        


        }

    }
}
