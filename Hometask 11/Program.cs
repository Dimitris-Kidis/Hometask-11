using System;

// 1. Create one or more sequences and use one operator of each type:
//     - filtering ✅
//     - projection ✅
//     - joining ✅
//     - ordering ✅
//     - grouping ✅
//     - set operators ✅
//     - conversion methods ✅
//     - element operators ✅
//     - aggregation methods ✅
//     - quantifiers ✅
//     - generation methods ✅
// 2. Use closures 

namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {

            // Declaring Sequences
            var list = new List<Person>
            {
                new Person(0, "John", "Smith", "Baker St. 221b", "London", "01-01-2000", "+2 453 544 553", "M"),
                new Person(1, "Emma", "Doe", "Lupu St. 52", "Moscow", "05-07-1997", "+2 453 544 553", "F"),
                new Person(2, "Liam", "Jones", "Bucuresti St. 84", "Kiev", "14-03-1988", "+2 453 544 553", "M"),
                new Person(3, "Amelia", "Williams", "Armeneasca St. 93", "Chisinau", "07-12-2009", "+2 453 544 553", "F"),
                new Person(4, "Oliver", "Brown", "Creanga St. 11", "Havana", "11-04-2008", "+2 453 544 553", "M"),
                new Person(5, "Mia", "Johnson", "31 August St. 32", "Cairo", "25-06-2004", "+2 453 544 553", "F"),
                new Person(6, "James", "Taylor", "Columna St. 6", "Chisinau", "09-01-1998", "+2 453 544 553", "M"),
                new Person(7, "Sophia", "Evans", "Siusev St. 97", "Chisinau", "29-07-1995", "+2 453 544 553", "F"),
                new Person(8, "William", "Wilson", "Puskin St. 53", "London", "12-09-2001", "+2 453 544 553", "M"),
                new Person(9, "Bella", "Roberts", "Matievici St. 32", "Chisinau", "19-04-2002", "+2 453 544 553", "F"),
                new Person(10, "Benjamin", "Thomas", "Izvor St. 2", "Rome", "02-11-1978", "+2 453 544 553", "M")
            };

            var intSequence = new int[] { 65, 342, 2, 8, 27, 8, 1, 0, 33, 33, 43, 2, 53 }; // length = 13

            // Filtering & Order
            var theOldestPersonOver18 = list
                                            .Where(x => x.Age > 18 && x.ID > 5)
                                            .OrderBy(x => x.Age)
                                            .Reverse()
                                            .Take(1);
            Console.WriteLine("----- The Oldest Person Over 18 -----");
            foreach (var person in theOldestPersonOver18)
            {
                Console.WriteLine(person.ToString());
            }

            var filteredSeq = intSequence
                                    .Skip(3)
                                    .Distinct()
                                    .SkipWhile(x => x < 30)
                                    .OrderBy(x => x)
                                    .Reverse()
                                    .TakeWhile(x => x > 40);

            var filteredSeq1 = list.Where(x => x.Gender == "M" && x.Age > 20);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Filtered Int Sequence -----");
            foreach (var item in filteredSeq)
            {
                Console.Write(item + " ");
            }

            // Projection


            var allTheAddresses = list.Select(x => x.Address);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n----- All The Addresses -----");
            foreach (var item in allTheAddresses)
            {
                Console.WriteLine(item + " ");
            }

            int[][] IDs =
            {
                new[] { 1, 5, 7, 9},
                new[] { 2, 4, 8},
                new[] { 3, 6}
            };

            var fullIDsList = IDs.SelectMany(x => x).OrderBy(x => x);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Flatten Full IDs List -----");
            foreach (var item in fullIDsList)
            {
                Console.Write(item + " ");
            }


            // Joining & COnversion Method

            List<Card> cardList = new List<Card>
            {
                new Card("James", "33-53-97-21"),
                new Card("Amelia", "22-41-99-53"),
                new Card("Mia", "89-37-38-22"),
                new Card("Benjamin", "90-76-73-13"),
            };
            var joinQueryLINQ = list.Join(cardList,
                                      person => person.FirstName,
                                      card => card.Owner,
                                      (person, card) => new { OwnerName = person.FirstName, Card = card.Number });

            var sqlStyle = from person in list
                            join card in cardList on person.FirstName equals card.Owner
                            select new { person.ID, OwnerName = person.FirstName, Card = card.Number, person.BirthDate };
            sqlStyle.ToList();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n----- Joining the Cards And the Owners -----");
            foreach (var item in sqlStyle)
            {
                Console.WriteLine(item + " ");
            }

            // Grouping & Conversion Method

            var groupByExample = from person in list
                                 group person by person.City into g
                                 select new { City = g.Key, Count = g};

            groupByExample = groupByExample.ToList();
            var group = list.GroupBy(x => x.City);


            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Group People By City -----");

            foreach (var item in groupByExample)
            {
                Console.WriteLine(item + " ");
            }

            // Set Operations
            Person personTest = new Person(12, "Roger", "Siemens", "Izvor St. 2", "Rome", "02-11-1978", "+2 453 544 553", "M");
            var tmpList = new List<Person>
            {
                new Person(11, "Rita", "Siegel", "Matievici St. 32", "Chisinau", "19-04-2002", "+2 453 544 553", "F"),
                personTest,
                personTest
            };

            // Concat
            var resultConcatList = list.Concat(tmpList);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----- Concatination of Two Lists -----");

            foreach (var item in resultConcatList)
            {
                Console.WriteLine(item + " ");
            }

            // Union
            var resultUnionList = list.Union(tmpList);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Union of Two Lists -----");

            foreach (var item in resultUnionList)
            {
                Console.WriteLine(item + " ");
            }

            // Intersect
            list.Add(personTest);
            var resultIntersectList = list.Intersect(tmpList);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----- Intersection of Two Lists -----");

            foreach (var item in resultIntersectList)
            {
                Console.WriteLine(item + " ");
            }

            // Except
            var resultExceptList = list.Except(tmpList);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Except of Two Lists -----");

            foreach (var item in resultExceptList)
            {
                Console.WriteLine(item + " ");
            }

            // Element Operators

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----- First & Last -----");
            Console.WriteLine("First element:\n{0}.\nLast element:\n{1}.", list.First(), list.Last());

            // Aggregation Methods
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Average Age -----");
            var AverageAge = list.Average(x => x.Age);
            Console.WriteLine(AverageAge.ToString());

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----- Min & Max Age -----");
            Console.WriteLine("Min: " + list.Min(x => x.Age).ToString() + ". Max: " + list.Max(x => x.Age).ToString());


            // Quantifiers
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Check if there's a woman between 20 and 40 -----");
            Console.WriteLine("Answer: " + list.Any(x => x.Age > 20 && x.Age < 40 && x.Gender == "F"));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n----- Check if everyone on the list is over 18 -----");
            Console.WriteLine("Answer: " + list.All(x => x.Age > 18));

            // Generation Methods
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n----- Create 10 Students & take the oldest 3 -----");
            var newRandomList = Enumerable.Repeat(new Student { ID = new Random().Next(0, 100), Age = new Random().Next(18, 24) }, 10);
            
        }
    }




    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string BirthDate { get; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - Int32.Parse(BirthDate.Substring(6, 4));
            }
        }
        public Person(int iD, string firstName, string lastName, string address, string city, string birthDate, string phoneNumber, string gender)
        {
            ID = iD;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            City = city;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        public override string ToString()
        {
            return $"[{ID,2}]: {FirstName,10} {LastName,10}, {BirthDate,10}, from {Address}, {City}. ({PhoneNumber})";
        }
    }

    public class Card
    {
        public string Owner { get; set; }

        public string Number { get; set; }

        public Card (string Owner, string Number)
        {
            this.Owner = Owner;
            this.Number = Number;
        }
    }

    public class Student
    {
        public int ID { get; set; }   
        public int Age { get; set; }
        public override string ToString()
        {
            return "ID: " + ID + ". Age: " + Age;
        }
    }

}