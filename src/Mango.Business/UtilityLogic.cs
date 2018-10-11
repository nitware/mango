using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mango.Business
{
    public class UtilityLogic
    {
        public static string JumbbleText(string text)
        {
            try
            {
                string s = null;

                if (!string.IsNullOrWhiteSpace(text))
                {
                    string[] textArray = null;
                    if (text.Length > 0)
                    {
                        textArray = new string[text.Length];
                        for (int i = 0; i < text.Length; i++)
                        {
                            textArray[i] = text.Substring(i, 1);
                        }
                    }



                    string[] newTextArray = new string[textArray.Length];
                    for (int i = 0; i < textArray.Length; i++)
                    {
                        switch (textArray[i])
                        {
                            case "a":
                                {
                                    newTextArray[i] = "b";
                                    break;
                                }
                            case "A":
                                {
                                    newTextArray[i] = "B";
                                    break;
                                }
                            case "b":
                                {
                                    newTextArray[i] = "c";
                                    break;
                                }
                            case "B":
                                {
                                    newTextArray[i] = "C";
                                    break;
                                }
                            case "c":
                                {
                                    newTextArray[i] = "d";
                                    break;
                                }
                            case "C":
                                {
                                    newTextArray[i] = "D";
                                    break;
                                }
                            case "d":
                                {
                                    newTextArray[i] = "e";
                                    break;
                                }
                            case "D":
                                {
                                    newTextArray[i] = "E";
                                    break;
                                }
                            case "e":
                                {
                                    newTextArray[i] = "f";
                                    break;
                                }
                            case "E":
                                {
                                    newTextArray[i] = "F";
                                    break;
                                }
                            case "f":
                                {
                                    newTextArray[i] = "g";
                                    break;
                                }
                            case "F":
                                {
                                    newTextArray[i] = "G";
                                    break;
                                }
                            case "g":
                                {
                                    newTextArray[i] = "h";
                                    break;
                                }
                            case "G":
                                {
                                    newTextArray[i] = "H";
                                    break;
                                }
                            case "i":
                                {
                                    newTextArray[i] = "j";
                                    break;
                                }
                            case "I":
                                {
                                    newTextArray[i] = "J";
                                    break;
                                }
                            case "k":
                                {
                                    newTextArray[i] = "l";
                                    break;
                                }
                            case "K":
                                {
                                    newTextArray[i] = "L";
                                    break;
                                }
                            case "l":
                                {
                                    newTextArray[i] = "m";
                                    break;
                                }
                            case "L":
                                {
                                    newTextArray[i] = "M";
                                    break;
                                }
                            case "m":
                                {
                                    newTextArray[i] = "n";
                                    break;
                                }
                            case "M":
                                {
                                    newTextArray[i] = "N";
                                    break;
                                }
                            case "n":
                                {
                                    newTextArray[i] = "o";
                                    break;
                                }
                            case "N":
                                {
                                    newTextArray[i] = "O";
                                    break;
                                }
                            case "o":
                                {
                                    newTextArray[i] = "p";
                                    break;
                                }
                            case "O":
                                {
                                    newTextArray[i] = "P";
                                    break;
                                }
                            case "p":
                                {
                                    newTextArray[i] = "q";
                                    break;
                                }
                            case "P":
                                {
                                    newTextArray[i] = "Q";
                                    break;
                                }
                            case "q":
                                {
                                    newTextArray[i] = "p";
                                    break;
                                }
                            case "Q":
                                {
                                    newTextArray[i] = "P";
                                    break;
                                }
                            case "r":
                                {
                                    newTextArray[i] = "s";
                                    break;
                                }
                            case "R":
                                {
                                    newTextArray[i] = "S";
                                    break;
                                }
                            case "s":
                                {
                                    newTextArray[i] = "t";
                                    break;
                                }
                            case "S":
                                {
                                    newTextArray[i] = "T";
                                    break;
                                }
                            case "t":
                                {
                                    newTextArray[i] = "u";
                                    break;
                                }
                            case "T":
                                {
                                    newTextArray[i] = "U";
                                    break;
                                }
                            case "u":
                                {
                                    newTextArray[i] = "v";
                                    break;
                                }
                            case "U":
                                {
                                    newTextArray[i] = "V";
                                    break;
                                }
                            case "v":
                                {
                                    newTextArray[i] = "x";
                                    break;
                                }
                            case "V":
                                {
                                    newTextArray[i] = "X";
                                    break;
                                }
                            case "x":
                                {
                                    newTextArray[i] = "y";
                                    break;
                                }
                            case "X":
                                {
                                    newTextArray[i] = "Y";
                                    break;
                                }
                            case "y":
                                {
                                    newTextArray[i] = "z";
                                    break;
                                }
                            case "Y":
                                {
                                    newTextArray[i] = "Z";
                                    break;
                                }

                            default:
                                {
                                    newTextArray[i] = textArray[i];
                                    break;
                                }
                        }
                    }

                    //string s = newTextArray.Join();
                    //s = string.Join("*", newTextArray);

                    for (int i = 0; i < newTextArray.Length; i++)
                    {
                        s += newTextArray[i];
                    }


                }

                return s;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
