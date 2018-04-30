# Scan
An attempt to build a 'smart' Fridge application by using barcodes to keep track of fridge inventory.

## Authors:
Andreea Andreea Oprea
Eugenio Maria Capuani
Jayanth Selvaraj

DISCLAIMER: this project is still under development and does not work as intended at the time of writing. 

*Please note: for various reasons, we ended up using a number of different repositories through the course of this project: Oldrer repositories, containing previous attempt at this or previous iterations of this code, have been included here as sub-trees with link provided to the original repo*.

##Requirements
- A barcode scanner of sorts
- .NET core SDK => 1.4
- MySQL 5.7
- a Dropbox account fro syncing, if desired

##Setup

0) We assume you have both MySQL insalled alongside the .NET SDK.
1) Create a new sql connection or use an existing.
2) Import DB from Server>Import Data, set schema name as "scan". you can find the SQL file in the 'SQL DB' folder.
4) Open the VS project, navigate to the *ScanContex* class and modify the connection string to match your parameters.
4.5) In order to use Droèbox you will need to register an API App first, and generate a private access token.
6) Log into DropBox.
7)  go to [this link] (https://www.dropbox.com/developers/documentation/dotnet#tutorial), look for the sectiona names "Register a Dropbox API app" and follow the link: tou will be guided through the setup of an API app
8) Once at the last screen, with the process complete, scroll untill you find an otion to generate an access token: you will need this later, we suggest copying it somewhere safe.
9) Navigate to the Program.cs class and on line 26, sobstitute your access token inside the double quotes
10) connect your barcode reader and tou should be good to go, just build and run
11) remeber that the barcode reader output appears as a keyboard input: in order for the program to work corectl, you have to have the console/terminal as your selected window when you scan a product.