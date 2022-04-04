using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Core
{
    public class Word
    {
        public static char type = '0';
        public string word = null;
        public int length = 0;
        public int weight = 0;
        public int index = 0;
        public char word_head = '0';
        public char word_tail = '0';

        public Word(string w, int i)
        {
            word = w;
            length = w.Length;
            word_head = w[0];
            word_tail = w[length - 1];
            index = i;
            
            if (type == 'c')
            {
                weight = length;
            } else
            {
                weight = 1;
            }
            //Console.WriteLine(w + " " + weight.ToString());
        }


        override
        public string ToString()
        {
            return this.word;
        }

    }
}
