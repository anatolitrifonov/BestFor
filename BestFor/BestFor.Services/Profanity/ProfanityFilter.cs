using System.Collections.Generic;
using System.Text.RegularExpressions;
using BestFor.Domain.Entities;

namespace BestFor.Services.Profanity
{
    public class ProfanityFilter
    {
        //private static Dictionary<string, string> _data = new Dictionary<string, string>
        //{
        //    ["a"] = "[aA@]",
        //    ["b"] = "[b B I3 l3 i3]",
        //    ["c"] = "(?:[c C (]|[k K])",
        //    ["d"] = "[d D]",
        //    ["e"] = "[e E 3]",
        //    ["f"] = "(?:[f F]|[ph pH Ph PH])",
        //    ["g"] = "[g G 6]",
        //    ["h"] = "[h H]",
        //    ["i"] = "[i I l ! 1]",
        //    ["j"] = "[j J]",
        //    ["k"] = "(?:[c C (]|[k K])",
        //    ["l"] = "[l L 1 ! i]",
        //    ["m"] = "[m M]",
        //    ["n"] = "[n N]",
        //    ["o"] = "[o O 0]",
        //    ["p"] = "[p P]",
        //    ["q"] = "[q Q 9]",
        //    ["r"] = "[r R]",
        //    ["s"] = "[s S $ 5]",
        //    ["t"] = "[t T 7]",
        //    ["u"] = "[u U v V]",
        //    ["v"] = "[v V u U]",
        //    ["w"] = "[w W vv VV]",
        //    ["x"] = "[x X]",
        //    ["y"] = "[y Y]",
        //    ["z"] = "[z Z 2]"
        //};
        public static bool IsAlphaNumeric(string strToCheck)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");
            return rg.IsMatch(strToCheck);
        }

        /// <summary>
        /// Checks if line has unprintable characters.
        /// Linebreaks are allowed.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool AllCharactersAllowed(string data)
        {
            // \x2022 is a unicode bullet point
            Regex r = new Regex("[^\x20-\x7e\r\n\t\x2022\xa1-\x10fffd]");
            // var matches = r.Matches(data);
            return !r.IsMatch(data);
        }

        public static string FirstDisallowedCharacter(string data)
        {
            // \x2022 is a unicode bullet point
            Regex r = new Regex("[^\x20-\x7e\r\n\t\x2022\xa1-\x10fffd]");
            var matches = r.Matches(data);
            if (matches.Count > 0)
                return matches[0].Value;
            return null;
        }

        public static string CleanupData(string input)
        {
            // first level cleanup. Change C0m to com. D!nk to dink.
            // TODO speed this up
            //var result = input;
            //foreach(var key in _data.Keys)
            //    result = Regex.Replace(result, _data[key], key);
            //return result;
            if (string.IsNullOrEmpty(input)) return input;
            if (string.IsNullOrWhiteSpace(input)) return input;
            // For now just to lower. Regular expressions still do not work
            return input.ToLower();
        }

        public static string GetProfanity(string input, IEnumerable<BadWord> badwords)
        {
            var localInput = CleanupData(input);
            if (localInput == null) return null;
            foreach (var word in badwords)
            {
                if (localInput.EndsWith(" " + word.Phrase)) return word.Phrase;
                if (localInput.EndsWith(word.Phrase + ".")) return word.Phrase;
                if (localInput.StartsWith(word.Phrase + " ")) return word.Phrase;
                if (localInput.Contains(" " + word.Phrase + " ")) return word.Phrase;
                if (localInput == word.Phrase) return word.Phrase;
                if (localInput + "." == word.Phrase) return word.Phrase;

                var result = CheckContains(localInput, word.Phrase, " `~!@#$%^&*()-_+=[{]};:'\"<>/?,.№");
                if (result != null)
                    return result;
            }
            return null;
        }

        public static string CheckContains(string input, string badWord, string charsToCheck)
        {
            var chars = charsToCheck.ToCharArray();
            foreach (var c in chars)
                foreach (var c1 in chars)
                {
                    var check = c + badWord + c1;
                    if (input.Contains(check))
                        return c + badWord + c1;
                }
            return null;
        }

