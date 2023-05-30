using System;
using static System.Console;
using System.IO;

namespace SpectraConfCreator
{
    internal class SpectraCfgCreator
    {
        static void Main(string[] args)
        {
            Title = "Spectralink cfg File Creator";
            bool userInputBool = false;
            string userInput;
            bool userInput2Bool = false;
            string userInput2;
            bool userInput3Bool = false;
            string userInput3;
            bool fileExistsBool = false;
            string userInput4;
            bool userInput4Bool = false;
            bool fileCreated = false;
            do
            {
                Write("Enter the MAC address of the phone >> ");
                userInput = ReadLine();
                if (userInput.Length == 12)
                {
                    userInputBool = true;
                    break;
                }
                else { userInputBool = false; }
            } while (userInputBool != true);
            do
            {
                Write("Enter the extension of the phone >> ");
                userInput3 = ReadLine();
                if (userInput3.Length == 6) // IF YOUR EXTENSIONS ARE NOT EXACTLY 6 DIGITS, CHANGE THIS LINE OF CODE
                {
                    userInput3Bool = true;
                    break;
                }
                else { userInputBool = false; }
            } while (userInput3Bool != true);
            WriteLine($"MAC Address:\n{userInput}\n");
            WriteLine($"Extension: \n{userInput3}\n");

            // check if file exists
            DirectoryInfo pwd = new DirectoryInfo(".");
            var masterCFGFilePath = (pwd.FullName + "\\" + userInput + ".cfg");
            var handsetCFGFilePath = (pwd.FullName + "\\" + userInput + "-ext.cfg");
            FileInfo masterCFGFileInfo = new FileInfo(masterCFGFilePath);
            FileInfo handsetCFGFileInfo = new FileInfo(handsetCFGFilePath);
            DateTime masterCFGLWT = masterCFGFileInfo.LastWriteTime;
            DateTime handsetCFGLWT = handsetCFGFileInfo.LastWriteTime;
            if (masterCFGFileInfo.Exists)
            {
                WriteLine("WARNING: File already exists: \n" + masterCFGFileInfo.FullName + "\nLast Write Time: " + masterCFGLWT + "\n");
                fileExistsBool = true;
            }
            if (handsetCFGFileInfo.Exists)
            {
                WriteLine("WARNING: File already exists: \n" + handsetCFGFileInfo.FullName + "\nLast Write Time: " + handsetCFGLWT);
                fileExistsBool = true;
            }

            do
            {
                Write("\nPress 1 to create the files or 2 to exit >> ");
                userInput2 = ReadLine();
                    switch (userInput2)
                    {
                        case "1":
                            {
                                if (fileExistsBool == true)
                                {
                                    WriteLine("Would you like to overwrite the .cfg files?");
                                    do
                                    {
                                        Write("Press 1 for \"yes\" or 2 for \"no\" >> ");
                                        userInput4 = ReadLine();
                                        switch (userInput4)
                                        {
                                            case "1":
                                            {
                                                WriteLine("Overwriting files...");
                                                MasterConfigFile(userInput);
                                                HandsetConfigFile(userInput,userInput3);
                                                userInput2Bool = true;
                                                userInput4Bool = true;
                                                fileCreated = true;
                                                break;
                                            }
                                            case "2":
                                            {
                                                userInput4Bool = true;
                                                break;
                                            }
                                            default:
                                            {
                                                WriteLine("Invalid Input!");
                                                break;
                                            }
                                        }
                                    } while (userInput4Bool != true);
                                }
                                
                                if (userInput4Bool == false)
                                {
                                    MasterConfigFile(userInput);
                                    HandsetConfigFile(userInput, userInput3);
                                    userInput2Bool = true;
                                    fileCreated = true;
                                }
                                break;
                            }
                        case "2":
                            {
                                userInput2Bool = true;
                                break;
                            }
                        default:
                            {
                                WriteLine("Invalid input!");
                                userInput2Bool = false;
                                break;
                            }
                    }
            } while (userInput2Bool != true);
            if (fileCreated == true)
            {
                FileInfo masterCFGFileInfo2 = new FileInfo(masterCFGFilePath);
                FileInfo handsetCFGFileInfo2 = new FileInfo(handsetCFGFilePath);
                DateTime masterCFGLWT2 = masterCFGFileInfo2.LastWriteTime;
                DateTime handsetCFGLWT2 = handsetCFGFileInfo2.LastWriteTime;
                WriteLine("\nFiles created: \n");
                WriteLine(masterCFGFileInfo2.FullName + "\nLast Write Time: " + masterCFGLWT2 + "\n");
                WriteLine(handsetCFGFileInfo2.FullName + "\nLast Write Time: " + handsetCFGLWT2);
                Write("\nPress any key to exit..."); ReadKey();
            }
        }
        static void MasterConfigFile(string userInputMAC) // method creates the MAC.cfg file
        {
            using (StreamWriter sw = File.CreateText($"{userInputMAC}" + ".cfg"))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                sw.WriteLine("<!-- *************************************************************************** -->");
                sw.WriteLine("<!-- * Default Master SIP Configuration File for FLAT DEPLOYMENTS              * -->");
                sw.WriteLine("<!-- *                                                                         * -->");
                sw.WriteLine("<!-- * This file is read by every handset at boot to specify where to load its * -->");
                sw.WriteLine("<!-- * software from (APP_FILE_PATH), to specfify the sequence in which to read* -->");
                sw.WriteLine("<!-- * configuration files (CONFIG_FILES) and whether to store or retrieve     * -->");
                sw.WriteLine("<!-- * information in specific directories (server root by default)            * -->");
                sw.WriteLine("<!-- *************************************************************************** -->");
                sw.WriteLine("<MASTER_CONFIG>");
                sw.WriteLine("  <!-- You can specify a path with subdirectories to specify the location of the handset software.  -->");
                sw.WriteLine("  <!-- See the Deployment Guide for more information about setting up subdirectories. -->");
                sw.WriteLine("  <SOFTWARE APP_FILE_PATH=\"sip.ld\" />");
                sw.WriteLine("  <!-- Information from files on the left overrules information from files to their right -->");
                sw.WriteLine("  <!-- [PHONE_MAC_ADDRESS]dynamically gets replaced by that handset's MAC address -->");
                sw.WriteLine("  <!-- For FLAT DEPLOYMENT, create a <macaddress>-ext.cfg for each handset following the -->");
                sw.WriteLine("  <!-- template provided in this directory -->");
                sw.WriteLine("  <CONFIGURATION CONFIG_FILES=\"{0}-ext.cfg, site.cfg, oai.cfg, ptt.cfg\" />", userInputMAC);
                sw.WriteLine("  <DIRECTORIES LOG_FILE_DIRECTORY=\"\" OVERRIDES_DIRECTORY=\"\" CONTACTS_DIRECTORY=\"\" CALL_LISTS_DIRECTORY=\"\" />");
                sw.WriteLine("</MASTER_CONFIG>");
            }
        }
        static void HandsetConfigFile(string userInputMAC, string userInputExt) // method creates the MAC-ext.cfg file
        {
            using (StreamWriter sw = File.CreateText($"{userInputMAC}" + "-ext.cfg"))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>");
                sw.WriteLine("<handsetConfig xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"handsetConfig.xsd\">");
                sw.WriteLine("  <!-- *************************************************************************** -->");
                sw.WriteLine("  <!-- *  Sample per-phone Configuration File for FLAT  DEPLOYMENTS              * -->");
                sw.WriteLine("  <!-- *                                                                         * -->");
                sw.WriteLine("  <!-- * This file contains the user-specific information for the user of a      * -->");
                sw.WriteLine("  <!-- * specific handset identified by its MAC address. In particular it        * -->");
                sw.WriteLine("  <!-- * contains the user name and extention for that user.                     * -->");
                sw.WriteLine("  <!-- * -->");
                sw.WriteLine("  <!-- * -->");
                sw.WriteLine("  <!-- * -->");
                sw.WriteLine("  <!-- * This file MUST be named as <macaddress>-ext.cfg where <macaddress> is the   * -->");
                sw.WriteLine("  <!-- * 12 digit MAC address of the handset without : separators. The MAC       * -->");
                sw.WriteLine("  <!-- * address is printed on the label in the battery well of each handset.    * -->");
                sw.WriteLine("  <!-- * For example if the handset label reads MAC: 00:90:7A:0E:7F:E%, this file* -->");
                sw.WriteLine("  <!-- * should be named 00907a0e7fe5-ext.cfg                                        * -->");
                sw.WriteLine("  <!-- *************************************************************************** -->");
                sw.WriteLine("  <LineRegistration>");
                sw.WriteLine("    <!-- * -->");
                sw.WriteLine("    <!-- * -->");
                sw.WriteLine("    <!-- The information below is the user-specific information. The global server settings are in -->");
                sw.WriteLine("    <!-- the SystemParameters->TelephonyParameters->SIPserver -->");
                sw.WriteLine("    <openSIPTelephony call.callsPerLineKey=\"24\">");
                /* THIS LINE CONTAINS THE SERVER ADDRESS, USER EXTENSION, AND AUTHORIZATION PASSWORD THAT WILL NEED CUSTOMIZED TO YOUR SETUP */ sw.WriteLine("      <TelephonyLine1 reg.1.address=\"{0}@192.168.0.1\" reg.1.auth.password=\"0000\" reg.1.auth.userID=\"{0}\" reg.1.label=\"{0}\" reg.1.displayName=\"{0}\"></TelephonyLine1>", userInputExt);
                sw.WriteLine("      <!-- Additional lines: -->");
                sw.WriteLine("      <!-- * -->");
                sw.WriteLine("      <!-- Additional telephony lines can be addded (reg.3, etc...) by copying the TelephonyLine1 group above and -->");
                sw.WriteLine("      <!-- editing appropriately-->");
                sw.WriteLine("    </openSIPTelephony>");
                sw.WriteLine("  </LineRegistration>");
                sw.WriteLine("  <qbc qbc.connect.ipAddress-hostname=\"\">");
                sw.WriteLine("    <!-- This is the IP address or hostname of the computer the phone should connect to in single mode. -->");
                sw.WriteLine("    <!-- See the QBC Admin Guide for detailed information. -->");
                sw.WriteLine("  </qbc>");
                sw.WriteLine("</handsetConfig>");
            }
        }
    }
}