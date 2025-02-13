﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }
        static void loadfunc(string filename) 
        {
            filename = $"..\\..\\..\\dict\\{filename}";
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!         
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line.ToLower());
                        dictionary.Add(gloss);
                        line = sr.ReadLine();
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"File not found: {e.FileName}");
            }
        }
        static void translatefunc(string input) 
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == input)
                    Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                if (gloss.word_eng == input)
                    Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
            }
        }
        static void deletefunc(string swedish, string english) 
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe.ToLower() == swedish.ToLower() && gloss.word_eng.ToLower() == english.ToLower())
                    index = i;
            }
            dictionary.RemoveAt(index);
        }
        static string askSwe() 
        {
            Console.WriteLine("Write word in Swedish: ");
            string swedish = Console.ReadLine();
            return swedish.ToLower();
        }
        static string askEng() 
        {
            Console.Write("Write word in English: ");
            string english = Console.ReadLine();
            return english.ToLower();
        }
       
        static void Main(string[] args)
        {
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            dictionary = new List<SweEngGloss>();
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0].ToLower();

                switch (command) 
                {
                    case "quit": 
                    { 
                            Console.WriteLine("Goodbye!");
                            Environment.Exit(0); 
                            break; 
                    }
                    case "load": 
                    { 
                            if (argument.Length == 2)
                            {
                                loadfunc(argument[1]);
                            } 
                            else if (argument.Length == 1)
                            { 
                                loadfunc(defaultFile); 
                            }
                            
                            break; 
                    }
                    case "list": 
                    {

                            foreach (SweEngGloss gloss in dictionary)
                            {
                                Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                            }

                            break; 
                    }
                    case "new": 
                    {
                            if (argument.Length == 3)
                            {
                                dictionary.Add(new SweEngGloss(argument[1].ToLower(), argument[2].ToLower()));
                            }
                            else if (argument.Length == 1)
                            {
                                dictionary.Add(new SweEngGloss(askSwe(), askEng()));
                            }  

                            break; 
                    }
                    case "delete": 
                    {
                            if (argument.Length == 3)
                            {
                                deletefunc(argument[1], argument[2]);
                            }
                            else if (argument.Length == 1)
                            {
                                deletefunc(askSwe(), askEng());
                            }

                            break; 
                    }
                    case "translate": 
                    {
                            if (argument.Length == 2)
                            {
                                translatefunc(argument[1]);
                            }
                            else if (argument.Length == 1)
                            {
                                Console.WriteLine("Write word to be translated: ");
                                string wordToBeTranslated = Console.ReadLine();
                                translatefunc(wordToBeTranslated.ToLower());
                            }

                            break; 
                    }
                    case "help": 
                    {
                            Console.WriteLine("The command recognised by this app and how they work are as follows:");
                            Console.WriteLine("quit: Will exit the application");
                            Console.WriteLine("load: Will load the from file of your choosing or the defaul path. Example: 'load computing.lis'");
                            Console.WriteLine("list: Will list all of content in the apps current dictionary");
                            Console.WriteLine("new: Lets you add new words to the dictionary by either typing 'new' and following the promts or by Example: 'new kaka Cookie'");
                            Console.WriteLine("delete: Lets you delete words from the dictionary by either typing 'delete' and following the promts or by Example: 'delete kaka Cookie'\" ");
                            Console.WriteLine("translate: Lets you type a word of your choosing in either swedish or english and translates it for you if the translation is the the apps dicitionary. Example 'translate kaka'");
                            break;
                    }
                    default: { Console.WriteLine($"Unknown command: '{command}'"); break; }
                }
            }
            while (true);
        }
    }
}