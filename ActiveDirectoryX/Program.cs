﻿using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Policy;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ActiveDirectoryX
{
    class Program
    {

        public static string ReadPassword()
        {

            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // remove one character from the list of password characters
                        password = password.Substring(0, password.Length - 1);
                        // get the location of the cursor
                        int pos = Console.CursorLeft;
                        // move the cursor to the left by one character
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        // replace it with space
                        Console.Write(" ");
                        // move the cursor to the left by one character again
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }



        static void Main(string[] args)
        {

            string dominio = @"global.baxter.com";
            string path = @"LDAP://global.baxter.com";
            string usuario;
            string contraseña;
            
            
            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, dominio))
            {
               
                Console.WriteLine("Ingrese nombre de usuario: ");
                usuario = Console.ReadLine();
                Console.WriteLine("Ingrese Contraseña: ");
                contraseña = ReadPassword(); 

                bool userValid = principalContext.ValidateCredentials(usuario, contraseña);

               
                if (userValid == true)
                {
                    Console.WriteLine(userValid + "    " + "validacion correcta");
                    Console.WriteLine( "+++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++");


                    using (UserPrincipal user = UserPrincipal.FindByIdentity(principalContext, usuario))
                    {



                        /////////BLOQUE1
                        if (user != null)
                        {
                            PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();
                            Console.WriteLine("El usuario pertenece a los siguientes grupos: ");
                            Console.WriteLine("\n\n");

                            // iterate over all groups
                            foreach (Principal p in groups)
                            {
                                // make sure to add only group principals
                                if (p is GroupPrincipal)
                                {

                                    //Console.WriteLine((GroupPrincipal)p);
                                    Console.WriteLine(p.SamAccountName);

                                }
                            }
                        }


                        /////BLoque2 obtiene grupo y grupos anidados
                        //List<string> result = new List<string>();
                        //WindowsIdentity wi = new WindowsIdentity(usuario);

                        //foreach (IdentityReference group in wi.Groups)
                        //{
                        //    try
                        //    {
                        //        result.Add(group.Translate(typeof(NTAccount)).ToString());
                        //    }
                        //    catch (Exception ex) { }
                        //}
                        //result.Sort();
                        //foreach (var t in result)
                        //{
                        //    Console.WriteLine(t);

                        //}








                        ////BLOQUE3
                        //using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                        //{
                        //    string result = string.Empty;
                        //    string result1 = string.Empty;
                        //    string result2 = string.Empty;
                        //    string result3 = string.Empty;
                        //    string result4 = string.Empty;
                        //    string result5 = string.Empty;
                        //    DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);

                        //    if (de != null)
                        //    {

                        //        //if (de.Properties.Contains("department"))
                        //        //{
                        //        result = de.Properties["userPrincipalName"][0].ToString();
                        //        result1 = de.Properties["manager"].Value.ToString();
                        //        result2 = de.Properties["title"][0].ToString();
                        //        result3 = de.Properties["company"][0].ToString();
                        //        result4 = de.Properties["employeeType"][0].ToString();
                        //        //result5 = de.Properties["manager"][0].ToString();
                        //        //result5 = de.Properties["allowedAttributes"].PropertyName.ToString();
                        //        result5 = de.Properties["allowedAttributes"].PropertyName.ToString();

                        //        Console.WriteLine(result + "\n" + result1 + "\n" + result2 + "\n" + result3 + "\n" + result4 + "\n" + result5);
                        //        //}
                        //    }
                        //}





                        ///////Bloque4
                        //using (var searcher = new DirectorySearcher(new DirectoryEntry(path)))
                        //{
                        //    searcher.Filter = String.Format("(&(objectCategory=group)(member={0}))", user.DistinguishedName);
                        //    searcher.SearchScope = SearchScope.Subtree;
                        //    searcher.PropertiesToLoad.Add("CN");

                        //    foreach (SearchResult entry in searcher.FindAll())
                        //        if (entry.Properties.Contains("CN"))
                        //            Console.WriteLine(entry.Properties["CN"][0].ToString());
                        //}  //LIST RAPIDA





                    }







                }
                else { Console.WriteLine(userValid + "    " + " validacion incorrecta"); }


              









               

                Console.ReadKey();



               

               











            }

            }

            //using (GroupPrincipal oGroupPrincipal = new GroupPrincipal(principalContext))
            //{
            //    PrincipalSearcher srch = new PrincipalSearcher(oGroupPrincipal);

            //    foreach (var found in srch.FindAll())
            //    {

            //        GroupPrincipal foundGroup = found as GroupPrincipal;

            //        if (foundGroup != null)
            //        {
            //            Console.WriteLine(foundGroup.);
            //        }
            //        // do whatever here - "found" is of type "Principal" - it could be user, group, computer.....          
            //    }

            
            }

            


        }
    

