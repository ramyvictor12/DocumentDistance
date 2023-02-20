using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


using System.Text.RegularExpressions;

namespace DocumentDistance
{
    class DocDistance
    {
        // *****************************************
        // DON'T CHANGE CLASS OR FUNCTION NAME
        // YOU CAN ADD FUNCTIONS IF YOU NEED TO
        // *****************************************
        /// <summary>
        /// Write an efficient algorithm to calculate the distance between two documents
        /// </summary>
        /// <param name="doc1FilePath">File path of 1st document</param>
        /// <param name="doc2FilePath">File path of 2nd document</param>
        /// <returns>The angle (in degree) between the 2 documents</returns>

        public static Dictionary<string, long> splitWords(string txt)
        {
            //we will define the alphanumirec char in regex


            Dictionary<string, long> dic = new Dictionary<string, long>();
            Regex rgx = new Regex("[^A-Za-z0-9]", RegexOptions.Compiled);
            //replace the seprators with space

            string s = rgx.Replace(txt, " ");
            // we store this words in array of string
            string[] word_arr = s.Split(' ');
            foreach (string word in word_arr)
            {
                if (dic.ContainsKey(word))
                {
                    dic[word]++;
                }
                else if (!word.Equals(""))
                {
                    dic.Add(word, 1);
                }
            }
            return dic;
        }
        public static double InnerProduct(Dictionary<string, long> dic1, Dictionary<string, long> dic2)
        {
            double result = 0;
            //search if the word is exist in the other document 
            foreach (string word in dic1.Keys)
            {
                if (dic2.ContainsKey(word))
                {
                    result += (dic1[word] * dic2[word]);
                }
            }
            return result;
        }
        public static double CalculateDistance(string doc1FilePath, string doc2FilePath)
        {
            // TODO comment the following line THEN fill your code here
            //  throw new NotImplementedException();
            //read the files as strings
            if (doc1FilePath == doc2FilePath)
                return 0;
            else
            {
                string txt1 = File.ReadAllText(doc1FilePath).ToLower();
                string txt2 = File.ReadAllText(doc2FilePath).ToLower();
                //since we want to split each document into word we will split it in function
                //same as in inner product 
                Dictionary<string, long> dic1 = splitWords(txt1);
                Dictionary<string, long> dic2 = splitWords(txt2);

                // now we calculate the distance
                double Innerpro = InnerProduct(dic1, dic2);
                double Innerpro1 = InnerProduct(dic1, dic1);
                double Innerpro2 = InnerProduct(dic2, dic2);
                double root = Math.Sqrt(Innerpro1 * Innerpro2);



                double dist = Math.Acos(Innerpro / root);
                return dist * (180 / Math.PI);
            }
        }
    }
}