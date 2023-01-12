using System;
using System.Collections.Concurrent;
using Microsoft.VisualBasic;
using System.IO;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;


class ImprovedDictionary<T1, T2>
{
    public List<string> Keys = new List<string>();
    public List<string> Values = new List<string>();

    public void Add(string t1, string t2)
    {
        Keys.Add(t1);
        Values.Add(t2);
    }

    public void changeKey(int i, string s)
    {
        Keys[i] = s;
    }
}

class EnteringInteger
{
    static List<string> sport = new List<string>();
    static List<string> politic = new List<string>();
    static List<string> music = new List<string>();
    static List<string> over = new List<string>();
    static int m = 0;
    static int p = 0;
    static int s = 0;

    static void Main()
    {
        ImprovedDictionary<string, string> dic = new ImprovedDictionary<string, string>();
        StreamReader sr = new StreamReader("D:\\music.txt");
        string word = "";
        char ss;
        while (sr.Peek() != -1)
        {
            ss = (char)sr.Read();
            if ((ss != ' ') && (ss != ',') && (ss != '.') && (ss != '\n') && (ss != '\t') && (ss != ';') && (ss != '-'))
            {
                word += ss;
            }
            else
            {
                music.Add(word.ToLower());
                word = "";
            }
        }

        sr.Close();
        sr = new StreamReader("D:\\sport.txt");
        word = "";
        while (sr.Peek() != -1)
        {
            ss = (char)sr.Read();
            if ((ss != ' ') && (ss != ',') && (ss != '.') && (ss != '\n') && (ss != '\t') && (ss != ';') && (ss != '-'))
            {
                word += ss;
            }
            else
            {
                sport.Add(word.ToLower());
                word = "";
            }
        }

        sr.Close();
        sr = new StreamReader("D:\\politic.txt");
        word = "";
        while (sr.Peek() != -1)
        {
            ss = (char)sr.Read();
            if ((ss != ' ') && (ss != ',') && (ss != '.') && (ss != '\n') && (ss != '\t') && (ss != ';') && (ss != '-'))
            {
                word += ss;
            }
            else
            {
                politic.Add(word.ToLower());
                word = "";
            }
        }

        sr.Close();
        for (int i = 0; i < music.Count; i++)
        {
            word = music[i];
            if ((sport.Contains(word)) && (politic.Contains(word)))
            {
                over.Add(word);
                music.Remove(word);
                sport.Remove(word);
                politic.Remove(word);
            }
        }

        sr = new StreamReader("D:\\статья.txt");
        word = "";
        while (sr.Peek() != -1)
        {
            ss = (char)sr.Read();
            if ((ss != ' ') && (ss != ',') && (ss != '.') && (ss != '\n') && (ss != '\t') && (ss != ';') && (ss != '-'))
            {
                word += ss;
            }
            else
            {
                if (word != "")
                {
                    dic.Add("bad", word.ToLower());
                }

                word = "";
            }
        }

        sr.Close();


        for (int i = 0; i < dic.Values.Count; i++)
        {
            if (over.Contains(dic.Values[i]))
            {
                dic.changeKey(i, "over");
                continue;
            }

            if (politic.Contains(dic.Values[i]))
            {
                dic.changeKey(i, "politic");
                p++;
                continue;
            }

            if (sport.Contains(dic.Values[i]))
            {
                dic.changeKey(i, "sport");
                s++;
                continue;
            }

            if (music.Contains(dic.Values[i]))
            {
                dic.changeKey(i, "music");
                m++;
                continue;
            }
        }

        var watch = Stopwatch.StartNew();
        
        Parallel.ForEach(Partitioner.Create(0, dic.Values.Count), range =>
        {
            for (int i = range.Item1; i < range.Item2; i++)
            {
                if (dic.Keys[i] == "bad")
                {
                    dic.Keys[i] = "work";
                    string w = Work(dic.Values[i]);
                    dic.Values[i] = w;
                }
            }
        });

        // for (int i = 0; i < dic.Values.Count; i++)
        // {
        //     if (dic.Keys[i] == "bad")
        //     {
        //         dic.Keys[i] = "work";
        //         string w = Work(dic.Values[i]);
        //         dic.Values[i] = w;
        //     }
        // }
        
        watch.Stop();
        Console.WriteLine(
            $"The Execution time of the program is {watch.ElapsedMilliseconds}ms");
        
        for (int i = 0; i < dic.Values.Count; i++)
        {
            Console.Write(dic.Values[i] + " ");
        }
    }

