# _Hair Salon.Solutions_

#### _MVC web application for Epicodus,C# Week 4 project. 12.14.2018_

#### By _**Manasa Vesala**_

## Description

_MVC web application for a hair salon. where the  owner should be able to add a list of the stylists, and for each stylist, add clients who see that stylist and view/add speciality and save that data into database._

## Specifications

_For HairSalon_

* _Homepage consints of View Stylist, View Clients List, view Specialty list and Assign stylist to specialty ._
* _when you click on View Stylist it takes to new page to view Lists of Stylists._
* _In the Styist page user can add, delete or update the Stylists._ 
* _If you click on Stylists you can see all the clients for that Stylist._
* _If you click on view Clients in the Homepage it takes to lists of clients page._
* _you can add or delete the clients only on the Stylists page._
* _If you click on View Specialty you can see all the specialties listed.In that you can add Specialty._
* _You can delete and update clients informationas well_
* _For a specific stylist you can view all his specialities by clicking on the View Specialties link in List of stylist page._
* _For a specific specality you can view all the stylists by clicking on the View stylists link in List of stylist page._
* _You can delete specialty if you like._
* _You can delete all Stylists if you like._
* _You can delete all the clients if you like._

## Setup/Installation Requirements

1. _Clone this repository https://github.com/manasavesala/HairSalon.Solution.git_
2. _In MySQL_
   * CREATE DATABASE manasa_vesala;
   * USE manasa_vesala;
   * CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255));
   * CREATE TABLE stylists (id serial PRIMARY KEY, description VARCHAR(255));
3. _In Terminal Change into the work directory:: $ cd HairSalon.Solution_
4. _To edit the project, open the project in your preferred text editor._
5. _To run the program, first navigate to the location of the HairSalon.cs file_ 
6. _To run the program: $ cd HairSalon $ cd dotnet build $cd dotnet run_
7. _To run the tests, use these commands: $ cd HairSalon.Tests $ dotnet test_
8. _Navigate to http://localhost:5000 in your browser to view the splashpage._

## Support and contact details

_Contact Manasa Vesala - vesalamanasa@gmail.com_


## Technologies Used

* _C#_
* _.NET_
* _MySQL_
* _Atom_
* _Git_
* _GitHub_
* _MAMP_
* _HTML_
* _BootStrap_

### License

*This software is licensed under the MIT license.*

Copyright (c) 2018 **_Manasa Vesala_**