# UniVerServer

## Getting Started

The following instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

Ensure that you have latest version of Visual Studio installed and install the ASP .NET Core dependancies and App tpye. Be sure to install the .NET and rloads into your installation directory.

### How to install

### Installation
Here are a couple of ways to clone this repo:

1. GitHub Desktop </br>
Enter `https://github.com/LeandervanAarde/UniVerServer.git` into the URL field and press the `Clone` button.

2. Clone Repository </br>
Run the following in the command-line to clone the project:
   ```sh
   git clone https://github.com/LeandervanAarde/UniVerse.git
   ```
    Open `Software` and select `File | Open...` from the menu. Select cloned directory and press `Open` button

3. Run the App on your local machine
   Once installed on your machine, open appSettings.Json and enter your PostGresSql information under connectionStrings (DefaultConnection is the field you're looking for). Once that is done open the package manager console and run the following commands
   ```sh
    Update-Database
   ```
   This will add the migrations to your local Database for the nedded schemas. You can now open swagger and add people or alternatively run it on The MAUI application.
   To run the backend, open visual Studio and press the play button, the buttons label is "https", this will open the swagger interface. 
