using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace OriginalCode
{
    public class Word
    {
        public string word = null;
        public int length = 0;
        public int index = 0;
        public char word_head = ' ';
        public char word_tail = ' ';

        public Word(string w, int i)
        {
            word = w;
            length = w.Length;
            word_head = w[0];
            word_tail = w[length - 1];
            index = i;
        }

        override
        public string ToString()
        {
            return word;
        }

    }
}
