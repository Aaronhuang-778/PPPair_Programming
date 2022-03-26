using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OriginalCode
{
    public class Word
    {
        public string word = null;
        public int length = 0;
        public int weight = 0;
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
            
            if (GlobalPara.type == 'c')
            {
                weight = length;
            } else
            {
                weight = 1;
            }
        }

        public override string ToString()
        {
            return this.word;
        }
    }
}
