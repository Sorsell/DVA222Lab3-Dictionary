using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace HashDictionary
{
    class Program
    {
        static uint FowlerNollVo(string str, uint h) {
            foreach(char ch in str)
                h = (31*h) ^ (uint)ch;
            return h;
        }

        static void Main()
        {
            //data structure to test
            var cities = new HashDictionary<string, int> ();
            //read input file   
            string[] lines = System.IO.File.ReadAllLines(@"cities.txt");
            //duplicates info collected during the insertion phase
            uint duplicates_hash = 0;
            int duplicates_counter = 0;
            //insertion phase
            foreach (string line in lines) 
            {
                string[] words = line.Split('\t');
                if (words.Length == 2)
                    try {
                        cities.Add(words[0], Convert.ToInt32(words[1]));
                    } catch (Exception) {
                        duplicates_counter += 1;
                        duplicates_hash = FowlerNollVo(words[0], duplicates_hash);
                    }
            }
            //print result
            Console.WriteLine("{0} of {1} cities added: {2} duplicates skipped [hash={3}]", cities.Count, lines.Length, duplicates_counter, duplicates_hash);
            //copy the set of collection keys into an array and add NoWhere, finally sort the array
            string[] keys = new string[cities.Count+1];
            keys[cities.Count] = "NoWhere";
            cities.Keys.CopyTo(keys, 0);
            Array.Sort(keys);
            //print out cities with 110000 inhabitants
            foreach(var key in keys)
                try {
                    if(cities[key] == 110000)
                    Console.WriteLine("'{0}' has exactly 110k inhabitants.", key);
                } catch (Exception) {
                    Console.WriteLine("'{0}' does not exist.", key);
                }
            //update the number of inhabitants of the first city
            try {
                cities[keys[0]] = 123121;
                Console.WriteLine("Now '{0}' has {1} inhabitants.", keys[0], cities[keys[0]]);
            } catch (Exception) {
                Console.WriteLine("Exception caught looking for '{0}'.", keys[0]);
            }
            //add NoWhere in cities
            try {
                cities["NoWhere"] = 123326;
                Console.WriteLine("Now 'NoWhere' has {0} inhabitants.", cities["NoWhere"]);
            } catch (Exception) {
                Console.WriteLine("Exception caught looking for 'NoWhere'.");
            }
            //remove Cajamarca
            try {
                cities.Remove("Cajamarca");
                Console.WriteLine("'Cajamarca' has been removed");
            } catch (Exception) {
                Console.WriteLine("Exception caught removing 'Cajamarca'.");
            }
            //count the overall number of inhabitants
            long total = 0;
            foreach(var value in cities.Values)
                total += value;
            Console.WriteLine("The overall number of inhabitants is {0}.", total);
            //remove NoWhere
            cities.Remove("NoWhere");
            Console.WriteLine("Looking for 'NoWhere' after deletion: found = {0}.", cities.ContainsKey("NoWhere"));
            //create a copy the first pair then query
            var firstpair = new KeyValuePair<string, int>(keys[0], cities[keys[0]]);
            Console.WriteLine("Collection contains '{0}' = {1}.", firstpair.Key, cities.Contains(firstpair));
            Console.WriteLine("Collection contains ('{0}',{1}) = {2}.", firstpair.Key, firstpair.Value, cities.Contains(firstpair));
            //create a wrong-copy the first pair and half inhabitants then query
            var nonexistingpair = new KeyValuePair<string, int>(keys[0], cities[keys[0]]/2);
            Console.WriteLine("Collection contains ('{0}',{1}) = {2}.", nonexistingpair.Key, nonexistingpair.Value, cities.Contains(nonexistingpair));
            Console.WriteLine("Remove('{0}',{1}) returned {2}.", nonexistingpair.Key, nonexistingpair.Value, cities.Remove(nonexistingpair));
            //remove the first pair from the collection then try to get the value
            int v = firstpair.Value;
            Console.WriteLine("Remove('{0}',{1}) returned {2}. Trying to get '{0}' after deletion returned {3} and value = {4}.", firstpair.Key, firstpair.Value, cities.Remove(firstpair), cities.TryGetValue(firstpair.Key, out v), v);
            //try to copy the collection in a null array
            KeyValuePair<string, int>[] copy = null;
            try {
                cities.CopyTo(copy, 0);
                Console.WriteLine("Coping collection: {0} pairs copied.", cities.Count);
            } catch (Exception) {
                Console.WriteLine("Exception caught copying collection (null destination).");
            }
            //copy the collection in an array
            copy = new KeyValuePair<string, int>[cities.Count];
            try {
                cities.CopyTo(copy, 0);
                Console.WriteLine("Coping collection: {0} pairs copied.", cities.Count);
            } catch (Exception) {
                Console.WriteLine("Exception caught copying collection.");
            }
            //check that every city is in the array
            try {
                for(int i=0; i<copy.Length; i++)
                    if(!cities.ContainsKey(copy[i].Key))
                        Console.WriteLine("Element '{0}' missing in keys[] after copy!", copy[i].Key);
                for(int i=0; i<copy.Length; i++)
                    if(cities[copy[i].Key]!=copy[i].Value)
                        Console.WriteLine("The number of inhabitants for '{0}' is wrong!", copy[i].Key);
            } catch (Exception) {
                Console.WriteLine("Exception checking the copy of collection.");
            }
            //try to copy the collection in the array from position 1 (it shouldn't be enough space)
            try {
                cities.CopyTo(copy, 1);
                Console.WriteLine("Coping collection: {0} pairs copied.", cities.Count);
            } catch (Exception) {
                Console.WriteLine("Exception caught copying collection (not enough space).");
            }
            //clear collection
            cities.Clear();
            Console.WriteLine("Collection cleared! There are {0} cities now.", cities.Count);
            //add first pair to the empty collection
            cities.Add(firstpair);
            try {
                Console.WriteLine("'{0}' has {1} inhabitants.", firstpair.Key, cities[firstpair.Key]);
            } catch (Exception) {
                Console.WriteLine("Exception caught looking for {0}.", firstpair.Key);
            }
            //end test
        }   
    }
}

/*** EXPECTED OUTPUT: ***

4209 of 4296 cities added: 86 duplicates skipped [hash=3408895133]
'Kufa' has exactly 110k inhabitants.
'Mendip' has exactly 110k inhabitants.
'NoWhere' does not exist.
'Noyabrsk' has exactly 110k inhabitants.
'Ressano Garcia' has exactly 110k inhabitants.
'Vacoas' has exactly 110k inhabitants.
Now 'Aachen' has 123121 inhabitants.
Now 'NoWhere' has 123326 inhabitants.
'Cajamarca' has been removed
The overall number of inhabitants is 1916730000.
Looking for 'NoWhere' after deletion: found = False.
Collection contains 'Aachen' = True.
Collection contains ('Aachen',123121) = True.
Collection contains ('Aachen',61560) = False.
Remove('Aachen',61560) returned False.
Remove('Aachen',123121) returned True. Trying to get 'Aachen' after deletion returned False and value = 0.
Exception caught copying collection (null destination).
Coping collection: 4207 pairs copied.
Exception caught copying collection (not enough space).
Collection cleared! There are 0 cities now.
'Aachen' has 123121 inhabitants.

*************************/
