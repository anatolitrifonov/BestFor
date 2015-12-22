using System.Text.RegularExpressions;

namespace BestFor.Services.Profanity
{
    public class ProfanityFilter
    {
        /// <summary>
        /// Checks if line has unprintable characters.
        /// Linebreaks are allowed.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool AllChractersAllowed(string data)
        {
            Regex r = new Regex("[^\x20-\x7e\r\n\t]");
            var matches = r.Matches(data);
            return !r.IsMatch(data);
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