    static string Work(string word)
    {
        List<string> ov = new List<string>();
        List<string> mus = new List<string>();
        List<string> sp = new List<string>();
        List<string> polit = new List<string>();
        int i, j, k, l;
        for (i = 0; i < over.Count; i++)
        {
            if (word.Length == over[i].Length)
            {
                ov.Add(over[i]);
            }
        }

        for (i = 0; i < music.Count; i++)
        {
            if (word.Length == music[i].Length)
            {
                mus.Add(music[i]);
            }
        }

        for (i = 0; i < sport.Count; i++)
        {
            if (word.Length == sport[i].Length)
            {
                sp.Add(sport[i]);
            }
        }

        for (i = 0; i < politic.Count; i++)
        {
            if (word.Length == politic[i].Length)
            {
                polit.Add(politic[i]);
            }
        }

        int h;
        for (i = 1; i <= word.Length; i++)
        {
            k = -1;
            l = -1;
            h = -1;
            for (j = 0; j < ov.Count; j++)
            {
                if (check(i, word, ov[j]))
                {
                    return ov[j];
                }
            }

            for (j = 0; j < mus.Count; j++)
            {
                if (check(i, word, mus[j]))
                {
                    k = j;
                    break;
                }
            }

            for (j = 0; j < sp.Count; j++)
            {
                if (check(i, word, sp[j]))
                {
                    l = j;
                    break;
                }
            }

            for (j = 0; j < polit.Count; j++)
            {
                if (check(i, word, polit[j]))
                {
                    h = j;
                    break;
                }
            }

            if (k >= 0)
            {
                if (l >= 0)
                {
                    if (h >= 0)
                    {
                        if (m == max())
                        {
                            m++;
                            return mus[k];
                        }

                        if (s == max())
                        {
                            s++;
                            return sp[l];
                        }

                        if (p == max())
                        {
                            p++;
                            return polit[h];
                        }
                    }
                    else
                    {
                        if (m == max())
                        {
                            m++;
                            return mus[k];
                        }

                        if (s == max())
                        {
                            s++;
                            return sp[l];
                        }
                    }
                }
                else
                {
                    if (h >= 0)
                    {
                        if (m == max())
                        {
                            m++;
                            return mus[k];
                        }

                        if (p == max())
                        {
                            p++;
                            return polit[h];
                        }
                    }
                    else
                    {
                        m++;
                        return mus[k];
                    }
                }
            }
            else
            {
                if (l >= 0)
                {
                    if (h >= 0)
                    {
                        if (s == max())
                        {
                            s++;
                            return sp[l];
                        }

                        if (p == max())
                        {
                            p++;
                            return polit[h];
                        }
                    }
                    else
                    {
                        if (s == max())
                        {
                            s++;
                            return sp[l];
                        }
                    }
                }
                else
                {
                    if (h >= 0)
                    {
                        p++;
                        return polit[h];
                    }
                }
            }
        }

        return word;
    }

    static bool check(int c, string w1, string w2)
    {
        char[] ch1 = w1.ToCharArray();
        char[] ch2 = w2.ToCharArray();
        int k = 0;
        for (int i = 0; i < ch1.Length; i++)
        {
            if (ch1[i] != ch2[i])
            {
                k++;
            }
        }

        if (k == c)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static int max()
    {
        int max = 0;
        if (m > s)
        {
            max = m;
        }
        else
        {
            max = s;
        }

        if (p > max)
        {
            max = p;
        }

        return max;
    }
}