        //<?php

        ///**
        // * @author unkwntech@unkwndesign.com
        // **/

        //if($_GET['act'] == 'do')
        // {
        //    $pattern['a'] = '/[a]/'; $replace['a'] = '[a A @]';
        //    $pattern['b'] = '/[b]/'; $replace['b'] = '[b B I3 l3 i3]';
        //    $pattern['c'] = '/[c]/'; $replace['c'] = '(?:[c C (]|[k K])';
        //    $pattern['d'] = '/[d]/'; $replace['d'] = '[d D]';
        //    $pattern['e'] = '/[e]/'; $replace['e'] = '[e E 3]';
        //    $pattern['f'] = '/[f]/'; $replace['f'] = '(?:[f F]|[ph pH Ph PH])';
        //    $pattern['g'] = '/[g]/'; $replace['g'] = '[g G 6]';
        //    $pattern['h'] = '/[h]/'; $replace['h'] = '[h H]';
        //    $pattern['i'] = '/[i]/'; $replace['i'] = '[i I l ! 1]';
        //    $pattern['j'] = '/[j]/'; $replace['j'] = '[j J]';
        //    $pattern['k'] = '/[k]/'; $replace['k'] = '(?:[c C (]|[k K])';
        //    $pattern['l'] = '/[l]/'; $replace['l'] = '[l L 1 ! i]';
        //    $pattern['m'] = '/[m]/'; $replace['m'] = '[m M]';
        //    $pattern['n'] = '/[n]/'; $replace['n'] = '[n N]';
        //    $pattern['o'] = '/[o]/'; $replace['o'] = '[o O 0]';
        //    $pattern['p'] = '/[p]/'; $replace['p'] = '[p P]';
        //    $pattern['q'] = '/[q]/'; $replace['q'] = '[q Q 9]';
        //    $pattern['r'] = '/[r]/'; $replace['r'] = '[r R]';
        //    $pattern['s'] = '/[s]/'; $replace['s'] = '[s S $ 5]';
        //    $pattern['t'] = '/[t]/'; $replace['t'] = '[t T 7]';
        //    $pattern['u'] = '/[u]/'; $replace['u'] = '[u U v V]';
        //    $pattern['v'] = '/[v]/'; $replace['v'] = '[v V u U]';
        //    $pattern['w'] = '/[w]/'; $replace['w'] = '[w W vv VV]';
        //    $pattern['x'] = '/[x]/'; $replace['x'] = '[x X]';
        //    $pattern['y'] = '/[y]/'; $replace['y'] = '[y Y]';
        //    $pattern['z'] = '/[z]/'; $replace['z'] = '[z Z 2]';
        //    $word = str_split(strtolower($_POST['word']));
        //    $i=0;
        //    while($i<count($word))
        //     {
        //        if(!is_numeric($word[$i]))
        //         {
        //            if($word[$i] != ' ' || count($word[$i]) < '1')
        //             {
        //                $word[$i] = preg_replace($pattern[$word[$i]], $replace[$word[$i]], $word[$i]);
        //    }
        //}
        //        $i++;
        //     }
        //    //$word = "/" . implode('', $word) . "/";
        //    echo implode('', $word);
        // }

        //if($_GET['act'] == 'list')
        // {
        //    $link = mysql_connect('localhost', 'username', 'password', '1');
        //    mysql_select_db('peoples');
        //    $sql = "SELECT word FROM filters";
        //    $result = mysql_query($sql, $link);
        //    $i=0;
        //    while($i<mysql_num_rows($result))
        //     {
        //        echo mysql_result($result, $i, 'word') . "<br />";
        //        $i++;
        //     }
        //     echo '<hr>';
        // }
        //?>
        //<html>
        //    <head>
        //        <title>RegEx Generator</title>
        //    </head>
        //    <body>
        //        <form action = 'badword.php?act=do' method= 'post' >
        //            Word: <input type = 'text' name= 'word' />< br />
        //            < input type= 'submit' value= 'Generate' />
        //        </ form >
        //        < a href= "badword.php?act=list" > List Words</a>
        //    </body>
        //</html>    
    }
}
