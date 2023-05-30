# SpectraLinkConfigurationGenerator
SpectraConfCreator
=================

SpectraConfCreator is a C# program for generating Spectralink configuration files for SIP handsets in a flat file deployment setup.

Configuration Files
-------------------

The program generates two configuration files:

1. `<MAC>.cfg`: This is the master configuration file that specifies the software location, configuration file sequence, and information directories. 

2. `<MAC>-ext.cfg`: This file contains user-specific information for a particular handset identified by its MAC address. 

Note: Make sure to follow the Spectralink deployment guide for detailed information on setting up subdirectories and customizing the configuration files.

Usage
-----

To use SpectraConfCreator, follow these steps:

1. Customize the `<MAC>.cfg` and `<MAC>-ext.cfg` files according to your infrastructure and setup.

2. Compile and run the program using a C# compiler or integrated development environment (IDE) of your choice.

3. Enter the MAC address of the phone (12 digits without colons) when prompted.

4. Enter the extension of the phone (6 digits by default, line 37) when prompted.

5. Choose whether to overwrite existing configuration files or exit the program.

6. Once the files are created, their details (file path and last write time) will be displayed.

ASSISTANCE
----------

If you need any assistance using this, please feel free to reach out to me by email: jordanwhthd@gmail.com
I cannot help you with deployments of SpectraLink infrastructure, but I would be glad to help you fill in the blanks for the parameters of these .cfg files as they relate to your environment. 

License
-------

This code is released under the MIT License.

Feel free to modify and use it to meet your requirements in your Spectralink deployment setup.
