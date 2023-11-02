using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace KBurns_Task2.Models
{
    public class Translate
    {
        [Required]
        [Display(Name = "Text in English:")]
        public string EnglishText { get; set; }

        [Display(Name = "Text in Pig Latin:")]
        public string PigLatinText { get; set; }

        //translates to piglatin
        public void TranslationToPigLatin()
        {
            //remove leading and trailing blank spaces
            string englishText = EnglishText.Trim();
            //splits each word defined by a space inbetween them and stores individual string in text array
            string[] text = englishText.Split(' ');

            foreach (string word in text)
            {
                PigLatinText += dealWithCaps(word);
                PigLatinText += " ";
            }
        }

        //deals with character logic
        private string TranslateWord(string word)
        {
            char w = word[0]; //checks first letter
            int characterCount = 1; //keeps track of character count
            //deals with vowels
            if (isAVowel(w) == true)
            {
                word += "way";
            }
            else
            {
                switch (w)
                {
                    case 'y': case 'Y':
                        word = word.Remove(0, 1);
                        word += w.ToString();
                        w = word[0];
                        break;
                }
                //deals with consonants
                while (isAVowel(w) == false && w != 'y' && w != 'Y')
                {
                    word = word.Remove(0, 1);
                    word += w.ToString();
                    w = word[0];
                    if (characterCount >= word.Length) //fixes constant loop
                    {
                        characterCount = word.Length + 1;
                        break;
                    }
                    characterCount++;
                }
                if (characterCount <= word.Length) //ensure untranslated words aren't affected
                {
                    word += "ay";
                }
                characterCount = 1; //reset character count back to 1
            }
            return word;
        }

/*
* ======================================================DEAL WITH PUNCT & CAPS======================================================================================
*/

        private string dealWithPunctuation(string word)
        {
            int wordLen = word.Length;
            string puncAtEnd = word.Substring(wordLen - 1);
            string punctuation = ""; //to reserve punctuation

            switch (puncAtEnd)
            {
                case ".":
                    punctuation = ".";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                case ",":
                    punctuation = ",";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                case "!":
                    punctuation = "!";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                case "?":
                    punctuation = "?";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                case ":":
                    punctuation = ":";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                case ";":
                    punctuation = ";";
                    word = word.Remove(word.Length - 1, 1);
                    break;
                default:
                    puncAtEnd = ""; //resets if no punctuation is found
                    break;
            }
            return punctuation;
        }

        private string dealWithCaps(string word)
        {
            string punctuation = "";
            punctuation = dealWithPunctuation(word);

            //checks to see if punctuation isn't empty before removing char at the end
            if(!punctuation.IsEmpty())
            {
                word = word.Remove(word.Length - 1, 1);
            }
            //translate word and deal with captialisation
            if (FirstLetterCaps(word))
            {
                word = FirstCharToUppercase(TranslateWord(word));
            }
            else if (WordIsUppercase(word))
            {
                word = TranslateWord(word).ToUpper();
            }
            else if (WordIsLowercase(word))
            {
                word = TranslateWord(word).ToLower();
            }

            //place punctuation to the end of new translated word
            word += punctuation;

            return word;
        }
/*
* ======================================================DEAL WITH LETTERS======================================================================================
*/

        //private string ToInitialCaps(string word)//capitalising the first character in a word
        //{
        //    string firstLetter = word.Substring(0, 1).ToUpper();
        //    string otherLetters = word.Substring(1).ToLower();
        //    return firstLetter + otherLetters;
        //}

        private string FirstCharToUppercase(string word)
        {
            string firstLetter = word.Substring(0, 1).ToUpper();
            string restOfTheLetters = word.Substring(1).ToLower();

            return firstLetter + restOfTheLetters;
        }

        //check if character is uppercase
        private bool CharIsUppercase(char Letter)
        {
            //comparing letter to ascii int equivalent for uppercase values
            if (Letter >= 65 && Letter <= 90 )
            {
                return true;
            }
            else if (Letter.ToString() == "'")
            {
                return true;
            }
            return false;
        }

        //checks if character is lowercase
        private bool CharIsLowercase(char Letter)
        {
            //comparing letter to ascii int equivalent for lowercase values
            if (Letter >= 97 && Letter <= 122)
            {
                return true;
            }
            else if (Letter.ToString() == "'")
            {
                return true;
            }
            return false;
        }

        //checks if FULL word is uppercase
        private bool WordIsUppercase(string word)
        {
            int i = 0;
            while (i < word.Length)
            {
                if (CharIsUppercase(word[i]) == false)
                { 
                    return false;
                }
                i++;
            }
            return true;
        }

        //checks if FULL word is lowercase
        private bool WordIsLowercase(string word)
        {
            int i = 0;
            while (i < word.Length)
            {
                if (CharIsLowercase(word[i]) == false)
                {
                    return false;
                }
                i++;
            }
            return true;
        }

        //checks if the first letter is caps
        private bool FirstLetterCaps(string word)
        {
            char firstLetter = word[0];
            string charsAfterFirst = word.Remove(0, 1);
            if (CharIsUppercase(firstLetter) && WordIsLowercase(charsAfterFirst))
            {
                return true;
            }

            return false;
        }
/*
 * ======================================================CHECKS VOWEL======================================================================================
 */
        private bool isAVowel(char w)
        {
            switch (w)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    return true;
            }
            return false;
        }
    }
}